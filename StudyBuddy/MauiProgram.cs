﻿namespace StudyBuddy;

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
				fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
				fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
				fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<HomeViewModel>();

		builder.Services.AddSingleton<HomePage>();

		builder.Services.AddSingleton<CategoryViewModel>();

		builder.Services.AddSingleton<CategoryPage>();

		builder.Services.AddSingleton<ManageCardsViewModel>();

		builder.Services.AddSingleton<ManageCardsPage>();

		builder.Services.AddSingleton<LearningViewModel>();

		builder.Services.AddSingleton<LearningPage>();

		builder.Services.AddSingleton<LocalizationViewModel>();

		builder.Services.AddSingleton<LocalizationPage>();

		return builder.Build();
	}
}
