using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krankenkassen.Models.Interfaces;

namespace Krankenkassen.Models.Model;

public class CsvModel : ICsvModel
{
    public CsvModel(string path)
    {
        this.path = path;
    }
    private readonly string path;

    public string FilePath => path;

    public ObservableRangeCollection<CsvLineModel> Lines { get; set; } = new();
    public string GetDirectory()
    {
        return Path.GetDirectoryName(this.path);
    }
}
