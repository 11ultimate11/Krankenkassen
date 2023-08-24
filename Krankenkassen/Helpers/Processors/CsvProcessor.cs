using Krankenkassen.Helpers.Interfaces;
using Krankenkassen.Models.Interfaces;
using Krankenkassen.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krankenkassen.Helpers.Processors;

public class CsvProcessor : ICsvProcessor
{
    //Spalte 1 und 17 IK
    //Spalte 0 Name
    //Spalte 8 Anschrifft

    /// <summary>
    /// Liest Daten aus einer CSV-Datei, die sich im angegebenen Pfad befindet, und erstellt eine Instanz von ICsvModel.
    /// </summary>
    /// <param name="csvFilePath">Der vollständige Pfad zu der zu lesenden CSV-Datei.</param>
    /// <returns>
    /// Ein Objekt, das die ICsvModel-Schnittstelle implementiert und die aus der CSV-Datei gelesenen Daten enthält,
    /// oder null, wenn die Datei nicht existiert, eine nicht unterstützte Erweiterung hat oder andere Ausnahmen auftreten.
    /// </returns>
    public async Task<ICsvModel> ReadCsvFile(string csvFilePath)
    {
        if (Path.GetExtension(csvFilePath) != ".csv" || !Path.IsPathRooted(csvFilePath) || !File.Exists(csvFilePath)) return null;
        CsvModel model = new(csvFilePath);
        try
        {
            using var stream = new StreamReader(csvFilePath);
            while (!stream.EndOfStream)
            {
                var line = await stream.ReadLineAsync();
                var splits = line.Split(";");
                model.Lines.Add(new CsvLineModel { Line = splits});
            }
            return model;
        }
        catch (Exception ex)
        {
            await MainPage.Instance.DisplayAlert("Error", ex.Message, "Abbrechen");
            return null;
        }
    }
    /// <summary>
    /// Filtert die bereitgestellte Datensammlung basierend auf dem angegebenen Wert und gibt eine neue Sammlung mit gefilterten Ergebnissen zurück.
    /// </summary>
    /// <param name="data">Die ursprüngliche Sammlung von String-Arrays, die gefiltert werden soll.</param>
    /// <param name="value">Der anzuwendende Filterwert.</param>
    /// <returns>
    /// Eine neue ObservableRangeCollection, die die gefilterten String-Arrays basierend auf dem Filterwert enthält.
    /// </returns>
    public ObservableRangeCollection<CsvLineModel> FilterData(ObservableRangeCollection<CsvLineModel> data , string value)
    {
        bool numeric = int.TryParse(value.Trim() , out int number);
        if (!numeric)
        {
            string lower = value.ToLower().Trim();
            var output = from d in data
                          where d.Name.Contains(lower) ||
                         d.Adress.Contains(lower)
                          select d;
            return new ObservableRangeCollection<CsvLineModel>(output);
        }
        else
        {
            string trimmed = value.Trim();
            var output = from d in data where (d.IK.Equals(trimmed) && d.IKverweis.Equals(trimmed)) || d.Line[7].Contains(trimmed) select d;
            return new ObservableRangeCollection<CsvLineModel>(output);
        }
    }
    /// <summary>
    /// Erstellt eine neue CSV-Datei und schreibt eine Sammlung von CsvLineModel-Daten in diese.
    /// </summary>
    /// <param name="data">Die Sammlung von CsvLineModel-Daten, die in die CSV-Datei geschrieben werden soll.</param>
    /// <param name="path">Der Verzeichnispfad, in dem die neue CSV-Datei erstellt werden soll.</param>
    /// <returns>Returnt true, wenn die CSV-Datei erfolgreich erstellt und geschrieben wurde, oder false, wenn nicht.</returns>
    public bool CreateNewCsvFile(IEnumerable<CsvLineModel> data , string path)
    {
        if (data is null || !data.Any() || string.IsNullOrEmpty(path)) return false;
        try
        {
            var newpath = $"newData-copy-{Guid.NewGuid()}.csv";
            using var stream = new StreamWriter(Path.Combine(path , newpath));
            foreach (var line in data)
            {
                stream.WriteLine(line.GetCsvString());
            }
            return true;
        }
        catch (Exception ex)
        {
            MainPage.Instance.DisplayAlert("Error", $"Beim Schreiben der neuen Datei ist ein Fehler aufgetreten.\n{ex.Message}" , "OK");
            return false;
        }
    }
}
