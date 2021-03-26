using System;
using System.Collections.Generic;
using System.Linq;

namespace Wumpus
{
    public class Player
    {
        private string name;
        private Memory[,] forestMemory;
        private Memory memoryPlayerPosition;

        public int Pos_l
        {
            get { return memoryPlayerPosition.Line; }
        }

        public int Pos_c
        {
            get { return memoryPlayerPosition.Column; }
        }

        public string Name
        {
            get { return name; }
        }

        public Player(string name, int dim_foret)
        {
            this.name = name;

            forestMemory = new Memory[dim_foret, dim_foret];

            for(int l = 0; l < dim_foret; l++){
                for(int c = 0; c < dim_foret; c++){
                    forestMemory[l,c] = new Wumpus.Memory(l, c);
                }
            }
        }

        public MemoryManager.Node Play(Foret foret)
        {
            ObserveAndMemorizeCurrentPosition(foret.Grille);
            ObserveAndMemorizeAllForest();
            
            MemoryManager.Node node = Reflexion();

            return node;
        }

        public void ObserveAndMemorizeCurrentPosition(Case[,] foret)
        { 
            memoryPlayerPosition.CalculateLocalProbabilityMonster(foret[memoryPlayerPosition.Line, memoryPlayerPosition.Column].Type);
            memoryPlayerPosition.CalculateLocalProbabilityCave(foret[memoryPlayerPosition.Line, memoryPlayerPosition.Column].Type);
            
            memoryPlayerPosition.CheckExistOdor(foret[memoryPlayerPosition.Line, memoryPlayerPosition.Column].Odeur);
            memoryPlayerPosition.CheckExistVent(foret[memoryPlayerPosition.Line, memoryPlayerPosition.Column].VitesseVent);
            memoryPlayerPosition.CheckExistLuminosite(foret[memoryPlayerPosition.Line, memoryPlayerPosition.Column].Luminosite);
        }


        //met a jour les connaissances du joueur de la foret
        //la memoire du joueur attribu a chaque case de la foret une liste de 7 valeurs : 
        //  3 pour la probabilité d'avoir soit un monstre, soit une crevasse, soit le portail 
        //  1 valeur pour le nombre de passage du joueur sur la case
        //  3 pour connaitre l'etat des capteurs sur la case (lumiere, ordeur, vent)
        public void ObserveAndMemorizeAllForest()
        {
            forestMemory.Cast<Memory>().ToList().ForEach(itemMemory => itemMemory.CalculateProbabilityPortal());

            forestMemory.Cast<Memory>()
            .Where(itemMemory => itemMemory.IsCaseIsNotExplored())
            .ToList().ForEach(itemMemory => {
                itemMemory.ResetProbabilityVariables();

                MemoryManager.GetInstance().OnNeighborhoods()
                .Where(neighborhood => Wumpus.Memory.PositionExist(neighborhood.GetLine(itemMemory.Line), neighborhood.GetColumn(itemMemory.Column), forestMemory.GetLength(0)))
                .ToList()
                .ForEach(neighborhood => {
                    itemMemory.AnalyzeOdorNeighborhood(forestMemory[neighborhood.GetLine(itemMemory.Line), neighborhood.GetColumn(itemMemory.Column)].ExistOdeur);
                    itemMemory.AnalyzeVentNeighborhood(forestMemory[neighborhood.GetLine(itemMemory.Line), neighborhood.GetColumn(itemMemory.Column)].ExistVent);
                });
                
                itemMemory.CalculateProbabilityMonster();
                itemMemory.CalculateProbabilityCave();
            });
        }

        internal bool NeedThrowStone(MemoryManager.Node d)
        {
            return forestMemory[d.GetLine(memoryPlayerPosition.Line), d.GetColumn(memoryPlayerPosition.Column)].ProbabilityMonster > 0;
        }

