using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Models;

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

        public ReviewItemViewModel(Review review, Game game)
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
    }
}