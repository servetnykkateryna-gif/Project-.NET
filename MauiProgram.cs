using Microsoft.Extensions.Logging;
using VelvetRelics.Services;
using VelvetRelics.ViewModels;
using VelvetRelics.Views;

namespace VelvetRelics;

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

		// Services
		builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

		// ViewModels
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<RegisterViewModel>();

		// Views
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<RegisterPage>();

		return builder.Build();
	}
}
