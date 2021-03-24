using System;

namespace Wumpus
{
    public class Partie
    {
        private int score;
        private int niveau;
        private Foret foret_magique;
        public Joueur joueur;

        public String messages = String.Empty;

        public Partie(int niveau){
            score = 0;
            this.niveau = niveau;

            foret_magique = new Foret(niveau);
            foret_magique.InitForest();

            joueur = new Joueur("Bob", 2 + niveau);
        }

        public Partie(int niveau, ForestConfiguration forestConfiguration){
            score = 0;
            this.niveau = niveau;
            
            foret_magique = new Foret(niveau);
            foret_magique.InitForestForTests(forestConfiguration);

            joueur = new Joueur("Bob", 2 + niveau);
        }

        public int Jouer(){
            Console.WriteLine(foret_magique);
            messages += foret_magique + Environment.NewLine;

            bool partie_en_cours = true;

            do{
                joueur.Placer(foret_magique.PlayerSpawnL, foret_magique.PlayerSpawnC);
                bool joueur_en_vie = true;

                Console.WriteLine(joueur.Name + " est apparu en case [" + joueur.Pos_l + "," + joueur.Pos_c + "]");
                messages += joueur.Name + " est apparu en case [" + joueur.Pos_l + "," + joueur.Pos_c + "]" + Environment.NewLine;
                do{
                    partie_en_cours = !joueur.Jouer(foret_magique);
                    joueur_en_vie = Etat_Joueur();
                }while(joueur_en_vie && partie_en_cours);
                
                if(joueur_en_vie == false){
                    Console.WriteLine(joueur.Name + " est mort");
                    messages += joueur.Name + " est mort" + Environment.NewLine;

                    joueur.ObserveAndMemorizeCurrentPosition(foret_magique.Grille); // Review this call later
                    joueur.Score -= (niveau + 2) * (niveau + 2) * 10; 
                }
            }while(partie_en_cours);
            joueur.Score += (niveau + 2) * (niveau + 2) * 10; 

            return joueur.Score;
        }

        public bool Etat_Joueur(){
            CaseType type_case_j = foret_magique.Grille[joueur.Pos_l, joueur.Pos_c].Type;
            if(type_case_j == CaseType.Monstre || type_case_j == CaseType.Crevasse){
                return false;
            }
            return true;
        }
    }
}