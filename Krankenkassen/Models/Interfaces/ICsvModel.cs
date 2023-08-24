using Krankenkassen.Models.Model;

namespace Krankenkassen.Models.Interfaces
{
    /// <summary>
    /// Dies ist ein Modell, das eine csv-Datei darstellt
    /// </summary>
    public interface ICsvModel
    {
        ObservableRangeCollection<CsvLineModel> Lines { get; set; }
        string FilePath { get; }
        string GetDirectory();
    }
}