using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.ViewModels;

namespace ReviewApp
{
    public partial class AllPublicReviewsPageWindows : ContentPage
    {
        public AllPublicReviewsPageWindows(AllPublicReviewsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is AllPublicReviewsViewModel viewModel)
            {
                await viewModel.OnAppearing();
            }
        }
    }
}