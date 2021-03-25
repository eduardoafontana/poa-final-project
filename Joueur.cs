using System;
using System.Linq;

namespace Wumpus
{
    public class Joueur
    {
        private string name;
        private int pos_l;
        private int pos_c;
        private Memory[,] memoire;
        private Memory playerPosition;
        private int dim_foret;
        int score = 0;
        
        public String messages = String.Empty;

        public Joueur(string name, int dim_foret)
        {
            this.name = name;
            this.dim_foret = dim_foret;

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
            
            char d = Reflexion();
            
            if(d == 'P'){
                Console.WriteLine(name + " prend le portail et passe au niveau suivant.");
                messages += name + " prend le portail et passe au niveau suivant." + Environment.NewLine;
            }
            else{
                Console.WriteLine(name + " va vers " + d);
                messages += name + " va vers " + d + Environment.NewLine;
            }
           
            return Bouger_vers(d, foret);
        }

        public void ObserveAndMemorizeCurrentPosition(Case[,] foret)
        { 
            memoire[pos_l, pos_c].CalculateLocalProbabilityMonster(foret[pos_l,pos_c].Type);
            memoire[pos_l, pos_c].CalculateLocalProbabilityCave(foret[pos_l,pos_c].Type);
            
            memoire[pos_l, pos_c].CheckExistOdor(foret[pos_l,pos_c].Odeur);
            memoire[pos_l, pos_c].CheckExistVent(foret[pos_l,pos_c].VitesseVent);
            memoire[pos_l, pos_c].CheckExistLuminosite(foret[pos_l,pos_c].Luminosite);
        }


        //met a jour les connaissances du joueur de la foret
        //la memoire du joueur attribu a chaque case de la foret une liste de 7 valeurs : 
        //  3 pour la probabilité d'avoir soit un monstre, soit une crevasse, soit le portail 
        //  1 valeur pour le nombre de passage du joueur sur la case
        //  3 pour connaitre l'etat des capteurs sur la case (lumiere, ordeur, vent)
        public void ObserveAndMemorizeAllForest()
        {
            for(int l = 0; l < memoire.GetLength(0); l++){
                for(int c = 0; c < memoire.GetLength(1); c++){

                    memoire[l,c].CalculateProbabilityPortal();

                    if(memoire[l,c].IsCaseAlreadyExplored())
                        continue;

                    memoire[l,c].ResetProbabilityVariables();

                    // verification des cases N S W E
                    int[,] delta = {{-1, 0}, {0, 1}, {1, 0}, {0, -1}};
                    for(int i = 0; i < delta.GetLength(0); i++)
                    {
                        if(!Wumpus.Memory.PositionExist(l + delta[i,0], c + delta[i,1], dim_foret))
                            continue;

                        memoire[l,c].AnalyzeOdorNeighborhood(memoire[l + delta[i,0], c + delta[i,1]].ExistOdeur);
                        memoire[l,c].AnalyzeVentNeighborhood(memoire[l + delta[i,0], c + delta[i,1]].ExistVent);
                    }
                    
                    memoire[l,c].CalculateProbabilityMonster();
                    memoire[l,c].CalculateProbabilityCave();
                }
            }
        }

        //determiner la case la plus probable de contenir le portail
        public char Reflexion(){
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
                return 'P';

            //sinon se diriger vers la case
            return Direction_vers(caseToGo.Line, caseToGo.Column);
        }


