using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ReviewApp.Pages;
using ReviewApp.Popups;
using ReviewApp.Services;
using ReviewApp.ViewModels;

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

		builder.Services.AddTransient<ReviewDetailsViewModel>();
		builder.Services.AddTransient<ReviewDetailsPopupAndroid>();
		builder.Services.AddTransient<ReviewDetailsPopupWindows>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
