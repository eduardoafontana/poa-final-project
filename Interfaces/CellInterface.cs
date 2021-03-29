using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Wumpus.Environment;

namespace Wumpus.Interfaces
{
    public interface CellInterface
    {
        CellOdeur Odeur { get; set; }
        CellVitesseVent VitesseVent { get; set; }
        CellLuminosite Luminosite { get; set; }
        CellType Type { get; set; }
    }
}
