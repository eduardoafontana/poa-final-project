using System;
using System.Collections.Generic;
using System.Linq;

namespace Wumpus.Character
{
    /// <summary>
    /// This class is responsible for the player's logic.
    /// This class aims to process and store the player's memory in relation to the forest.
    /// As well as calculating the next cell that the player must move to based on its memory.
    /// Processing the memory means observing the forest, calculating the probability of the elements arrund and updating the player's internal memory represented by the grid Memory.
    /// </summary>
    public class Player : PlayerInterface
    {
        private Memory[,] forestMemory;
        private Memory memoryPlayerPosition;
        private int[] playerPosition;

        /// <summary>
        /// Player builder, initializes the player's memory from the size of the forest.
        /// </summary>
        public Player(int forestDimension)
        {
            forestMemory = new Memory[forestDimension, forestDimension];

            for(int l = 0; l < forestDimension; l++)
            {
                for(int c = 0; c < forestDimension; c++)
                {
                    forestMemory[l,c] = new Memory(l, c);
                }
            }
        }

        /// <summary>
        /// This method is the player's move.
        /// The move consists of looking at the forest, updating the internal memory and reflecting on the memory to decide which position it should move to.
        /// </summary>
        public ExplorerNode Play()
        {
            ObserveAndMemorizeAllForest();
            
            ExplorerNode node = Reflexion();

            return node;
        }

        public void ObserveAndMemorizeCurrentPosition(CellMemory cellMemory)
        { 
            memoryPlayerPosition.CalculateLocalProbabilityMonster(cellMemory.ProbabilityMonster);
            memoryPlayerPosition.CalculateLocalProbabilityCrevasse(cellMemory.ProbabilityCrevasse);
            
            memoryPlayerPosition.CheckExistOdor(cellMemory.ExistOdour);
            memoryPlayerPosition.CheckExistWind(cellMemory.ExistWind);
            memoryPlayerPosition.CheckExistLuminosity(cellMemory.ExistLuminosity);
        }

        /// <summary>
        /// Update the knowledge of the player related to forest.
        /// The memory of the player is attributed to each cell of the forest a list of values:
        /// 3 for the probability of having either a monster, a crevasse, or the portal;
        /// 1 value for the number of times the player passes the cell;
        /// 3 to know the state of the sensors on the box (light, odour, wind).
        /// </summary>
        private void ObserveAndMemorizeAllForest()
        {
            forestMemory.Cast<Memory>().ToList().ForEach(itemMemory => itemMemory.CalculateProbabilityPortal());

            forestMemory.Cast<Memory>()
            .Where(itemMemory => itemMemory.IsCellIsNotExplored())
            .ToList().ForEach(itemMemory => {
                itemMemory.ResetProbabilityVariables();

                Explorer.GetInstance().OnNeighbors()
                .Where(neighbor => neighbor.IsValidPosition(neighbor.GetLine(itemMemory.Line), neighbor.GetColumn(itemMemory.Column), forestMemory.GetLength(0)))
                .ToList()
                .ForEach(neighbor => {
                    itemMemory.AnalyzeOdorNeighbor(forestMemory[neighbor.GetLine(itemMemory.Line), neighbor.GetColumn(itemMemory.Column)].ExistOdour);
                    itemMemory.AnalyzeWindNeighbor(forestMemory[neighbor.GetLine(itemMemory.Line), neighbor.GetColumn(itemMemory.Column)].ExistWind);
                });
                
                itemMemory.CalculateProbabilityMonster();
                itemMemory.CalculateProbabilityCrevasse();
            });
        }

        /// <summary>
        /// Check if the cell to be moved has a monster to throw a stone.
        /// </summary>
        public bool NeedThrowStone(ExplorerNode d)
        {
            return forestMemory[d.GetLine(playerPosition[0]), d.GetColumn(playerPosition[1])].ProbabilityMonster > 0;
        }

