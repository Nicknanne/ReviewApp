using ReviewApp.ViewModels;

namespace ReviewApp.Pages;

public partial class MainPageWindows : ContentPage
{
	public MainPageWindows(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}