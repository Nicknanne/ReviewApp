using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Models;
using ReviewApp.Popups;

namespace ReviewApp.ViewModels
{
    public partial class GameItemViewModel : ObservableObject
    {
        // review.cs
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public string? GameStatus { get; set; }
        public double Graphics { get; set; }
        public double Gameplay { get; set; }
        public double Sound { get; set; }
        public double PlotAndLore { get; set; }
        public double Impression { get; set; }
        public double Immersive { get; set; }
        public double Replaybility { get; set; }
        public double OverallRating => Math.Round((Graphics + Gameplay + Sound + PlotAndLore + Impression + Immersive + Replaybility) / 7, 2);

        // game.cs
        public string? GameTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Platforms { get; set; }
        public string? Genre { get; set; }
        public string? Developer { get; set; }
        public string? Description { get; set; }

        public string? Rating { get; set; }

        private ObservableCollection<Review> _review = new();
        private Game _game;
        public GameItemViewModel(ObservableCollection<Review> review, Game game)
        {
            _review = review;
            _game = game;

            GameTitle = game.Title;
            ReleaseDate = game.ReleaseDate;
            Platforms = game.Platforms;
            Genre = game.Genre;
            Developer = game.Developer;
            Description = game.Description;

            // var sb = new StringBuilder();
            // sb.AppendLine($"Graphics: {review.Average(x => x.Graphics)}");
            // sb.AppendLine($"Gameplay: {review.Average(x => x.Gameplay)}");
            // sb.AppendLine($"Sound: {review.Average(x => x.Sound)}");
            // sb.AppendLine($"Plot and lore: {review.Average(x => x.PlotAndLore)}");
            // sb.AppendLine($"Impression: {review.Average(x => x.Impression)}");
            // sb.AppendLine($"Immersive: {review.Average(x => x.Immersive)}");
            // sb.AppendLine($"Replaybility: {review.Average(x => x.Replaybility)}");
            // sb.AppendLine($"Overall rating: {review.Average(x => x.OverallRating)}");
            // Rating = sb.ToString();
        }

        // [RelayCommand]
        // private async Task OpenReviewDetailsPopap()
        // {
        //     var reviewDetailsPopup = new ReviewDetailsPopup(new ReviewDetailsViewModel(_review, _game));
        //     await Shell.Current.ShowPopupAsync(reviewDetailsPopup);
        // }
    }
}