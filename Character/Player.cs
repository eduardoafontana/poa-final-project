using System;
using System.Collections.Generic;
using System.Linq;
using Wumpus.Environment;

namespace Wumpus.Character
{
    public class Player : PlayerInterface
    {
        private string name;
        private Memory[,] forestMemory;
        private Memory memoryPlayerPosition;
        private int[] playerPosition;

        public Player(int forestDimension)
        {
            forestMemory = new Memory[forestDimension, forestDimension];

            for(int l = 0; l < forestDimension; l++)
            {
                for(int c = 0; c < forestDimension; c++)
                {
                    forestMemory[l,c] = new Memory(l, c);
                }
            }
        }

        public ExplorerNode Play()
        {
            ObserveAndMemorizeAllForest();
            
            ExplorerNode node = Reflexion();

            return node;
        }

        public void ObserveAndMemorizeCurrentPosition(CellMemory cellMemory)
        { 
            memoryPlayerPosition.CalculateLocalProbabilityMonster(cellMemory.ProbabilityMonster);
            memoryPlayerPosition.CalculateLocalProbabilityCave(cellMemory.ProbabilityCave);
            
            memoryPlayerPosition.CheckExistOdor(cellMemory.ExistOdeur);
            memoryPlayerPosition.CheckExistVent(cellMemory.ExistVent);
            memoryPlayerPosition.CheckExistLuminosite(cellMemory.ExistLuminosite);
        }


        //met a jour les connaissances du joueur de la foret
        //la memoire du joueur attribu a chaque case de la foret une liste de 7 valeurs : 
        //  3 pour la probabilité d'avoir soit un monstre, soit une crevasse, soit le portal 
        //  1 valeur pour le nombre de passage du joueur sur la case
        //  3 pour connaitre l'etat des capteurs sur la case (lumiere, ordeur, vent)
        private void ObserveAndMemorizeAllForest()
        {
            forestMemory.Cast<Memory>().ToList().ForEach(itemMemory => itemMemory.CalculateProbabilityPortal());

            forestMemory.Cast<Memory>()
            .Where(itemMemory => itemMemory.IsCellIsNotExplored())
            .ToList().ForEach(itemMemory => {
                itemMemory.ResetProbabilityVariables();

                Explorer.GetInstance().OnNeighbors()
                .Where(neighbor => neighbor.IsValidPosition(neighbor.GetLine(itemMemory.Line), neighbor.GetColumn(itemMemory.Column), forestMemory.GetLength(0)))
                .ToList()
                .ForEach(neighbor => {
                    itemMemory.AnalyzeOdorNeighbor(forestMemory[neighbor.GetLine(itemMemory.Line), neighbor.GetColumn(itemMemory.Column)].ExistOdeur);
                    itemMemory.AnalyzeVentNeighbor(forestMemory[neighbor.GetLine(itemMemory.Line), neighbor.GetColumn(itemMemory.Column)].ExistVent);
                });
                
                itemMemory.CalculateProbabilityMonster();
                itemMemory.CalculateProbabilityCave();
            });
        }

        public bool NeedThrowStone(ExplorerNode d)
        {
            return forestMemory[d.GetLine(playerPosition[0]), d.GetColumn(playerPosition[1])].ProbabilityMonster > 0;
        }

