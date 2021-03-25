using System;
using System.Collections.Generic;
using System.Linq;

namespace Wumpus
{
    public class Joueur
    {
        private string name;
        private Memory[,] memoire;
        private Memory playerPosition;
        int score = 0;
        
        public String messages = String.Empty;

        public int Pos_l
        {
            get { return playerPosition.Line; }
        }

        public int Pos_c
        {
            get { return playerPosition.Column; }
        }

        public string Name
        {
            get { return name; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Joueur(string name, int dim_foret)
        {
            this.name = name;

            memoire = new Memory[dim_foret, dim_foret];

            for(int l = 0; l < dim_foret; l++){
                for(int c = 0; c < dim_foret; c++){
                    memoire[l,c] = new Wumpus.Memory(l, c);
                }
            }
        }

        public bool Jouer(Foret foret)
        {
            ObserveAndMemorizeCurrentPosition(foret.Grille);
            ObserveAndMemorizeAllForest();
            
            MemoryManager.Node node = Reflexion();
            
            if(node.Direction == 'P'){
                Console.WriteLine(name + " prend le portail et passe au niveau suivant.");
                messages += name + " prend le portail et passe au niveau suivant." + Environment.NewLine;
            }
            else{
                Console.WriteLine(name + " va vers " + node.Direction);
                messages += name + " va vers " + node.Direction + Environment.NewLine;
            }
           
            return Bouger_vers(node, foret);
        }

        public void ObserveAndMemorizeCurrentPosition(Case[,] foret)
        { 
            playerPosition.CalculateLocalProbabilityMonster(foret[playerPosition.Line, playerPosition.Column].Type);
            playerPosition.CalculateLocalProbabilityCave(foret[playerPosition.Line, playerPosition.Column].Type);
            
            playerPosition.CheckExistOdor(foret[playerPosition.Line, playerPosition.Column].Odeur);
            playerPosition.CheckExistVent(foret[playerPosition.Line, playerPosition.Column].VitesseVent);
            playerPosition.CheckExistLuminosite(foret[playerPosition.Line, playerPosition.Column].Luminosite);
        }


        //met a jour les connaissances du joueur de la foret
        //la memoire du joueur attribu a chaque case de la foret une liste de 7 valeurs : 
        //  3 pour la probabilité d'avoir soit un monstre, soit une crevasse, soit le portail 
        //  1 valeur pour le nombre de passage du joueur sur la case
        //  3 pour connaitre l'etat des capteurs sur la case (lumiere, ordeur, vent)
        public void ObserveAndMemorizeAllForest()
        {
            memoire.Cast<Memory>().ToList().ForEach(itemMemory => itemMemory.CalculateProbabilityPortal());

            memoire.Cast<Memory>()
            .Where(itemMemory => itemMemory.IsCaseIsNotExplored())
            .ToList().ForEach(itemMemory => {
                itemMemory.ResetProbabilityVariables();

                MemoryManager.GetInstance().OnNeighborhoods()
                .Where(neighborhood => Wumpus.Memory.PositionExist(neighborhood.GetLine(itemMemory.Line), neighborhood.GetColumn(itemMemory.Column), memoire.GetLength(0)))
                .ToList()
                .ForEach(neighborhood => {
                    itemMemory.AnalyzeOdorNeighborhood(memoire[neighborhood.GetLine(itemMemory.Line), neighborhood.GetColumn(itemMemory.Column)].ExistOdeur);
                    itemMemory.AnalyzeVentNeighborhood(memoire[neighborhood.GetLine(itemMemory.Line), neighborhood.GetColumn(itemMemory.Column)].ExistVent);
                });
                
                itemMemory.CalculateProbabilityMonster();
                itemMemory.CalculateProbabilityCave();
            });
        }

        //determiner la case la plus probable de contenir le portail
        public MemoryManager.Node Reflexion(){
            //parcours memoire pour trouver max proba portail
            float proba_portail_max = memoire.Cast<Memory>().Max(x => x.ProbabilityPortal);

            //parcours memoire pour trouver min proba crevasse pour max proba portail
            float proba_crevasse_min = 100;
            proba_crevasse_min = memoire.Cast<Memory>().Where(x => x.ProbabilityPortal == proba_portail_max).Min(x => x.ProbabilityCave);

            //calculer eloignement de chaque case portail avec proba la plus forte
            memoire.Cast<Memory>().ToList().ForEach(item => item.DistanceRelative = Nb_cases_vers(item.Line, item.Column));

            playerPosition.DistanceRelative = 0;
            
            //trouver la distance la plus faible avec proba portail la plus forte et min proba crevasse
            int case_la_plus_proche = Int32.MaxValue;
            case_la_plus_proche = memoire.Cast<Memory>().Where(x => x.ProbabilityPortal == proba_portail_max && x.ProbabilityCave == proba_crevasse_min).Min(x => x.DistanceRelative);

            //trouver une case
            Memory caseToGo = memoire.Cast<Memory>().OrderBy(x => x.Line).ThenBy(x => x.Column).LastOrDefault(x => x.ProbabilityPortal == proba_portail_max && x.ProbabilityCave == proba_crevasse_min && x.DistanceRelative == case_la_plus_proche);

            //si c'est notre case, prendre le portail
            if(caseToGo == playerPosition)
                return new MemoryManager.Node('P', 0);

            //sinon se diriger vers la case
            return Direction_vers(caseToGo.Line, caseToGo.Column);
        }


        //bouge le joueur vers l'une des 4 directions, si la proba d'un monstre sur la case d'arrivee est non nul, le joueur jete une pierre
        public bool Bouger_vers(MemoryManager.Node d, Foret foret){
            score -= 1;

            if(d.Direction == 'X')
            {
                return false;
            }
            else if(d.Direction == 'P')
            {
                return true;
            }
            else
            {
                if(memoire[d.GetLine(playerPosition.Line), d.GetColumn(playerPosition.Column)].ProbabilityMonster > 0){
                    Jeter_pierre(d, foret);
                }

                Placer(d.GetLine(playerPosition.Line), d.GetColumn(playerPosition.Column));

                return false;
            }
        }

        //place le joueur sur la grille
        public bool Placer(int l, int c){
            bool test = false;

            playerPosition = memoire[l, c];
            playerPosition.AmountOfPassage++; //ajoute 1 au nombre de passage sur cette case dans la memoire du joueur
            test = true;

            return test;
        }


        //renvoie le nombre de case le plus proche de l'objectif situé en [lf, cf]
        public int Nb_cases_vers(int lf, int cf){
            memoire.Cast<Memory>().ToList().ForEach(item => item.Passage = Int32.MaxValue);

            playerPosition.Passage = 0;
            
            List<int> list_n = MemoryManager.GetInstance().OnNeighborhoods().Select(item => 
                Direction_vers_recursif(lf, cf, item.GetLine(playerPosition.Line), item.GetColumn(playerPosition.Column), 1)
            ).ToList();

            return list_n.Min();
        }

        //renvoie la direction pour se rendre à l'objectif situé en [lf, cf]
        public MemoryManager.Node Direction_vers(int lf, int cf)
        {
            int l0 = playerPosition.Line;
            int c0 = playerPosition.Column;

            memoire.Cast<Memory>().ToList().ForEach(x => x.Passage = Int32.MaxValue);

            playerPosition.Passage = 0;

            List<MemoryManager.Node> listMemories = MemoryManager.GetInstance().OnNeighborhoods()
            .Where(x => Memory.PositionExist(x.GetLine(l0), x.GetColumn(c0), memoire.GetLength(0)))
            .Where(x => memoire[x.GetLine(l0), x.GetColumn(c0)].ProbabilityCave < 100)
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
            if(!Memory.PositionExist(l0, c0, memoire.GetLength(0)))
                return Int32.MaxValue;
                
            memoire[l0,c0].Passage = n;

            if(l0 == lf && c0 == cf){
                return n;
            }

            List<int> list_n = MemoryManager.GetInstance().OnNeighborhoods()
            .Where(x => Memory.PositionExist(x.GetLine(l0), x.GetColumn(c0), memoire.GetLength(0)))
            .Where(x => memoire[x.GetLine(l0), x.GetColumn(c0)].ProbabilityCave < 100 && memoire[x.GetLine(l0), x.GetColumn(c0)].Passage > n + 1)
            .Select(item => 
                Direction_vers_recursif(lf, cf, item.GetLine(l0), item.GetColumn(c0), n + 1)
            ).ToList();

            return list_n.Count() > 0 ? list_n.Min() : Int32.MaxValue;
        }

        public void Jeter_pierre(MemoryManager.Node d, Foret foret){
            score -= 10;

            Console.WriteLine(name + " lance une pierre vers le " + d.Direction);
            messages += name + " lance une pierre vers le " + d.Direction + Environment.NewLine;

            foret.Utilisation_de_roches(d.GetLine(playerPosition.Line), d.GetColumn(playerPosition.Column));
        }
    }
}