        //bouge le joueur vers l'une des 4 directions, si la proba d'un monstre sur la case d'arrivee est non nul, le joueur jete une pierre
        public bool Bouger_vers(char d, Foret foret){
            score -= 1;
            if(d == 'N'){
                try{
                    if(memoire[pos_l - 1, pos_c].ProbabilityMonster > 0){
                        Jeter_pierre('N', foret);
                    }
                }
                catch{}
                Placer(pos_l - 1, pos_c);
                return false;
            }
            if(d == 'S'){
                try{
                    if(memoire[pos_l + 1, pos_c].ProbabilityMonster > 0){
                        Jeter_pierre('S', foret);
                    }
                }
                catch{}
                Placer(pos_l + 1, pos_c);
                return false;
            }
            if(d == 'W'){
                try{
                    if(memoire[pos_l, pos_c - 1].ProbabilityMonster > 0){
                        Jeter_pierre('W', foret);
                    }
                }
                catch{}
                Placer(pos_l, pos_c - 1);
                return false;
            }
            if(d == 'E'){
                try{
                    if(memoire[pos_l, pos_c + 1].ProbabilityMonster > 0){
                        Jeter_pierre('E', foret);
                    }
                }
                catch{}
                Placer(pos_l, pos_c + 1);
                return false;
            }
            if(d == 'P'){
                return Prendre_portail(foret.Grille);
            }
            Console.WriteLine("/!\\ " + d);
            //Console.ReadLine();
            return false;
        }



        //place le joueur sur la grille
        public bool Placer(int l, int c){
            bool test = false;
            if(0 <= l && l < dim_foret){
                if(0 <= c && c < dim_foret){
                    pos_l = l;
                    pos_c = c;
                    memoire[pos_l,pos_c].AmountOfPassage++; //ajoute 1 au nombre de passage sur cette case dans la memoire du joueur
                    playerPosition = memoire[pos_l,pos_c];
                    test = true;
                }
            }
            return test;
        }


        //renvoie le nombre de case le plus proche de l'objectif situé en [lf, cf]
        public int Nb_cases_vers(int lf, int cf){
            memoire.Cast<Memory>().ToList().ForEach(item => item.Passage = Int32.MaxValue);

            playerPosition.Passage = 0;

            int n_N = Int32.MaxValue;
            int n_S = Int32.MaxValue;
            int n_W = Int32.MaxValue;
            int n_E = Int32.MaxValue;
            
            n_N = Direction_vers_recursif(lf, cf, playerPosition.Line - 1, playerPosition.Column, 1);
            n_S = Direction_vers_recursif(lf, cf, playerPosition.Line + 1, playerPosition.Column, 1);
            n_W = Direction_vers_recursif(lf, cf, playerPosition.Line, playerPosition.Column - 1, 1);
            n_E = Direction_vers_recursif(lf, cf, playerPosition.Line, playerPosition.Column + 1, 1);

            int[] list_n = {n_N, n_S, n_W, n_E};
            return list_n.Min();
        }

        //renvoie la direction pour se rendre à l'objectif situé en [lf, cf]
        public char Direction_vers(int lf, int cf){
            int n_N = Int32.MaxValue;
            int n_S = Int32.MaxValue;
            int n_W = Int32.MaxValue;
            int n_E = Int32.MaxValue;
            int l0 = pos_l;
            int c0 = pos_c;

            for(int i = 0; i < dim_foret; i++){
                for(int j = 0; j < dim_foret; j++){
                    memoire[i,j].Passage = Int32.MaxValue;
                }
            }

            memoire[l0,c0].Passage = 0;
            if(Memory.PositionExist(l0 - 1, c0, memoire.GetLength(0))){
                if(memoire[l0 - 1, c0].ProbabilityCave < 100){
                    n_N = Direction_vers_recursif(lf, cf, l0 - 1, c0, 1);
                }
            }
            if(Memory.PositionExist(l0 + 1, c0, memoire.GetLength(0))){
                if(memoire[l0 + 1, c0].ProbabilityCave < 100){
                    n_S = Direction_vers_recursif(lf, cf, l0 + 1, c0, 1);
                }
            }
            if(Memory.PositionExist(l0, c0 - 1, memoire.GetLength(0))){
                if(memoire[l0, c0 - 1].ProbabilityCave < 100){
                    n_W = Direction_vers_recursif(lf, cf, l0, c0 - 1, 1);
                }
            }
            if(Memory.PositionExist(l0, c0 + 1, memoire.GetLength(0))){
                if(memoire[l0, c0 + 1].ProbabilityCave < 100){
                    n_E = Direction_vers_recursif(lf, cf, l0, c0 + 1, 1);
                }
            }
            //Console.WriteLine(n_N + " " + n_S + " " + n_W + " " + n_E);
            int[] list_n = {n_N, n_S, n_W, n_E};
            if(list_n.Min() == Int32.MaxValue){
                return 'X';
            }
            if(list_n.Min() == n_N){
                return 'N';
            }
            if(list_n.Min() == n_S){
                return 'S';
            }
            if(list_n.Min() == n_W){
                return 'W';
            }
            if(list_n.Min() == n_E){
                return 'E';
            }
            return 'X';
        }

