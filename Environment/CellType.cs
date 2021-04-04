using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wumpus.Environment
{
    /// <summary>
    /// This class is responsible for defining the possible types that a cell in the forest can be.
    /// In addition, this class contains an extension method for the native Enum class, which allows you to capture the description attribute of each enumerator.
    /// </summary>
    public enum CellType
    {
        [Description("O")]
        Portal,
        [Description("M")]
        Monster,
        [Description("V")]
        Crevasse,
        [Description(".")]
        Empty
    }

    /// <summary>
    /// Extension method to obtain the description attribute of the enumerator. Uses reflection.
    /// </summary>
    public static class CellTypeExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
