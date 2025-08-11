using ReviewApp.Models;

namespace ReviewApp.Services
{
    public interface ISupabaseService
    {
        Task<(bool Success, string? ErrorMessage)> SignInAsync(string email, string password);
        Task<(bool Success, string? ErrorMessage)> SignUpAsync(string email, string password);
        Task<(bool Success, string? ErrorMessage)> SignOutAsync();

        Task<bool> IsUserAuthenticatedAsync();

        Task<Supabase.Gotrue.User?> GetCurrentUserAsync();

        Task<(bool Success, string? ErrorMessage, Review Review)> SubmitReviewAsync(Review review);
        Task<List<Review>> GetReviewsAsync();
    }
}