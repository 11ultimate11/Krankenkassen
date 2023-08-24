using Krankenkassen.Components;
using Krankenkassen.Models.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Maui.Graphics.Color;

namespace Krankenkassen.DataTemplates;

public class Template : DataTemplate
{
    public Template(CsvLineModel data) : base(()=> CreateItem(data))
    {
        
    }
    /// <summary>
    /// Einrichten einer Ansicht für den Selektor
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    static View CreateItem(CsvLineModel data) => new CsvLineComponent(data);
}
