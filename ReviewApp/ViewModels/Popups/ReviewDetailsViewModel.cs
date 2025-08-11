using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Popups;
using ReviewApp.Models;
using System.Text;

namespace ReviewApp.ViewModels
{
    public partial class ReviewDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _graphicsText;
        [ObservableProperty]
        private string? _gameplayText;
        [ObservableProperty]
        private string? _soundText;
        [ObservableProperty]
        private string? _plotAndLoreText;
        [ObservableProperty]
        private string? _impressionText;
        [ObservableProperty]
        private string? _immersiveText;
        [ObservableProperty]
        private string? _replayabilityText;
        [ObservableProperty]
        private string? _overallRatingText;
        [ObservableProperty]
        private Color _GameStatusColor;

        // review.cs
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _userId;
        [ObservableProperty]
        private string? _title;
        [ObservableProperty]
        private string? _comment;
        [ObservableProperty]
        private string? _gameStatus;
        [ObservableProperty]
        private double _graphics;
        [ObservableProperty]
        private double _gameplay;
        [ObservableProperty]
        private double _sound;
        [ObservableProperty]
        private double _plotAndLore;
        [ObservableProperty]
        private double _impression;
        [ObservableProperty]
        private double _immersive;
        [ObservableProperty]
        private double _replayability;
        public double OverallRating => Math.Round((_graphics + _gameplay + _sound + _plotAndLore + _impression + _immersive + _replayability) / 7, 2);

        // game.cs
        [ObservableProperty]
        private string? _gameTitle;
        [ObservableProperty]
        private DateTime _releaseDate;
        [ObservableProperty]
        private string? _platforms;
        [ObservableProperty]
        private string? _genre;
        [ObservableProperty]
        private string? _developer;
        [ObservableProperty]
        private string? _description;

        [ObservableProperty]
        private string? _rating;

        public Action ClosePopupAction;
        public ReviewDetailsViewModel(Review review, Game game)
        {
            Comment = review.Comment;
            Graphics = review.Graphics;
            Gameplay = review.Gameplay;
            Sound = review.Sound;
            PlotAndLore = review.PlotAndLore;
            Impression = review.Impression;
            Immersive = review.Immersive;
            Replayability = review.Replayability;

            if (game != null)
            {
                GameTitle = game.Title;
                GameStatus = $"Status: {review.GameStatus}";
                ReleaseDate = game.ReleaseDate;
                Platforms = game.Platforms;
                Genre = game.Genre;
                Developer = game.Developer;
                Description = game.Description;
            }
            else
            {
                GameTitle = review.Title;
                GameStatus = $"Status: {review.GameStatus}";
            }

            if (review.GameStatus == "Completed")
                GameStatusColor = new(0, 120, 0);
            else if (review.GameStatus == "Abandoned")
                GameStatusColor = new(120, 0, 0);
            else
                GameStatusColor = new(100, 0, 130);

            GraphicsText = $"Graphics: {Graphics}";
            GameplayText = $"Gameplay: {Gameplay}";
            SoundText = $"Sound: {Sound}";
            PlotAndLoreText = $"Plot and lore: {PlotAndLore}";
            ImpressionText = $"Impression: {Impression}";
            ImmersiveText = $"Immersive: {Immersive}";
            ReplayabilityText = $"Replayability: {Replayability}";
            OverallRatingText = $"Overall rating: {OverallRating}";

            var sb = new StringBuilder();
            sb.AppendLine($"Graphics: {Graphics}");
            sb.AppendLine($"Gameplay: {Gameplay}");
            sb.AppendLine($"Sound: {Sound}");
            sb.AppendLine($"Plot and lore: {PlotAndLore}");
            sb.AppendLine($"Impression: {Impression}");
            sb.AppendLine($"Immersive: {Immersive}");
            sb.AppendLine($"Replayability: {Replayability}");
            sb.AppendLine($"Overall rating: {OverallRating}");
            Rating = sb.ToString();
        }

        [RelayCommand]
        private void ClosePopup()
        {
            ClosePopupAction?.Invoke();
        }

        [RelayCommand]
        private async Task CopyReview()
        {
            string bufferText = $"Оценка игры {GameTitle}\n\n" +
            $"Comment:\n{Comment}\n\n" +
            $"Graphics: {Graphics}\n" +
            $"Gameplay: {Gameplay}\n" +
            $"Sound: {Sound}\n" +
            $"Plot and lore: {PlotAndLore}\n" +
            $"Impression: {Impression}\n" +
            $"Immersive: {Immersive}\n" +
            $"Replayability: {Replayability}\n" +
            $"Overall rating: {OverallRating}\n\n" +
            $"Telegram: https://t.me/nicknameu_u";

            await Clipboard.SetTextAsync(bufferText);
        }
    }
}