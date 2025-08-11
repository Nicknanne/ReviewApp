using System.Diagnostics;
using ReviewApp.ViewModels;

namespace ReviewApp;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
    }
}