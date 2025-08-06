using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.ViewModels;

namespace ReviewApp
{
    public partial class AllGamesPageWindows : ContentPage
    {
        public AllGamesPageWindows(AllGamesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}