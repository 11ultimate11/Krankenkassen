using Krankenkassen.ViewModel;
using Krankenkassen.Views;

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
        public MainPage(MainpageVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
            Instance = this;
            
        }
    }
}