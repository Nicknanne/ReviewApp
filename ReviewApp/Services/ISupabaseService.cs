namespace ReviewApp.Services
{
    public interface ISupabaseService
    {
        Task<(bool Success, string? ErrorMessage)> SignInAsync(string email, string password);
        Task<(bool Success, string? ErrorMessage)> SignUpAsync(string email, string password);

        Task<bool> IsUserAuthenticatedAsync();
    }
}