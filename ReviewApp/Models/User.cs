namespace ReviewApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Email { get; set; }
        public string? Username { get; set; }

        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }

        public DateTime RegistrationDate { get; set; }
        public int ReviewsCount { get; set; }
        public double AverageOverallRating { get; set; }

        public string? Role { get; set; } = "User";
        public bool IsActive { get; set; } = true;
    }
}
