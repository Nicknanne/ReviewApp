using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ReviewApp.Models
{
    [Table("reviews")]
    public class Review : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("game_id")]
        public int? GameId { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;
        [Column("comment")]
        public string Comment { get; set; } = string.Empty;
        [Column("game_status")]
        public string GameStatus { get; set; } = string.Empty;

        [Column("graphics")]
        public double Graphics { get; set; }
        [Column("gameplay")]
        public double Gameplay { get; set; }
        [Column("sound")]
        public double Sound { get; set; }
        [Column("plot_and_lore")]
        public double PlotAndLore { get; set; }
        [Column("impression")]
        public double Impression { get; set; }
        [Column("immersive")]
        public double Immersive { get; set; }
        [Column("replayability")]
        public double Replayability { get; set; }

        [Column("overall_rating", ignoreOnInsert:true)]
        public double OverallRating => Math.Round((Graphics + Gameplay + Sound + PlotAndLore + Impression + Immersive + Replayability) / 7, 2);

        public Review() { }
    }
}
