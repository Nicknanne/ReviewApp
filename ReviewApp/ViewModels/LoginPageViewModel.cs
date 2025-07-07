using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ReviewApp.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task Login()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}