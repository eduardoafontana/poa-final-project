using System;

namespace Wumpus.Character
{
    /// <summary>
    /// This class is responsible for maintaining the player's memory regarding a position in the forest.
    /// A forest position means a cell in the forest matrix.
    /// This class aims to calculate and store the probabilities of the monster, crevasse and portal elements referring to neighboring cells.
    /// </summary>
    public class Memory
    {
        public float ProbabilityMonster = -1;
        public float ProbabilityCrevasse = -1;
        public float ProbabilityPortal = -1;
        public float AmountOfPassage = 0;
        public float ExistOdour = -1;
        public float ExistWind = -1;
        public float ExistLuminosity = -1;

        public int DistanceRelative = 0;
        public int Passage = Int32.MaxValue;

        private bool existMonsterNeighbor = true;
        private bool existCrevasseNeighbor = true;
        private float countMonsterNeighbor = 4;// 4/8 <=> 50%
        private float countCrevasseNeighbor = 4;// 4/8 <=> 50%

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

        internal void CalculateLocalProbabilityCrevasse(float probabilityCrevasse)
        {
            this.ProbabilityCrevasse = probabilityCrevasse;
        }

        internal void CheckExistOdor(float odor)
        {
            this.ExistOdour = odor;
        }

        internal void CheckExistWind(float speedWind)
        {
            this.ExistWind = speedWind;
        }

        internal void CheckExistLuminosity(float luminosity)
        {
            this.ExistLuminosity = luminosity;
        }

        internal void CalculateProbabilityPortal()
        {
            switch (this.ExistLuminosity)
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

        internal void AnalyzeOdorNeighbor(float neighborExistOdour)
        {
            switch (neighborExistOdour)
            {
                case 0: 
                    this.existMonsterNeighbor = false;
                    break;
                case 1: 
                    this.countMonsterNeighbor++;
                    break;
            }
        }

        internal void AnalyzeWindNeighbor(float neighborExistWind)
        {
            switch (neighborExistWind)
            {
                case 0: 
                    this.existCrevasseNeighbor = false;
                    break;
                case 1: 
                    this.countCrevasseNeighbor++;
                    break;
            }
        }

        internal void ResetProbabilityVariables()
        {
            this.existMonsterNeighbor = true;
            this.existCrevasseNeighbor = true;
            this.countMonsterNeighbor = 4;// 4/8 <=> 50%
            this.countCrevasseNeighbor = 4;// 4/8 <=> 50%
        }

        internal void CalculateProbabilityMonster()
        {
            this.ProbabilityMonster = this.existMonsterNeighbor ?  100 * this.countMonsterNeighbor / 8 : 0;
        }

        internal void CalculateProbabilityCrevasse()
        {
            this.ProbabilityCrevasse = this.existCrevasseNeighbor ?  100 * this.countCrevasseNeighbor / 8 : 0;
        }
    }
}