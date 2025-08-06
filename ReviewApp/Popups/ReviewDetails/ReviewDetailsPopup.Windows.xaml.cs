using CommunityToolkit.Maui.Views;
using ReviewApp.ViewModels;

namespace ReviewApp.Popups
{
    public partial class ReviewDetailsPopupWindows : Popup
    {
        public ReviewDetailsPopupWindows(ReviewDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.ClosePopupAction = () => CloseAsync();
        }
    }
}