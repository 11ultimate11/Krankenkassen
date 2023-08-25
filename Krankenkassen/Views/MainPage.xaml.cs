using Krankenkassen.Helpers.Processors;
using Krankenkassen.PlatformInterfaces;
using Krankenkassen.ViewModel;
using Krankenkassen.Views;
using System.Diagnostics;

namespace Krankenkassen
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Instanz der Hauptseite, um auf die von der Inhaltsseite abgeleitete Funktion zuzugreifen
        /// <br></br>
        /// Diese Instanz darf nur ein einziges Mal zur Laufzeit instanziiert werden
        /// </summary>
        public static MainPage Instance { get; set; }
        //private IPrintProcessor _printer;
        public MainPage(MainpageVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
            Instance = this;
            //_printer = _print;

        }

    }
}