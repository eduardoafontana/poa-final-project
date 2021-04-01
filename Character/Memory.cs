using System;
using System.Linq;
using Wumpus.Environment;

namespace Wumpus.Character
{
    public class Memory
    {
        public float ProbabilityMonster = -1;
        public float ProbabilityCave = -1;
        public float ProbabilityPortal = -1;
        public float AmountOfPassage = 0;
        public float ExistOdeur = -1;
        public float ExistVent = -1;
        public float ExistLuminosite = -1;

        public int DistanceRelative = 0;
        public int Passage = Int32.MaxValue;

        private bool existMonsterNeighbor = true;
        private bool existCaveNeighbor = true;
        private float countMonsterNeighbor = 4;// 4/8 <=> 50%
        private float countCaveNeighbor = 4;// 4/8 <=> 50%

        public int Line { get; private set; }
        public int Column { get; private set; }

        public Memory(int line, int column)
        {
            this.Line = line;
            this.Column = column;
        }

        internal void CalculateLocalProbabilityMonster(float probabilityMonster)
        {
            this.ProbabilityMonster = probabilityMonster;
        }

        internal void CalculateLocalProbabilityCave(float probabilityCave)
        {
            this.ProbabilityCave = probabilityCave;
        }

        internal void CheckExistOdor(float odor)
        {
            this.ExistOdeur = odor;
        }

        internal void CheckExistVent(float vitesseVent)
        {
            this.ExistVent = vitesseVent;
        }

        internal void CheckExistLuminosite(float luminosite)
        {
            this.ExistLuminosite = luminosite;
        }

        internal void CalculateProbabilityPortal()
        {
            switch (this.ExistLuminosite)
            {
                case -1: 
                    this.ProbabilityPortal = 50; 
                    break;
                case 0: 
                    this.ProbabilityPortal = 0; 
                    break;
                case 1: 
                    this.ProbabilityPortal = 100; 
                    break;
            }
        }

        internal bool IsCellIsNotExplored()
        {
            return this.AmountOfPassage == 0;
        }

        internal void AnalyzeOdorNeighbor(float neighborExistOdeur)
        {
            switch (neighborExistOdeur)
            {
                case 0: 
                    this.existMonsterNeighbor = false;
                    break;
                case 1: 
                    this.countMonsterNeighbor++;
                    break;
            }
        }

        internal void AnalyzeVentNeighbor(float neighborExistVent)
        {
            switch (neighborExistVent)
            {
                case 0: 
                    this.existCaveNeighbor = false;
                    break;
                case 1: 
                    this.countCaveNeighbor++;
                    break;
            }
        }

        internal void ResetProbabilityVariables()
        {
            this.existMonsterNeighbor = true;
            this.existCaveNeighbor = true;
            this.countMonsterNeighbor = 4;// 4/8 <=> 50%
            this.countCaveNeighbor = 4;// 4/8 <=> 50%
        }

        internal void CalculateProbabilityMonster()
        {
            this.ProbabilityMonster = this.existMonsterNeighbor ?  100 * this.countMonsterNeighbor / 8 : 0;
        }

        internal void CalculateProbabilityCave()
        {
            this.ProbabilityCave = this.existCaveNeighbor ?  100 * this.countCaveNeighbor / 8 : 0;
        }
    }
}