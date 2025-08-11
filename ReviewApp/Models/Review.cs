namespace ReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int GameId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string GameStatus { get; set; } = string.Empty;

        public double Graphics { get; set; }
        public double Gameplay { get; set; }
        public double Sound { get; set; }
        public double PlotAndLore { get; set; }
        public double Impression { get; set; }
        public double Immersive { get; set; }
        public double Replayability { get; set; }

        public double OverallRating => Math.Round((Graphics + Gameplay + Sound + PlotAndLore + Impression + Immersive + Replayability) / 7, 2);

        public Review() { }
    }
}
