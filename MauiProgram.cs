using Microsoft.Extensions.Logging;
using VelvetRelics.Data;
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

		// ─── Database ───────────────────────────────────────────────────────────
		// Singleton — один екземпляр на весь час роботи застосунку
		builder.Services.AddSingleton<VelvetRelicsDatabase>();

		// ─── Services ───────────────────────────────────────────────────────────
		builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
		builder.Services.AddSingleton<SessionService>();
		builder.Services.AddSingleton<IProductService, ProductService>();

		// ─── ViewModels ─────────────────────────────────────────────────────────
		// Transient — новий екземпляр кожного разу, коли потрібен
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<RegisterViewModel>();
		builder.Services.AddTransient<CatalogViewModel>();
		builder.Services.AddTransient<ProductDetailsViewModel>();

		// ─── Views ──────────────────────────────────────────────────────────────
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<RegisterPage>();
		builder.Services.AddTransient<CatalogPage>();
		builder.Services.AddTransient<ProductDetailsPage>();

		return builder.Build();
	}
}
