using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Models;
using ReviewApp.Services;

namespace ReviewApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _pageLabel;
        private int _page;
        private int _maxPage;

        private ObservableCollection<Review> _allReviews;
        private ObservableCollection<Game> _allGames;

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
            _allReviews = await _reviewService.GetReviewsAsync();
            _allGames = await _gameService.GetGamesAsync();

            _page = 0;
            _maxPage = (int)MathF.Ceiling(_allReviews.Count / 10f);

            await UpdateGames(_page);
        }

        private async Task UpdateGames(int page)
        {
            Reviews.Clear();

            await Task.Run(() =>
            {
                foreach (var review in _allReviews)
                {
                    Reviews.Add(new(review, null));
                }
            });

            PageLabel = $"Page {page + 1}";
        }

        public ObservableCollection<ReviewItemViewModel> Reviews { get; set; } = new ObservableCollection<ReviewItemViewModel>();

        [RelayCommand]
        private async Task NextPage()
        {
            if (_page == _maxPage - 1)
                return;
            await UpdateGames(++_page);
        }

        [RelayCommand]
        private async Task PreviousPage()
        {
            if (_page == 0)
                return;
            await UpdateGames(--_page);
        }
    }
}