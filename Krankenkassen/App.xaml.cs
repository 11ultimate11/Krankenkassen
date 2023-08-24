using Krankenkassen.Views;

namespace Krankenkassen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute(nameof(InfoPage), typeof(InfoPage));
            //Eintragung der Infoseite in die Navigation
            Shell.Current.GoToAsync(nameof(InfoPage));
        }
    }
}