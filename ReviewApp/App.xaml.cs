using ReviewApp.Popups;

namespace ReviewApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage(new()));

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
    }
}
