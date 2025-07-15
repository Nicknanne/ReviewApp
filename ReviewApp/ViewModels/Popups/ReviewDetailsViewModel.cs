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
        private double _replaybility;
        public double OverallRating => Math.Round((_graphics + _gameplay + _sound + _plotAndLore + _impression + _immersive + _replaybility) / 7, 2);

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
            Replaybility = review.Replaybility;

            GameTitle = game.Title;
            ReleaseDate = game.ReleaseDate;
            Platforms = game.Platforms;
            Genre = game.Genre;
            Developer = game.Developer;
            Description = game.Description;

            var sb = new StringBuilder();
            sb.AppendLine($"Graphics: {Graphics}");
            sb.AppendLine($"Gameplay: {Gameplay}");
            sb.AppendLine($"Sound: {Sound}");
            sb.AppendLine($"Plot and lore: {PlotAndLore}");
            sb.AppendLine($"Impression: {Impression}");
            sb.AppendLine($"Immersive: {Immersive}");
            sb.AppendLine($"Replaybility: {Replaybility}");
            sb.AppendLine($"Overall rating: {OverallRating}");
            Rating = sb.ToString();
        }

        [RelayCommand]
        private async Task ClosePopup()
        {
            ClosePopupAction?.Invoke();
        }
    }
}