using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krankenkassen.Global;

/// <summary>
/// Globale Parameter für die Anwendung festlegen
/// </summary>
public static class AppSettings
{
    /// <summary>
    /// Die Zeitspanne, die gewartet werden soll, nachdem der Benutzer aufgehört hat zu tippen
    /// </summary>
    public static int WaitTime => 500;
    /// <summary>
    /// Tick time
    /// </summary>
    public static int Interval => 50;
    
}
