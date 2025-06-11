namespace ReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int GameId { get; set; }

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

        public double OverallRating => (Graphics + Gameplay + Sound + PlotAndLore + Impression + Immersive + Replaybility) / 7;

        public Review() { }
    }
}
