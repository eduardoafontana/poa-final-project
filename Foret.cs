using System;
using System.Linq;

namespace Wumpus
{
    public class Foret 
    {
        private int dim;
        private Case[,] grille;
        private int[] spawn;
        private Random random = new Random();

        //genere un foret donc toutes les cases sans crevasses sont accessibles a n'importe quelle autre casse sasn crevasse
        public Foret(int dim)
        {
            dim++;
            dim++;
            this.dim = dim;
            grille = new Case[dim, dim];
            int nb_monstre = (int)Math.Truncate(0.2 * dim * dim); //probabilite d'apparition de montre
            int nb_crevasse = (int)Math.Truncate(0.15 * dim * dim); //probabilite d'apparition de crevasse

            InitializeGrid();

            PlaceElement(CaseType.Crevasse, nb_crevasse);
            PlaceElement(CaseType.Monstre, nb_monstre);
            PlaceElement(CaseType.Portail, 1);

            //update cases
            for (int l = 0; l < grille.GetLength(0); l++)
            {
                for (int c = 0; c < grille.GetLength(1); c++)
                {
                    int[] coo = { l, c };
                    Update(coo);
                }
            }

            //placer le point d'apparition du joueur
            bool[] case_vide = new bool[dim * dim];
            int nb_case_vide = 0;
            for (int l = 0; l < grille.GetLength(0); l++)
            {
                for (int c = 0; c < grille.GetLength(1); c++)
                {
                    if (grille[l, c].Type == CaseType.Vide)
                    {
                        case_vide[l * dim + c] = true;
                        nb_case_vide++;
                    }
                    else
                    {
                        case_vide[l * dim + c] = false;
                    }
                }
            }

            int case_vide_choisie = random.Next(0, nb_case_vide - 1);

            nb_case_vide = 0;
            for (int i = 0; i < case_vide.GetLength(0); i++)
            {
                if (case_vide[i] == true)
                {
                    if (nb_case_vide == case_vide_choisie)
                    {
                        spawn = new int[] { (int)i / dim, i % dim };
                    }
                    nb_case_vide++;
                }
            }
        }

        private void PlaceElement(CaseType typeElement, int quantityOfElement)
        {
            int i = 0;
            while(i < quantityOfElement)
            {
                int l = random.Next(0, grille.GetLength(0));
                int c = random.Next(0, grille.GetLength(1));

                if (grille[l, c].Type != CaseType.Vide)
                    continue;

                grille[l, c].Type = typeElement;
                i++;
            }
        }

        private void InitializeGrid()
        {
            for (int l = 0; l < grille.GetLength(0); l++)
            {
                for (int c = 0; c < grille.GetLength(1); c++)
                {
                    grille[l, c] = new Case(CaseType.Vide);
                }
            }
        }

        //update la case si un joueur lance une roche
        public void Utilisation_de_roches(int l, int c) 
        {
            int[] coo = {l, c};
            if(0 <= coo[0] && coo[0] < dim && 0 <= coo[1] && coo[1] < dim){
                if(grille[coo[0],coo[1]].Type == CaseType.Monstre){
                    grille[coo[0],coo[1]].Type = CaseType.Vide;
                }
                int[] cooN = {coo[0]+1, coo[1]};
                Update(cooN);
                int[] cooS = {coo[0]-1, coo[1]};
                Update(cooS);
                int[] cooW = {coo[0], coo[1]-1};
                Update(cooW);
                int[] cooE = {coo[0], coo[1]+1};
                Update(cooE);
            }
        }

        //met a jour le vent, les odeurs et les zones lumineuses sur la case
        private void Update(int[] coo) 
        {
            int l = coo[0];
            int c = coo[1];

            if(0 <= l && l < dim && 0 <= c && c < dim){
                if(grille[l,c].Type == CaseType.Portail){
                    grille[l,c].Luminosite = CaseLuminosite.Fort;
                }

                int[,] delta = {{-1, 0}, {0, 1}, {1, 0}, {0, -1}};
                for(int i = 0; i < delta.GetLength(0); i++){

                    if(0 <= l + delta[i,0] && l + delta[i,0] < dim && 0 <= c + delta[i,1] && c + delta[i,1] < dim){
                        
                        if(grille[l + delta[i,0], c + delta[i,1]].Type == CaseType.Monstre){
                            grille[l,c].Odeur = CaseOdeur.Mauvaise;
                        }
                        if(grille[l + delta[i,0], c + delta[i,1]].Type == CaseType.Crevasse){
                            grille[l,c].VitesseVent = CaseVitesseVent.Fort;
                        }
                    }
                }
            }
        }


        public override string ToString()
        {
            string r = "\n\nforet magique : vide\nv : crevasse\nM : monstre\nO : portail\n\n";
            for(int l = 0; l < grille.GetLength(0); l++){
                for(int c = 0; c < grille.GetLength(1); c++){
                    r += grille[l,c].Type.GetDescription();
                }
                r += "\n";
            }
            return r;
        }
        

        public int Spawn_l{
            get{return spawn[0];}
        }


        public int Spawn_c{
            get{return spawn[1];}
        }


        public Case[,] Grille{
            get{return grille;}
        }
    }
}
