using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Models;
using ReviewApp.Services;

namespace ReviewApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IReviewService _reviewService;
        private readonly IGamesService _gameService;
        public MainPageViewModel(IReviewService reviewService, IGamesService gamesService)
        {
            _reviewService = reviewService;
            _gameService = gamesService;    

            _ = Init();
        }

        private async Task Init()
        {
            var allReviews = await _reviewService.GetReviewsAsync();
            var allGames = await _gameService.GetGamesAsync();

            var gameDictionary = allGames.ToDictionary(g => g.Id);

            foreach (var review in allReviews)
            {
                Reviews.Add(new (review, gameDictionary[review.GameId]));
            }
            
        }

        public ObservableCollection<ReviewItemViewModel> Reviews { get; set; } = new ObservableCollection<ReviewItemViewModel>();

        [RelayCommand]
        private async Task GoToAddReviewPage()
        {
            await Shell.Current.GoToAsync(nameof(AddReviewPage));
        }
    }
}