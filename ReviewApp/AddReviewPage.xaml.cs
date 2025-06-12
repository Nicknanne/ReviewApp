using ReviewApp.Services;
using ReviewApp.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ReviewApp;

public partial class AddReviewPage : ContentPage
{
    private readonly IGamesService _gamesService;
    private readonly IReviewService _reviewService;

    private ObservableCollection<Game> _games;
    private ObservableCollection<Review> _reviews;

    public AddReviewPage(IGamesService gamesService, IReviewService reviewService)
    {
        InitializeComponent();

        _gamesService = gamesService;
        _reviewService = reviewService;

        _games = new();
        _reviews = new();

        _ = InitializationDataAsync();
    }

    private async Task InitializationDataAsync()
    {
        _games = await _gamesService.GetGamesAsync();
        _reviews = await _reviewService.GetReviewsAsync();

        GamesPicker.ItemsSource = _games;
        GamesPicker.ItemDisplayBinding = new Binding("Title");
        GamesPicker.SelectedIndex = 0;
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filteredGames = _games.Where(x => x.Title != null && x.Title.Contains(e.NewTextValue, StringComparison.CurrentCultureIgnoreCase)).ToList();
        GamesPicker.ItemsSource = filteredGames;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var selectedGame = GamesPicker.SelectedItem as Game;
        if (selectedGame != null)
        {
            var newReview = new Review()
            {
                Id = 1,
                UserId = -1,
                GameId = selectedGame.Id,
                Title = "My review",
                Comment = "Its prosto ahuenaya igra, top",
                Graphics = 7,
                Gameplay = 6,
                Sound = 5,
                PlotAndLore = 4,
                Impression = 3,
                Immersive = 2,
                Replaybility = 1
            };
            await _reviewService.AddReviewAsync(newReview);
        }
    }
}