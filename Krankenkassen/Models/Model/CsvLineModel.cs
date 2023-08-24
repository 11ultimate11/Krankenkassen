using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krankenkassen.Models.Model;

public class CsvLineModel
{
    public string[] Line { get; set; }
    public string Name => Line?[0] is null ? string.Empty : Line[0].ToLower().Trim();
    public string Adress => !CheckForFieldsAdress() ? string.Empty : $"{Line[7].Trim()} {Line[8].Trim()} {Line[9].Trim()}".ToLower().Trim();
    public string IK => Line?[1] is null ? string.Empty : Line[1].Trim();
    public string IKverweis => Line?[17] is null ? string.Empty : Line[17].Trim();

    private bool CheckForFieldsAdress()
    {
        return Line?[7] != null && Line?[8] != null && Line?[9] != null;
    }
    public string GetCsvString()
    {
        var output = string.Empty;
        if (Line?.Length == 0) return output;
        Line.ToList().ForEach(item =>
        {
            output += $"{item};";
        });
        return output;
    }
}
