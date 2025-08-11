using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Models;
using ReviewApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ReviewApp.ViewModels
{
    public partial class AddReviewViewModel : ObservableObject
    {
        private readonly IGamesService _gamesService;
        private readonly IReviewService _reviewService;

        [ObservableProperty]
        private ObservableCollection<Game> _filteredGames = new();

        private ObservableCollection<Game> _availableGames = new();

        [ObservableProperty]
        private Game? _selectedGame;

        [ObservableProperty]
        private int? _selectedGameIndex;

        [ObservableProperty]
        private string? _gameTitle;

        [ObservableProperty]
        private string? _commentText;

        // [ObservableProperty]
        // private string? _searchQuery;

        // partial void OnSearchQueryChanged(string value)
        // {
        //     _filteredGames.Clear();
        //     if (string.IsNullOrWhiteSpace(value))
        //     {
        //         foreach (var game in _availableGames)
        //         {
        //             _filteredGames.Add(game);
        //         }
        //         SelectedGameIndex = 0;
        //     }
        //     else
        //     {
        //         foreach (var game in _availableGames.Where(x => x.Title!.Contains(value, StringComparison.CurrentCultureIgnoreCase)))
        //         {
        //             _filteredGames.Add(game);
        //         }
        //         if (_filteredGames.Count > 0)
        //             SelectedGameIndex = 0;
        //     }
        // }

        [ObservableProperty]
        private double _graphicsRating = 5;
        [ObservableProperty]
        private double _gameplayRating = 5;
        [ObservableProperty]
        private double _soundRating = 5;
        [ObservableProperty]
        private double _plotAndLoreRating = 5;
        [ObservableProperty]
        private double _impressionRating = 5;
        [ObservableProperty]
        private double _immersiveRating = 5;
        [ObservableProperty]
        private double _replayabilityRating = 5;

        [ObservableProperty]
        private string _selectedGameStatus = "Completed";

        [RelayCommand]
        private async Task SubmitReview()
        {
            if (string.IsNullOrWhiteSpace(CommentText))
            {
                await Application.Current!.MainPage!.DisplayAlert("Ошибка", "Пожалуйста, выберите игру", "ОК");
                return;
            }

            var newReview = new Review
            {
                UserId = -1,
                GameId = -1,
                Title = GameTitle,
                Comment = CommentText,
                GameStatus = SelectedGameStatus,
                Graphics = (int)GraphicsRating,
                Gameplay = (int)GameplayRating,
                Sound = (int)SoundRating,
                PlotAndLore = (int)PlotAndLoreRating,
                Impression = (int)ImpressionRating,
                Immersive = (int)ImmersiveRating,
                Replayability = (int)ReplayabilityRating
            };

            try
            {
                var savedReview = await _reviewService.AddReviewAsync(newReview);

                if (savedReview != null)
                {
                    await Application.Current!.MainPage!.DisplayAlert("Успех", "Рецензия успешно сохранена!", "ОК");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Ошибка", "Не удалось сохранить рецензию. Попробуйте еще раз.", "ОК");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка сохранения рецензии: {ex.Message}");
                await Application.Current!.MainPage!.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "ОК");
            }
        }

        public AddReviewViewModel(IGamesService gameService, IReviewService reviewService)
        {
            _gamesService = gameService;
            _reviewService = reviewService;

            _ = LoadGames();
        }

        private async Task LoadGames()
        {
            var games = await _gamesService.GetGamesAsync();
            if (games != null)
            {
                _availableGames = new(games);
                foreach (var game in _availableGames)
                {
                    FilteredGames.Add(game);
                }
                SelectedGameIndex = 0;
            }
        }
    }
}
