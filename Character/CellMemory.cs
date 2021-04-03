namespace Wumpus.Character
{
    /// <summary>
    /// This class represents a piece of the character's memory.
    /// This class is responsible for being a data object with memory information.
    /// </summary>
    public class CellMemory
    {
        public float ProbabilityMonster { get; set; }
        public float ProbabilityCrevasse { get; set; }
        public float ExistOdour { get; set; }
        public float ExistWind { get; set; }
        public float ExistLuminosity { get; set; }
    }
}
