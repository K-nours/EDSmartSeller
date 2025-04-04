using EDSmartSellerUI.Class;
using Microsoft.Extensions.Logging;

namespace EDSmartSellerUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
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
#if WINDOWS
            builder.Services.AddSingleton<IMouseOperations, WindowsMouseOperations>();
#endif
#if MACCATALYST
            builder.Services.AddSingleton<IMouseOperations, MacMouseOperations>();
#endif
            return builder.Build();
        }
    }
}
