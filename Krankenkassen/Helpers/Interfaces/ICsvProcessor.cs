using Krankenkassen.Models.Interfaces;
using Krankenkassen.Models.Model;

namespace Krankenkassen.Helpers.Interfaces
{
    /// <summary>
    /// Dies ist eine Schnittstelle, die zur Verarbeitung und Filterung von CSV-Dateien verwendet wird.
    /// </summary>
    public interface ICsvProcessor
    {
        /// <summary>
        /// Liest Daten aus einer CSV-Datei, die sich im angegebenen Pfad befindet, und erstellt eine Instanz von ICsvModel.
        /// </summary>
        /// <param name="csvFilePath">Der vollständige Pfad zu der zu lesenden CSV-Datei.</param>
        /// <returns>
        /// Ein Objekt, das die ICsvModel-Schnittstelle implementiert und die aus der CSV-Datei gelesenen Daten enthält,
        /// oder null, wenn die Datei nicht existiert, eine nicht unterstützte Erweiterung hat oder andere Ausnahmen auftreten.
        /// </returns>
        Task<ICsvModel> ReadCsvFile(string csvFilePath);
        /// <summary>
        /// Filtert die bereitgestellte Datensammlung basierend auf dem angegebenen Wert und gibt eine neue Sammlung mit gefilterten Ergebnissen zurück.
        /// </summary>
        /// <param name="data">Die ursprüngliche Sammlung von String-Arrays, die gefiltert werden soll.</param>
        /// <param name="value">Der anzuwendende Filterwert.</param>
        /// <returns>
        /// Eine neue ObservableRangeCollection, die die gefilterten String-Arrays basierend auf dem Filterwert enthält.
        /// </returns>
        ObservableRangeCollection<CsvLineModel> FilterData(ObservableRangeCollection<CsvLineModel> data, string value);
        /// <summary>
        /// Erstellt eine neue CSV-Datei und schreibt eine Sammlung von CsvLineModel-Daten in diese.
        /// </summary>
        /// <param name="data">Die Sammlung von CsvLineModel-Daten, die in die CSV-Datei geschrieben werden soll.</param>
        /// <param name="path">Der Verzeichnispfad, in dem die neue CSV-Datei erstellt werden soll.</param>
        /// <returns>Returnt true, wenn die CSV-Datei erfolgreich erstellt und geschrieben wurde, oder false, wenn nicht.</returns>
        Task<bool> CreateNewCsvFileAsync(IEnumerable<CsvLineModel> data, string path);
        Task<bool> SaveFileAsync(ICsvModel model);
    }
}