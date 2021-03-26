using System;
using System.Linq;

namespace Wumpus
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

        internal static bool PositionExist(int l, int c, int size)
        {
            int limitRight = size;
            int limitLeft = 0;
            int limitTop = 0;
            int limitDown = size;

            if(c >= limitRight)
                return false;

            if(c < limitLeft)
                return false;

            if(l < limitTop)
                return false;

            if(l >= limitDown)
                return false;

            return true;
        }

        internal void CalculateLocalProbabilityMonster(CaseType type)
        {
            this.ProbabilityMonster = type == CaseType.Monstre ? 100 : 0;
        }

        internal void CalculateLocalProbabilityCave(CaseType type)
        {
            this.ProbabilityCave = type == CaseType.Crevasse ? 100 : 0;
        }

        internal void CheckExistOdor(CaseOdeur odor)
        {
            this.ExistOdeur = odor == CaseOdeur.Mauvaise ? 1 : 0;
        }

        internal void CheckExistVent(CaseVitesseVent vitesseVent)
        {
            this.ExistVent = vitesseVent == CaseVitesseVent.Fort ? 1 : 0;
        }

        internal void CheckExistLuminosite(CaseLuminosite luminosite)
        {
            this.ExistLuminosite = luminosite == CaseLuminosite.Fort ? 1 : 0;
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

        internal bool IsCaseIsNotExplored()
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

        //TODO remove later
        // memoire[l,c,0] = -1; //proba montre
        // memoire[l,c,1] = -1; //proba crevasse
        // memoire[l,c,2] = -1; //proba portail
        // memoire[l,c,3] = 0;  //nb passage
        // memoire[l,c,4] = -1; //odeur
        // memoire[l,c,5] = -1; //vent
        // memoire[l,c,6] = -1; //luminosite
    }
}