using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Services;

namespace ReviewApp.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly ISupabaseService _supabaseService;
        public LoginPageViewModel(ISupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [ObservableProperty]
        private string? _email;

        [ObservableProperty]
        private string? _password;

        [RelayCommand]
        private async Task Login()
        {
            try
            {
                var (Success, ErrorMessage) = await _supabaseService.SignInAsync(Email, Password);
                if (!Success)
                {
                    Debug.WriteLine(ErrorMessage);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
#if ANDROID
            Application.Current!.MainPage = new AppShellAndroid();
#else
            Application.Current!.MainPage = new AppShellWindows();
#endif
        }

        [RelayCommand]
        private async Task Register()
        {
            try
            {
                var (Success, ErrorMessage) = await _supabaseService.SignUpAsync(Email, Password);
                if (!Success)
                {
                    Debug.WriteLine(ErrorMessage);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
#if ANDROID
            Application.Current!.MainPage = new AppShellAndroid();
#else
            Application.Current!.MainPage = new AppShellWindows();
#endif
        }
    }
}