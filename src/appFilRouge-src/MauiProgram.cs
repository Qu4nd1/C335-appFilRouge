using Microsoft.Extensions.Logging;
using appFilRouge.Services;
using appFilRouge.ViewModels;

namespace appFilRouge
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

            builder.Services.AddSingleton<IDataService, JsonDataService>();

            // New ingredient form
            builder.Services.AddTransient<NewIngredientViewModel>();
            builder.Services.AddTransient<NewIngredient>();

            builder.Services.AddTransient<IngredientsViewModel>();
            builder.Services.AddTransient<MainPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
