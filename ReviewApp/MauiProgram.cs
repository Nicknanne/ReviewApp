using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ReviewApp.Pages;
using ReviewApp.Popups;
using ReviewApp.Services;
using ReviewApp.ViewModels;
using Microsoft.Extensions.Configuration;
using Supabase;
using System.Reflection;

namespace ReviewApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		var a = Assembly.GetExecutingAssembly();
		using var stream = a.GetManifestResourceStream("ReviewApp.appsettings.json");
		if (stream != null)
		{
			builder.Configuration.AddJsonStream(stream);
		}

		var options = new SupabaseOptions
		{
			AutoRefreshToken = true,
			AutoConnectRealtime = true,
			SessionHandler = new SecureSessionPersistence()
		};

		builder.Services.AddSingleton(provider =>
		{
			var config = provider.GetRequiredService<IConfiguration>();

			var supabaseUrl = config["Supabase:Url"];
			var supabaseKey = config["Supabase:Key"];

			return new Supabase.Client(supabaseUrl, supabaseKey, options);
		});

		builder.Services.AddSingleton<ISupabaseService, SupabaseService>();
		builder.Services.AddSingleton<IReviewService, ReviewService>();
		builder.Services.AddSingleton<IGamesService, GamesService>();

		builder.Services.AddTransient<LoginPageViewModel>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<MainPageAndroid>();
		builder.Services.AddTransient<MainPageWindows>();
		builder.Services.AddTransient<AddReviewViewModel>();
		builder.Services.AddTransient<AddReviewPage>();
		builder.Services.AddTransient<AllGamesViewModel>();
		builder.Services.AddTransient<AllGamesPageAndroid>();
		builder.Services.AddTransient<AllGamesPageWindows>();
		builder.Services.AddTransient<AllPublicReviewsViewModel>();
		builder.Services.AddTransient<AllPublicReviewsPageAndroid>();
		builder.Services.AddTransient<AllPublicReviewsPageWindows>();

		builder.Services.AddTransient<ReviewDetailsViewModel>();
		builder.Services.AddTransient<ReviewDetailsPopupAndroid>();
		builder.Services.AddTransient<ReviewDetailsPopupWindows>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
