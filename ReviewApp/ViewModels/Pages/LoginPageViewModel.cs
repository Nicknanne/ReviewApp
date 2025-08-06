using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ReviewApp.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [RelayCommand]
        private void Login()
        {
#if ANDROID
            Application.Current!.MainPage = new AppShellAndroid();
#else
            Application.Current!.MainPage = new AppShellWindows();
#endif
        }
    }
}