        /// <summary>
        /// Determine the most likely cell to contain the portal.
        /// </summary>
        private ExplorerNode Reflexion()
        {
            //Memory scan to find max probable portal.
            float proba_portal_max = forestMemory.Cast<Memory>().Max(x => x.ProbabilityPortal);

            //Memory scan to find min probable crevasse for max probable portal.
            float proba_crevasse_min = 100;
            proba_crevasse_min = forestMemory.Cast<Memory>().Where(x => x.ProbabilityPortal == proba_portal_max).Min(x => x.ProbabilityCrevasse);

            //Calculate the distance of each portal cell with the most strong probable.
            forestMemory.Cast<Memory>().ToList().ForEach(item => item.DistanceRelative = GetCellNearBy(item.Line, item.Column));

            memoryPlayerPosition.DistanceRelative = 0;
            
            //Find the lowest distance with the longest probable portal and min probable crevasse.
            int case_la_plus_proche = Int32.MaxValue;
            case_la_plus_proche = forestMemory.Cast<Memory>().Where(x => x.ProbabilityPortal == proba_portal_max && x.ProbabilityCrevasse == proba_crevasse_min).Min(x => x.DistanceRelative);

            //Find a box to go.
            Memory cellToGo = forestMemory.Cast<Memory>().OrderBy(x => x.Line).ThenBy(x => x.Column).LastOrDefault(x => x.ProbabilityPortal == proba_portal_max && x.ProbabilityCrevasse == proba_crevasse_min && x.DistanceRelative == case_la_plus_proche);

            //if it is our case, take the portal.
            if(cellToGo == memoryPlayerPosition)
                return new ExplorerNode(NodeDirection.Portal, 0);

            //Otherwise go to the cell.
            return GetDirectionToGoTo(cellToGo.Line, cellToGo.Column);
        }

        /// <summary>
        /// Update player position on the grid.
        /// Adds 1 to the number of passages on this cell in the player's memory.
        /// </summary>
        public int[] UpdatePlayerPosition(int l, int c)
        {
            memoryPlayerPosition = forestMemory[l, c];
            memoryPlayerPosition.AmountOfPassage++; 
            playerPosition = new int[] {l, c};

            return playerPosition;
        }

        /// <summary>
        /// Returns the number of cells closest to the objective located in [lf, cf].
        /// </summary>
        private int GetCellNearBy(int lf, int cf)
        {
            forestMemory.Cast<Memory>().ToList().ForEach(item => item.Passage = Int32.MaxValue);

            memoryPlayerPosition.Passage = 0;
            
            List<int> list_n = Explorer.GetInstance().OnNeighbors()
            .Where(item => item.IsValidPosition(item.GetLine(playerPosition[0]), item.GetColumn(playerPosition[1]), forestMemory.GetLength(0)))
            .Select(item => 
                DeepGetDirectionToGoTo(new int[] { lf, cf }, new int[] { item.GetLine(playerPosition[0]), item.GetColumn(playerPosition[1]) }, 1)
            ).ToList();

            return list_n.Min();
        }

        /// <summary>
        /// Returns the direction to go to the objective located in [lf, cf].
        /// </summary>
        private ExplorerNode GetDirectionToGoTo(int lf, int cf)
        {
            int l0 = playerPosition[0];
            int c0 = playerPosition[1];

            forestMemory.Cast<Memory>().ToList().ForEach(x => x.Passage = Int32.MaxValue);

            memoryPlayerPosition.Passage = 0;

            List<ExplorerNode> listMemories = Explorer.GetNeighborsFiltered(forestMemory, new int[] {l0, c0})
            .Select(item => 
                new ExplorerNode(item.GetLine(), item.GetColumn(), item.Direction, DeepGetDirectionToGoTo(new int[] { lf, cf }, new int[] { item.GetLine(l0), item.GetColumn(c0) }, 1))
            ).ToList();

            if(listMemories.Count() == 0)
                return new ExplorerNode(NodeDirection.NotFound, 0);

            if(listMemories.Min().Distance == Int32.MaxValue)
                return new ExplorerNode(NodeDirection.NotFound, 0);
            else
                return listMemories.Min();
        }

        /// <summary>
        /// Allows you to find a path avoiding crevasses (monsters are not taken into account).
        /// </summary>
        private int DeepGetDirectionToGoTo(int[] origin, int[] destination, int countDistance)
        {
            int lf = origin[0];
            int cf = origin[1];
            int l0 = destination[0];
            int c0 = destination[1];

            forestMemory[l0,c0].Passage = countDistance;

            if(l0 == lf && c0 == cf)
                return countDistance;

            List<int> list_n = Explorer.GetNeighborsFiltered(forestMemory, new int[] {l0, c0})
            .Where(x => forestMemory[x.GetLine(l0), x.GetColumn(c0)].Passage > countDistance + 1)
            .Select(item => 
                DeepGetDirectionToGoTo(new int[] { lf, cf }, new int[] { item.GetLine(l0), item.GetColumn(c0) }, countDistance + 1)
            ).ToList();

            return list_n.Count() > 0 ? list_n.Min() : Int32.MaxValue;
        }
    }
}