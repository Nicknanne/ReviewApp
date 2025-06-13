using ReviewApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ReviewApp;

public partial class AddReviewPage : ContentPage
{
    private AddReviewViewModel _viewModel => (AddReviewViewModel)BindingContext;

    public AddReviewPage(AddReviewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnGameStatusCheckedChange(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var radioButton = (RadioButton)sender;
            _viewModel.SelectedGameStatus = radioButton.Value.ToString();
        }
    }
}