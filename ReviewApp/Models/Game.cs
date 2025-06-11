namespace ReviewApp.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Platforms { get; set; }
        public string? Genre { get; set; }
        public string? Developer { get; set; }
        public string? Description { get; set; }
    }
}
