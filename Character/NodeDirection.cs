using System.ComponentModel;

namespace Wumpus.Character
{
    /// <summary>
    /// This class is responsible for storing the information of a direction to a cell in the forest
    /// </summary>
    public enum NodeDirection
    {
        [Description("N")]
        North,
        [Description("S")]
        South,
        [Description("E")]
        East,
        [Description("W")]
        West,
        [Description("P")]
        Portal,
        [Description("X")]
        NotFound,
    }
}
