﻿using System;
using System.Reflection;

namespace Wumpus.Environment
{
    public class Forest : ForestInterface
    {
        public int Size { get; private set; }
        public Cell[,] Grid { get; private set; }
        public int PlayerSpawnL { get { return playerSpawn[0]; } }
        public int PlayerSpawnC { get { return playerSpawn[1]; } }
        private int[] playerSpawn;
        private Random random = new Random();

        public Forest(int size)
        {
            this.Size = size + GameConfiguration.ForestMinimumDimension;

            this.Grid = new Cell[this.Size, this.Size];

            InitializeGrid();
        }

        public int InitForest()
        {
            //probabilite d'apparition de montre
            int monsterProbability = (int)Math.Truncate(0.2 * this.Size * this.Size);

            //probabilite d'apparition de crevasse
            int crevasseProbability = (int)Math.Truncate(0.15 * this.Size * this.Size);

            PlaceElement(CellType.Crevasse, crevasseProbability);
            PlaceElement(CellType.Monster, monsterProbability);
            PlaceElement(CellType.Portal, 1);

            InitNeighborStatusCell();

            playerSpawn = SelectVoidCellForPlayerInit();

            return this.Size;
        }

        public int InitForestForTests(ForestConfiguration configuration)
        {
            foreach (var position in configuration.MonstersPosition)
            {
                this.Grid[position[0], position[1]].Type = CellType.Monster;
            }

            foreach (var position in configuration.CrevassesPosition)
            {
                this.Grid[position[0], position[1]].Type = CellType.Crevasse;
            }

            this.Grid[configuration.PortalPosition[0], configuration.PortalPosition[1]].Type = CellType.Portal;

            InitNeighborStatusCell();

            playerSpawn = configuration.PlayerPosition;

            return this.Size;
        }

        private int[] SelectVoidCellForPlayerInit()
        {
            int l = 0;
            int c = 0;

            while(Grid[l, c].Type != CellType.Empty)
            {
                l = random.Next(0, Size);
                c = random.Next(0, Size);
            }

            return new int[2] {l, c};
        }

        private void InitNeighborStatusCell()
        {
            for (int l = 0; l < Grid.GetLength(0); l++)
            {
                for (int c = 0; c < Grid.GetLength(1); c++)
                {
                    UpdateNeighborStatusCell(l, c);
                }
            }
        }

        private void PlaceElement(CellType typeElement, int quantityOfElement)
        {
            int i = 0;
            while(i < quantityOfElement)
            {
                int l = random.Next(0, Grid.GetLength(0));
                int c = random.Next(0, Grid.GetLength(1));

                if (Grid[l, c].Type != CellType.Empty)
                    continue;

                Grid[l, c].Type = typeElement;
                i++;
            }
        }

        private void InitializeGrid()
        {
            for (int l = 0; l < Grid.GetLength(0); l++)
            {
                for (int c = 0; c < Grid.GetLength(1); c++)
                {
                    Grid[l, c] = new Cell(CellType.Empty);
                }
            }
        }

        //update la case si un joueur lance une roche
        public void HitMonsterWithStone(int l, int c) 
        {
            if(!IsCellValid(l, c))
                return;

            if(Grid[l, c].Type == CellType.Monster)
                Grid[l, c].Type = CellType.Empty;

            if(IsCellValid(l + 1, c))
                UpdateNeighborStatusCell(l + 1, c);

            if(IsCellValid(l - 1, c))
                UpdateNeighborStatusCell(l - 1, c);

            if(IsCellValid(l, c - 1))
                UpdateNeighborStatusCell(l, c - 1);

            if(IsCellValid(l, c + 1))
                UpdateNeighborStatusCell(l, c + 1);
        }

        private void UpdateNeighborStatusCell(int l, int c) 
        {
            switch (Grid[l, c].Type)
            {
                case CellType.Portal:
                    Grid[l, c].Luminosity = CellLuminosity.Strong;
                    break;
                case CellType.Monster:
                    SetCellStatusByEnumType(new int[] {l, c}, typeof(Cell).GetProperty("Odour"), CellOdour.Bad);
                    break;
                case CellType.Crevasse:
                    SetCellStatusByEnumType(new int[] {l, c}, typeof(Cell).GetProperty("SpeedWind"), CellSpeedWind.Strong);
                    break;
            }
        }

        private void SetCellStatusByEnumType(int [] positions, PropertyInfo enumProperty, Enum enumValue)
        {
            int l = positions[0];
            int c = positions[1];

            if(IsCellValid(l, c + 1))
                enumProperty.SetValue(Grid[l, c + 1], enumValue);

            if(IsCellValid(l, c - 1))
                enumProperty.SetValue(Grid[l, c - 1], enumValue);

            if(IsCellValid(l - 1, c))
                enumProperty.SetValue(Grid[l - 1, c], enumValue);

            if(IsCellValid(l + 1, c))
                enumProperty.SetValue(Grid[l + 1, c], enumValue);

            return;
        }

        private bool IsCellValid(int l, int c)
        {
            if((c >= this.Size) || (c < 0))
                return false;

            if((l < 0) || (l >= this.Size))
                return false;

            return true;
        }

        public override string ToString()
        {
            string r = "\n\nforet magique : empty\nv : crevasse\nM : monster\nO : portal\n\n";
            for(int l = 0; l < Grid.GetLength(0); l++)
            {
                for(int c = 0; c < Grid.GetLength(1); c++)
                {
                    r += Grid[l,c].Type.GetDescription();
                }
                r += "\n";
            }
            r += "\n";

            for(int l = 0; l < Grid.GetLength(0); l++)
            {
                for(int c = 0; c < Grid.GetLength(1); c++)
                {
                    string luminosity = Grid[l,c].Luminosity == CellLuminosity.Strong ? Grid[l,c].Luminosity.GetDescription() : "";
                    string odour = Grid[l,c].Odour == CellOdour.Bad ?  Grid[l,c].Odour.GetDescription() : "";
                    string wind = Grid[l,c].SpeedWind == CellSpeedWind.Strong ? Grid[l,c].SpeedWind.GetDescription() : "";
                    
                    r += " " + luminosity + odour + wind + " |";
                }
                r += "\n";
            }

            return r;
        }
    }
}
