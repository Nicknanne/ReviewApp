using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ReviewApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task GoToAddReviewPage()
        {
            await Shell.Current.GoToAsync(nameof(AddReviewPage));
        }
    }
}