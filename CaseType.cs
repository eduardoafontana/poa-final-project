using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wumpus
{
    public enum CaseType
    {
        [Description("O")]
        Portail,
        [Description("M")]
        Monstre,
        [Description("V")]
        Crevasse,
        [Description(".")]
        Vide
    }

    public static class CaseTypeExtensions
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
