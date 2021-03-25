using System;
using System.Linq;
using System.Reflection;

namespace Wumpus
{
    public class Foret 
    {
        public int Size { get; private set; }
        public Case[,] Grille { get; private set; }
        public int PlayerSpawnL { get { return spawn[0]; } }
        public int PlayerSpawnC { get { return spawn[1]; } }
        private int[] spawn;
        private Random random = new Random();

        public Foret(int size)
        {
            this.Size = size;
            this.Size++;
            this.Size++;
            this.Grille = new Case[this.Size, this.Size];

            InitializeGrid();
        }

        public void InitForest()
        {
            int nb_monstre = (int)Math.Truncate(0.2 * this.Size * this.Size); //probabilite d'apparition de montre
            int nb_crevasse = (int)Math.Truncate(0.15 * this.Size * this.Size); //probabilite d'apparition de crevasse

            PlaceElement(CaseType.Crevasse, nb_crevasse);
            PlaceElement(CaseType.Monstre, nb_monstre);
            PlaceElement(CaseType.Portail, 1);

            InitNeighborhoodStatusCase();

            spawn = SelectVoidCaseForPlayerInit();
        }

        public void InitForestForTests(ForestConfiguration configuration)
        {
            foreach (var position in configuration.MonstersPosition)
            {
                this.Grille[position[0], position[1]].Type = CaseType.Monstre;
            }

            foreach (var position in configuration.CavesPosition)
            {
                this.Grille[position[0], position[1]].Type = CaseType.Crevasse;
            }

            this.Grille[configuration.PortalPosition[0], configuration.PortalPosition[1]].Type = CaseType.Portail;

            InitNeighborhoodStatusCase();

            spawn = configuration.PlayerPosition;
        }

        private int[] SelectVoidCaseForPlayerInit()
        {
            int l = 0;
            int c = 0;

            while(Grille[l, c].Type != CaseType.Vide)
            {
                l = random.Next(0, Size);
                c = random.Next(0, Size);
            }

            return new int[2] {l, c};
        }

        private void InitNeighborhoodStatusCase()
        {
            for (int l = 0; l < Grille.GetLength(0); l++)
            {
                for (int c = 0; c < Grille.GetLength(1); c++)
                {
                    int[] coo = { l, c };
                    UpdateNeighborhoodStatusCase(coo);
                }
            }
        }

        private void PlaceElement(CaseType typeElement, int quantityOfElement)
        {
            int i = 0;
            while(i < quantityOfElement)
            {
                int l = random.Next(0, Grille.GetLength(0));
                int c = random.Next(0, Grille.GetLength(1));

                if (Grille[l, c].Type != CaseType.Vide)
                    continue;

                Grille[l, c].Type = typeElement;
                i++;
            }
        }

        private void InitializeGrid()
        {
            for (int l = 0; l < Grille.GetLength(0); l++)
            {
                for (int c = 0; c < Grille.GetLength(1); c++)
                {
                    Grille[l, c] = new Case(CaseType.Vide);
                }
            }
        }

        //update la case si un joueur lance une roche
        public void Utilisation_de_roches(int l, int c) 
        {
            int[] coo = {l, c};
            if(0 <= coo[0] && coo[0] < Size && 0 <= coo[1] && coo[1] < Size){
                if(Grille[coo[0],coo[1]].Type == CaseType.Monstre){
                    Grille[coo[0],coo[1]].Type = CaseType.Vide;
                }

                int[] cooN = {coo[0]+1, coo[1]};
                if(Memory.PositionExist(coo[0]+1, coo[1], Grille.GetLength(0))) //TODO
                    UpdateNeighborhoodStatusCase(cooN);

                int[] cooS = {coo[0]-1, coo[1]};
                if(Memory.PositionExist(coo[0]-1, coo[1], Grille.GetLength(0))) //TODO
                    UpdateNeighborhoodStatusCase(cooS);

                int[] cooW = {coo[0], coo[1]-1};
                if(Memory.PositionExist(coo[0], coo[1]-1, Grille.GetLength(0))) //TODO
                    UpdateNeighborhoodStatusCase(cooW);

                int[] cooE = {coo[0], coo[1]+1};
                if(Memory.PositionExist(coo[0], coo[1]+1, Grille.GetLength(0))) //TODO
                    UpdateNeighborhoodStatusCase(cooE);
            }
        }

        private void UpdateNeighborhoodStatusCase(int[] coo) 
        {
            int l = coo[0];
            int c = coo[1];

            switch (Grille[l,c].Type)
            {
                case CaseType.Portail:
                    Grille[l,c].Luminosite = CaseLuminosite.Fort;
                    break;
                case CaseType.Monstre:
                    SetCaseStatusByEnumType(l, c, typeof(Case).GetProperty("Odeur"), CaseOdeur.Mauvaise);
                    break;
                case CaseType.Crevasse:
                    SetCaseStatusByEnumType(l, c, typeof(Case).GetProperty("VitesseVent"), CaseVitesseVent.Fort);
                    break;
            }
        }

        private void SetCaseStatusByEnumType(int l, int c, PropertyInfo enumProperty, Enum enumValue)
        {
            int limitRight = Size - 1;
            int limitLeft = 0;
            int limitTop = 0;
            int limitDown = Size - 1;

            if(c + 1 <= limitRight)
                enumProperty.SetValue(Grille[l, c + 1], enumValue);

            if(c - 1 >= limitLeft)
                enumProperty.SetValue(Grille[l, c - 1], enumValue);

            if(l - 1 >= limitTop)
                enumProperty.SetValue(Grille[l - 1, c], enumValue);

            if(l + 1 <= limitDown)
                enumProperty.SetValue(Grille[l + 1, c], enumValue);

            return;
        }

        public override string ToString()
        {
            string r = "\n\nforet magique : vide\nv : crevasse\nM : monstre\nO : portail\n\n";
            for(int l = 0; l < Grille.GetLength(0); l++){
                for(int c = 0; c < Grille.GetLength(1); c++){
                    r += Grille[l,c].Type.GetDescription();
                }
                r += "\n";
            }
            r += "\n";

            for(int l = 0; l < Grille.GetLength(0); l++){
                for(int c = 0; c < Grille.GetLength(1); c++){
                    string luminosite = Grille[l,c].Luminosite == CaseLuminosite.Fort ? Grille[l,c].Luminosite.GetDescription() : "";
                    string odeur = Grille[l,c].Odeur == CaseOdeur.Mauvaise ?  Grille[l,c].Odeur.GetDescription() : "";
                    string vent = Grille[l,c].VitesseVent == CaseVitesseVent.Fort ? Grille[l,c].VitesseVent.GetDescription() : "";
                    
                    r += " " + luminosite + odeur + vent + " |";
                }
                r += "\n";
            }

            return r;
        }
    }
}
