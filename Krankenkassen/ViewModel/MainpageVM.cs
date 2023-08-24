
using CommunityToolkit.Mvvm.ComponentModel;
using Krankenkassen.Helpers.Interfaces;
using Krankenkassen.Models.Interfaces;
using Krankenkassen.Models.Model;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Krankenkassen.ViewModel;

public class MainpageVM : BaseViewModel
{
    private readonly ICsvProcessor _csv;
    private ITimerProcessor _timer;
    public MainpageVM(ICsvProcessor _csv , ITimerProcessor _timer)
    {
        //Einstellung der Abhängigkeit
        this._csv = _csv;
        this._timer = _timer;
        //Einstellung der Befehle
        SelectFileCommand = new AsyncCommand(SelectFile);
        DeleteCommand = new MvvmHelpers.Commands.Command(Delete);
        CreateCommand = new MvvmHelpers.Commands.Command(Create);
        //Anmeldung beim Timer-Delegaten
        _timer.TimeExpiredCallback +=AppSettings_TimeExpiredCallback;
    }


    private ICsvModel model;
    public ObservableRangeCollection<CsvLineModel> Lines { get; set; } = new();
    public ObservableRangeCollection<object> SelectedItems { get; set; } = new();
    #region Commands
    public AsyncCommand SelectFileCommand { get; set; }
    public MvvmHelpers.Commands.Command DeleteCommand { get; set; }
    public MvvmHelpers.Commands.Command CreateCommand { get; set; }
    #endregion
    #region Bindable Properties
    private string filePath;
    /// <summary>
    /// Der Pfad der zu ladenden Datei
    /// </summary>
    public string FilePath
    {
        get => filePath;
        set
        {
            filePath = value;
            OnPropertyChanged(nameof(FilePath));
        }
    }
    private string search;

    /// <summary>
    /// User input
    /// </summary>
    public string Search
    {
        get { return search; }
        set
        {
            search = value;
            OnPropertyChanged(nameof(Search));
            InvokeTimer();
        }
    }

    #endregion
    #region Methods
    /// <summary>
    /// Diese Methode wird verwendet, um eine neue csv-Datei zu laden
    /// </summary>
    /// <returns></returns>
    private async Task SelectFile()
    {
        var file = await FilePicker.PickAsync();
        if (file is null) return;
        //Aktualisierung der Benutzeroberfläche
        MainThread.BeginInvokeOnMainThread(() =>
        {
            FilePath = file.FullPath;
        });
        //Anzeige einer Warnung bei ungültiger Datei
        if (Path.GetExtension(file.FullPath) != ".csv")
        {
            await MainPage.Instance.DisplayAlert("Error", "Ausgewähltes Dateiformat ist nicht erlaubt, nur csv-Dateien", "OK");
            return;
        }
        //Lesen der neu ausgewählten Datei
        var read = await _csv.ReadCsvFile(FilePath);
        if(read != null)
        {
            model = read;
            Lines = model.Lines;
            OnPropertyChanged(nameof(Lines));
        }
    }
    /// <summary>
    /// Diese Methode wird verwendet, um einen Timer zu starten / zu aktualisieren, wenn sich die Benutzereingabe ändert. Dies hilft
    /// , um die Verzögerung der Benutzeroberfläche zu reduzieren, wenn der Benutzer zu schnell tippt.
    /// </summary>
    private void InvokeTimer()
    {
        _timer.Start();
    }
    /// <summary>
    /// Diese Methode ist für die Filterung von Daten durch Benutzereingaben zuständig
    /// </summary>
    private void Filter()
    {
        if (model?.Lines is null) return;
        if (string.IsNullOrEmpty(Search))
        {
            Lines = model.Lines;
        }
        Lines = _csv.FilterData(model.Lines, Search);
        OnPropertyChanged(nameof(Lines));
    }

    #endregion
    #region Delegate
    /// <summary>
    /// Diese Methode löst die Methode Filter aus, nachdem die Benutzereingabe beendet wurde
    /// </summary>
    private void AppSettings_TimeExpiredCallback()
    {
        Filter();
    }
    private async void Delete()
    {
        if(SelectedItems is not null && SelectedItems.Any())
        {
            var select = SelectedItems.Cast<CsvLineModel>();
            var result = await MainPage.Instance.DisplayAlert("Dateien löschen", $"Sind Sie sicher, dass Sie {select.Count()} Datei/en löschen wollen?", "Ja", "Nein");
            if (!result) return;
            SelectedItems = new();
            OnPropertyChanged(nameof(SelectedItems));
            model.Lines.RemoveRange(select);
            Lines.RemoveRange(select);
        }
    }
    private void Create()
    {
        if (SelectedItems is not null && SelectedItems.Any())
        {
            var select = SelectedItems.Cast<CsvLineModel>();
            var result = _csv.CreateNewCsvFile(select , model.GetDirectory());
            MainPage.Instance.DisplayAlert(result ? "Succes" : "Sorry", result ? "Datei wurde erfolgreich geschrieben" : "Die Datei konnte nicht geschrieben werden", "OK");
        }
    }

    #endregion

}
