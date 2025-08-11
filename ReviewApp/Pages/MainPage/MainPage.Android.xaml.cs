using ReviewApp.ViewModels;

namespace ReviewApp.Pages;

public partial class MainPageAndroid : ContentPage
{
	public MainPageAndroid(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is MainPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
    }
}