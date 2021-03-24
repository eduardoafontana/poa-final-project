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

        private bool existMonsterNeighborhood = true;
        private bool existCaveNeighborhood = true;
        private float countMonsterNeighborhood = 4;// 4/8 <=> 50%
        private float countCaveNeighborhood = 4;// 4/8 <=> 50%

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

        internal bool IsCaseAlreadyExplored()
        {
            return this.AmountOfPassage != 0;
        }

        internal void AnalyzeOdorNeighborhood(float neighborhoodExistOdeur)
        {
            switch (neighborhoodExistOdeur)
            {
                case 0: 
                    this.existMonsterNeighborhood = false;
                    break;
                case 1: 
                    this.countMonsterNeighborhood++;
                    break;
            }
        }

        internal void AnalyzeVentNeighborhood(float neighborhoodExistVent)
        {
            switch (neighborhoodExistVent)
            {
                case 0: 
                    this.existCaveNeighborhood = false;
                    break;
                case 1: 
                    this.countCaveNeighborhood++;
                    break;
            }
        }

        internal void ResetProbabilityVariables()
        {
            this.existMonsterNeighborhood = true;
            this.existCaveNeighborhood = true;
            this.countMonsterNeighborhood = 4;// 4/8 <=> 50%
            this.countCaveNeighborhood = 4;// 4/8 <=> 50%
        }

        internal void CalculateProbabilityMonster()
        {
            this.ProbabilityMonster = this.existMonsterNeighborhood ?  100 * this.countMonsterNeighborhood / 8 : 0;
        }

        internal void CalculateProbabilityCave()
        {
            this.ProbabilityCave = this.existCaveNeighborhood ?  100 * this.countCaveNeighborhood / 8 : 0;
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