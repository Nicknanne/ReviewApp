using ReviewApp.ViewModels;

namespace ReviewApp.Pages;

public partial class MainPageAndroid : ContentPage
{
	public MainPageAndroid(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}