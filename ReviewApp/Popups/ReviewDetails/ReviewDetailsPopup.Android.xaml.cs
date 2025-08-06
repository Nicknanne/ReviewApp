using CommunityToolkit.Maui.Views;
using ReviewApp.ViewModels;

namespace ReviewApp.Popups
{
    public partial class ReviewDetailsPopupAndroid : Popup
    {
        public ReviewDetailsPopupAndroid(ReviewDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.ClosePopupAction = () => CloseAsync();
        }
    }
}