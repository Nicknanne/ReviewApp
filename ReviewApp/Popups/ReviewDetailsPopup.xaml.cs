using CommunityToolkit.Maui.Views;
using ReviewApp.ViewModels;

namespace ReviewApp.Popups
{
    public partial class ReviewDetailsPopup : Popup
    {
        public ReviewDetailsPopup(ReviewDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.ClosePopupAction = () => CloseAsync();
        }
    }
}