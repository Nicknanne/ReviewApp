using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Models;
using ReviewApp.Services;

namespace ReviewApp.ViewModels
{
    public partial class AllGamesViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _pageLabel;

        private readonly IReviewService _reviewService;
        private readonly IGamesService _gameService;

        private ObservableCollection<Review> _allReview;
        private ObservableCollection<Game> _allGames;

        private int _page;
        private int _maxPage;

        public AllGamesViewModel(IReviewService reviewService, IGamesService gamesService)
        {
            _reviewService = reviewService;
            _gameService = gamesService;

            _ = Init();
        }

        private async Task Init()
        {
            _allReview = await _reviewService.GetReviewsAsync();
            _allGames = await _gameService.GetGamesAsync();

            _page = 0;
            _maxPage = (int)MathF.Ceiling(_allGames.Count / 10f);

            await UpdateGames(_page);
        }

        private async Task UpdateGames(int page)
        {
            Games.Clear();

            var newGameItems = await Task.Run(() =>
            {
                var viewGames = _allGames.Skip(page * 10).Take(10);

                var tempGameItems = new List<GameItemViewModel>();
                foreach (var game in viewGames)
                {
                    tempGameItems.Add(new(new(), game));
                }

                return tempGameItems;
            });

            foreach (var gameItem in newGameItems)
            {
                Games.Add(gameItem);
            }

            PageLabel = $"Page {page + 1}";
        }

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

        public ObservableCollection<GameItemViewModel> Games { get; set; } = new ObservableCollection<GameItemViewModel>();
    }
}