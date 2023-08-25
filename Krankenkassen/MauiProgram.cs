
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Krankenkassen.Helpers.Interfaces;
using Krankenkassen.Helpers.Processors;
using Krankenkassen.PlatformInterfaces;
using Krankenkassen.ViewModel;
using Krankenkassen.Views;
using Microsoft.Extensions.Logging;

namespace Krankenkassen
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCore()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // Registrierung der notwendigen Abhängigkeit
            builder.Services.AddTransient<ICsvProcessor, CsvProcessor>();
            builder.Services.AddSingleton<ITimerProcessor, TimerProcessor>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<InfoPage>();
            builder.Services.AddSingleton<MainpageVM>();
            //builder.Services.AddScoped<IPrintProcessor , PrintProcessor>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}