using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.ViewModels;

namespace ReviewApp
{
    public partial class AllGamesPageAndroid : ContentPage
    {
        public AllGamesPageAndroid(AllGamesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}