using System.Diagnostics;
using ReviewApp.Popups;

namespace ReviewApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Supabase.Client _supabaseClient;
        public App(IServiceProvider serviceProvider, Supabase.Client supabaseClient)
        {
            _serviceProvider = serviceProvider;
            _supabaseClient = supabaseClient;
            InitializeComponent();

            MainPage = new ContentPage();

            InitializeAsync();

#if ANDROID
            Routing.RegisterRoute("MainPage", typeof(Pages.MainPageAndroid));
            Routing.RegisterRoute("AllGamesPage", typeof(AllGamesPageAndroid));
            Routing.RegisterRoute("ReviewDetailsPopup", typeof(ReviewDetailsPopupAndroid));
#else
            Routing.RegisterRoute("MainPage", typeof(Pages.MainPageWindows));
            Routing.RegisterRoute("AllGamesPage", typeof(AllGamesPageWindows));
            Routing.RegisterRoute("ReviewDetailsPopup", typeof(ReviewDetailsPopupWindows));
#endif

            Routing.RegisterRoute(nameof(AddReviewPage), typeof(AddReviewPage));
        }

        private async Task InitializeAsync()
        {
            await _supabaseClient.InitializeAsync();

            _supabaseClient.Auth.LoadSession();
            await _supabaseClient.Auth.RetrieveSessionAsync();

            if (_supabaseClient.Auth.CurrentSession != null)
            {
#if ANDROID
                MainPage = new AppShellAndroid();
#else
                MainPage = new AppShellWindows();
#endif  
            }
            else
            {
                var loginPage = _serviceProvider.GetRequiredService<LoginPage>();
                MainPage = new NavigationPage(loginPage);
            }
        }
    }
}