        //permet de trouver un chemin evitant les crevasses (les montres ne sont pas pris en compte)
        public int Direction_vers_recursif(int lf, int cf, int l0, int c0, int n){
            if(!Memory.PositionExist(l0, c0, memoire.GetLength(0)))
                return Int32.MaxValue;
                
            memoire[l0,c0].Passage = n;

            if(l0 == lf && c0 == cf){
                return n;
            }

            int n_N = Int32.MaxValue;
            int n_S = Int32.MaxValue;
            int n_W = Int32.MaxValue;
            int n_E = Int32.MaxValue;

            if(Memory.PositionExist(l0 - 1, c0, memoire.GetLength(0))){
                if(memoire[l0 - 1, c0].ProbabilityCave < 100 && memoire[l0 - 1,c0].Passage > n + 1){
                    n_N = Direction_vers_recursif(lf, cf, l0 - 1, c0, n + 1);
                }
            }
            if(Memory.PositionExist(l0 + 1, c0, memoire.GetLength(0))){
                if(memoire[l0 + 1, c0].ProbabilityCave < 100 && memoire[l0 + 1,c0].Passage > n + 1){
                    n_S = Direction_vers_recursif(lf, cf, l0 + 1, c0, n + 1);
                }
            }
            if(Memory.PositionExist(l0, c0 - 1, memoire.GetLength(0))){
                if(memoire[l0, c0 - 1].ProbabilityCave < 100 && memoire[l0,c0 - 1].Passage > n + 1){
                    n_W = Direction_vers_recursif(lf, cf, l0, c0 - 1, n + 1);
                }
            }
            if(Memory.PositionExist(l0, c0 + 1, memoire.GetLength(0))){
                if(memoire[l0, c0 + 1].ProbabilityCave < 100 && memoire[l0,c0 + 1].Passage > n + 1){
                    n_E = Direction_vers_recursif(lf, cf, l0, c0 + 1, n + 1);
                }
            }

            int[] list_n = {n_N, n_S, n_W, n_E};
            return list_n.Min();
        }

        public void Jeter_pierre(char d, Foret foret){
            score -= 10;
            Console.WriteLine(name + " lance une pierre vers le " + d);
            messages += name + " lance une pierre vers le " + d + Environment.NewLine;
            if(d == 'N'){
                foret.Utilisation_de_roches(pos_l - 1, pos_c);
            }
            if(d == 'S'){
                foret.Utilisation_de_roches(pos_l + 1, pos_c);
            }
            if(d == 'W'){
                foret.Utilisation_de_roches(pos_l, pos_c - 1);
            }
            if(d == 'E'){
                foret.Utilisation_de_roches(pos_l, pos_c + 1);
            }
        }

        public bool Prendre_portail(Case[,] foret)
        {
            return (foret[pos_l, pos_c].Type == CaseType.Portail);
        }

        public int Pos_l{
            get{return pos_l;}
        }
        public int Pos_c{
            get{return pos_c;}
        }

        public string Name{
            get{return name;}
        }

        public int Score{
            get{return score;}
            set{score = value;}
        }
    }
}