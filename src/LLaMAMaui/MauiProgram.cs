using Drastic.Services;
using LLaMAMaui.Services;
using LLaMAMaui.ViewModels;
using Microsoft.Extensions.Logging;

namespace LLaMAMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
#if MACCATALYST
        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("ButtonChange", (handler, view) =>
        {
            handler.PlatformView.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
        });
#endif

        var builder = MauiApp.CreateBuilder();

        builder.Services
            .AddSingleton<IAppDispatcher, LLaMAMaui.Services.AppDispatcher>()
            .AddSingleton<IErrorHandlerService, ErrorHandler>()
            .AddSingleton<MainViewModel>();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
