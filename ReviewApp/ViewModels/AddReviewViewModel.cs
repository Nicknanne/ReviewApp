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
        private ObservableCollection<Game> _availableGames = new();

        [ObservableProperty]
        private Game? _selectedGame;

        [ObservableProperty]
        private string? _commentText;

        [ObservableProperty]
        private string? _searchQuery;

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
        private double _replaybilityRating = 5;

        [ObservableProperty]
        private string _selectedGameStatus = "Completed";

        [RelayCommand]
        private async Task SubmitReview()
        {
            if (SelectedGame == null)
            {
                await Application.Current.MainPage!.DisplayAlert("Ошибка", "Пожалуйста, выберите игру", "ОК");
                return;
            }
            if (string.IsNullOrWhiteSpace(CommentText))
            {
                await Application.Current.MainPage!.DisplayAlert("Ошибка", "Пожалуйста, выберите игру", "ОК");
                return;
            }

            var newReview = new Review
            {
                UserId = -1,
                GameId = SelectedGame.Id,
                Title = CommentText.Length > 50 ? CommentText.Substring(0, 50) + "..." : CommentText,
                Comment = CommentText,
                GameStatus = SelectedGameStatus, 
                Graphics = GraphicsRating,
                Gameplay = GameplayRating,
                Sound = SoundRating,
                PlotAndLore = PlotAndLoreRating,
                Impression = ImpressionRating,
                Immersive = ImmersiveRating,
                Replaybility = ReplaybilityRating
            };

            try
            {
                var savedReview = await _reviewService.AddReviewAsync(newReview);

                if (savedReview != null)
                {
                    await Application.Current.MainPage!.DisplayAlert("Успех", "Рецензия успешно сохранена!", "ОК");
                    await Shell.Current.GoToAsync(".."); 
                }
                else
                {
                    await Application.Current.MainPage!.DisplayAlert("Ошибка", "Не удалось сохранить рецензию. Попробуйте еще раз.", "ОК");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка сохранения рецензии: {ex.Message}");
                await Application.Current.MainPage!.DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "ОК");
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
                AvailableGames = new ObservableCollection<Game>(games);
            }
        }
    }
}