        //determiner la case la plus probable de contenir le portail
        public MemoryManager.Node Reflexion()
        {
            //parcours memoire pour trouver max proba portail
            float proba_portail_max = forestMemory.Cast<Memory>().Max(x => x.ProbabilityPortal);

            //parcours memoire pour trouver min proba crevasse pour max proba portail
            float proba_crevasse_min = 100;
            proba_crevasse_min = forestMemory.Cast<Memory>().Where(x => x.ProbabilityPortal == proba_portail_max).Min(x => x.ProbabilityCave);

            //calculer eloignement de chaque case portail avec proba la plus forte
            forestMemory.Cast<Memory>().ToList().ForEach(item => item.DistanceRelative = Nb_cases_vers(item.Line, item.Column));

            memoryPlayerPosition.DistanceRelative = 0;
            
            //trouver la distance la plus faible avec proba portail la plus forte et min proba crevasse
            int case_la_plus_proche = Int32.MaxValue;
            case_la_plus_proche = forestMemory.Cast<Memory>().Where(x => x.ProbabilityPortal == proba_portail_max && x.ProbabilityCave == proba_crevasse_min).Min(x => x.DistanceRelative);

            //trouver une case
            Memory caseToGo = forestMemory.Cast<Memory>().OrderBy(x => x.Line).ThenBy(x => x.Column).LastOrDefault(x => x.ProbabilityPortal == proba_portail_max && x.ProbabilityCave == proba_crevasse_min && x.DistanceRelative == case_la_plus_proche);

            //si c'est notre case, prendre le portail
            if(caseToGo == memoryPlayerPosition)
                return new MemoryManager.Node('P', 0);

            //sinon se diriger vers la case
            return Direction_vers(caseToGo.Line, caseToGo.Column);
        }

        //place le joueur sur la grille
        public bool UpdatePlayerPosition(int l, int c)
        {
            bool test = false;

            memoryPlayerPosition = forestMemory[l, c];
            memoryPlayerPosition.AmountOfPassage++; //ajoute 1 au nombre de passage sur cette case dans la memoire du joueur
            test = true;

            return test;
        }

        //renvoie le nombre de case le plus proche de l'objectif situé en [lf, cf]
        public int Nb_cases_vers(int lf, int cf)
        {
            forestMemory.Cast<Memory>().ToList().ForEach(item => item.Passage = Int32.MaxValue);

            memoryPlayerPosition.Passage = 0;
            
            List<int> list_n = MemoryManager.GetInstance().OnNeighborhoods().Select(item => 
                Direction_vers_recursif(lf, cf, item.GetLine(memoryPlayerPosition.Line), item.GetColumn(memoryPlayerPosition.Column), 1)
            ).ToList();

            return list_n.Min();
        }

        //renvoie la direction pour se rendre à l'objectif situé en [lf, cf]
        public MemoryManager.Node Direction_vers(int lf, int cf)
        {
            int l0 = memoryPlayerPosition.Line;
            int c0 = memoryPlayerPosition.Column;

            forestMemory.Cast<Memory>().ToList().ForEach(x => x.Passage = Int32.MaxValue);

            memoryPlayerPosition.Passage = 0;

            List<MemoryManager.Node> listMemories = MemoryManager.GetInstance().OnNeighborhoods()
            .Where(x => Memory.PositionExist(x.GetLine(l0), x.GetColumn(c0), forestMemory.GetLength(0)))
            .Where(x => forestMemory[x.GetLine(l0), x.GetColumn(c0)].ProbabilityCave < 100)
            .Select(item => 
                new MemoryManager.Node(item.GetLine(), item.GetColumn(), item.Direction, Direction_vers_recursif(lf, cf, item.GetLine(l0), item.GetColumn(c0), 1))
            ).ToList();

            if(listMemories.Count() == 0)
                return new MemoryManager.Node('X', 0);

            if(listMemories.Min().Distance == Int32.MaxValue)
                return new MemoryManager.Node('X', 0);
            else
                return listMemories.Min();
        }

        //permet de trouver un chemin evitant les crevasses (les montres ne sont pas pris en compte)
        public int Direction_vers_recursif(int lf, int cf, int l0, int c0, int n)
        {
            if(!Memory.PositionExist(l0, c0, forestMemory.GetLength(0)))
                return Int32.MaxValue;
                
            forestMemory[l0,c0].Passage = n;

            if(l0 == lf && c0 == cf){
                return n;
            }

            List<int> list_n = MemoryManager.GetInstance().OnNeighborhoods()
            .Where(x => Memory.PositionExist(x.GetLine(l0), x.GetColumn(c0), forestMemory.GetLength(0)))
            .Where(x => forestMemory[x.GetLine(l0), x.GetColumn(c0)].ProbabilityCave < 100 && forestMemory[x.GetLine(l0), x.GetColumn(c0)].Passage > n + 1)
            .Select(item => 
                Direction_vers_recursif(lf, cf, item.GetLine(l0), item.GetColumn(c0), n + 1)
            ).ToList();

            return list_n.Count() > 0 ? list_n.Min() : Int32.MaxValue;
        }
    }
}