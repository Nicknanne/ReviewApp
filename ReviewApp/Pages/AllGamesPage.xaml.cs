using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.ViewModels;

namespace ReviewApp
{
    public partial class AllGamesPage : ContentPage
    {
        public AllGamesPage(AllGamesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}