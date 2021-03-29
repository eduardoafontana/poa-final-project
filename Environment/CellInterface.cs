using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wumpus.Environment
{
    public interface CellInterface
    {
        CellOdeur Odeur { get; set; }
        CellVitesseVent VitesseVent { get; set; }
        CellLuminosite Luminosite { get; set; }
        CellType Type { get; set; }
    }
}