        //determiner la case la plus probable de contenir le portal
        private ExplorerNode Reflexion()
        {
            //parcours memoire pour trouver max proba portal
            float proba_portal_max = forestMemory.Cast<Memory>().Max(x => x.ProbabilityPortal);

            //parcours memoire pour trouver min proba crevasse pour max proba portal
            float proba_crevasse_min = 100;
            proba_crevasse_min = forestMemory.Cast<Memory>().Where(x => x.ProbabilityPortal == proba_portal_max).Min(x => x.ProbabilityCave);

            //calculer eloignement de chaque case portal avec proba la plus forte
            forestMemory.Cast<Memory>().ToList().ForEach(item => item.DistanceRelative = GetCellNearBy(item.Line, item.Column));

            memoryPlayerPosition.DistanceRelative = 0;
            
            //trouver la distance la plus low avec proba portal la plus forte et min proba crevasse
            int case_la_plus_proche = Int32.MaxValue;
            case_la_plus_proche = forestMemory.Cast<Memory>().Where(x => x.ProbabilityPortal == proba_portal_max && x.ProbabilityCave == proba_crevasse_min).Min(x => x.DistanceRelative);

            //trouver une case
            Memory cellToGo = forestMemory.Cast<Memory>().OrderBy(x => x.Line).ThenBy(x => x.Column).LastOrDefault(x => x.ProbabilityPortal == proba_portal_max && x.ProbabilityCave == proba_crevasse_min && x.DistanceRelative == case_la_plus_proche);

            //si c'est notre case, prendre le portal
            if(cellToGo == memoryPlayerPosition)
                return new ExplorerNode('P', 0);

            //sinon se diriger vers la case
            return GetDirectionToGoTo(cellToGo.Line, cellToGo.Column);
        }

        //place le joueur sur la grille
        public int[] UpdatePlayerPosition(int l, int c)
        {
            memoryPlayerPosition = forestMemory[l, c];
            memoryPlayerPosition.AmountOfPassage++; //ajoute 1 au nombre de passage sur cette case dans la memoire du joueur
            playerPosition = new int[] {l, c};

            return playerPosition;
        }

        //renvoie le nombre de case le plus proche de l'objectif situé en [lf, cf]
        private int GetCellNearBy(int lf, int cf)
        {
            forestMemory.Cast<Memory>().ToList().ForEach(item => item.Passage = Int32.MaxValue);

            memoryPlayerPosition.Passage = 0;
            
            List<int> list_n = Explorer.GetInstance().OnNeighbors()
            .Where(item => item.IsValidPosition(item.GetLine(playerPosition[0]), item.GetColumn(playerPosition[1]), forestMemory.GetLength(0)))
            .Select(item => 
                DeepGetDirectionToGoTo(new int[] { lf, cf }, new int[] { item.GetLine(playerPosition[0]), item.GetColumn(playerPosition[1]) }, 1)
            ).ToList();

            return list_n.Min();
        }

        //renvoie la direction pour se rendre à l'objectif situé en [lf, cf]
        private ExplorerNode GetDirectionToGoTo(int lf, int cf)
        {
            int l0 = playerPosition[0];
            int c0 = playerPosition[1];

            forestMemory.Cast<Memory>().ToList().ForEach(x => x.Passage = Int32.MaxValue);

            memoryPlayerPosition.Passage = 0;

            List<ExplorerNode> listMemories = Explorer.GetNeighborsFilted(forestMemory, new int[] {l0, c0})
            .Select(item => 
                new ExplorerNode(item.GetLine(), item.GetColumn(), item.Direction, DeepGetDirectionToGoTo(new int[] { lf, cf }, new int[] { item.GetLine(l0), item.GetColumn(c0) }, 1))
            ).ToList();

            if(listMemories.Count() == 0)
                return new ExplorerNode('X', 0);

            if(listMemories.Min().Distance == Int32.MaxValue)
                return new ExplorerNode('X', 0);
            else
                return listMemories.Min();
        }

        //permet de trouver un chemin evitant les crevasses (les montres ne sont pas pris en compte)
        private int DeepGetDirectionToGoTo(int[] origin, int[] destination, int countDistance)
        {
            int lf = origin[0];
            int cf = origin[1];
            int l0 = destination[0];
            int c0 = destination[1];

            forestMemory[l0,c0].Passage = countDistance;

            if(l0 == lf && c0 == cf)
                return countDistance;

            List<int> list_n = Explorer.GetNeighborsFilted(forestMemory, new int[] {l0, c0})
            .Where(x => forestMemory[x.GetLine(l0), x.GetColumn(c0)].Passage > countDistance + 1)
            .Select(item => 
                DeepGetDirectionToGoTo(new int[] { lf, cf }, new int[] { item.GetLine(l0), item.GetColumn(c0) }, countDistance + 1)
            ).ToList();

            return list_n.Count() > 0 ? list_n.Min() : Int32.MaxValue;
        }
    }
}