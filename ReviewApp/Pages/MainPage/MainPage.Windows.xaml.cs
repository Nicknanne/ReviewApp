using System.Diagnostics;
using ReviewApp.ViewModels;

namespace ReviewApp.Pages;

public partial class MainPageWindows : ContentPage
{
	public MainPageWindows(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is MainPageViewModel viewModel)
		{
			Debug.WriteLine("on appearing");
			await viewModel.OnAppearing();
		}
    }
}