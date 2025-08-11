using System.Text;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Models;
using ReviewApp.Popups;

namespace ReviewApp.ViewModels
{
    public partial class ReviewItemViewModel : ObservableObject
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

        private Review _review;
        private Game _game;
        public ReviewItemViewModel(Review review, Game game)
        {
            _review = review;
            _game = game;

            Comment = review.Comment!.Length > 130 ? review.Comment[..127] + "..." : review.Comment;
            Graphics = review.Graphics;
            Gameplay = review.Gameplay;
            Sound = review.Sound;
            PlotAndLore = review.PlotAndLore;
            Impression = review.Impression;
            Immersive = review.Immersive;
            Replaybility = review.Replayability;

            if (game != null)
            {
                GameTitle = game.Title;
                ReleaseDate = game.ReleaseDate;
                Platforms = game.Platforms;
                Genre = game.Genre;
                Developer = game.Developer;
                Description = game.Description;
            }
            else
            {
                GameTitle = review.Title;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Overall rating: {OverallRating}");
            Rating = sb.ToString();
        }

        [RelayCommand]
        private async Task OpenReviewDetailsPopap()
        {
#if ANDROID
            var reviewDetailsPopup = new ReviewDetailsPopupAndroid(new ReviewDetailsViewModel(_review, _game));
#else
            var reviewDetailsPopup = new ReviewDetailsPopupWindows(new ReviewDetailsViewModel(_review, _game));
#endif
            await Shell.Current.ShowPopupAsync(reviewDetailsPopup);
        }
    }
}