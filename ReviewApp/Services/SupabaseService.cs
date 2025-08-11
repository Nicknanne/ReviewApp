using ReviewApp.Models;

namespace ReviewApp.Services
{
    public class SupabaseService : ISupabaseService
    {
        private readonly Supabase.Client _client;

        public SupabaseService(Supabase.Client client)
        {
            _client = client;
        }

        public async Task<(bool Success, string? ErrorMessage)> SignInAsync(string email, string password)
        {
            try
            {
                var respone = await _client.Auth.SignIn(email, password);
                if (respone.User != null)
                    return (true, null);
                return (false, "User is null");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> SignUpAsync(string email, string password)
        {
            try
            {
                var respone = await _client.Auth.SignUp(email, password);
                if (respone.User != null)
                    return (true, null);
                return (false, "Login failed. Check email and password.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> SignOutAsync()
        {
            try
            {
                await _client.Auth.SignOut();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public Task<bool> IsUserAuthenticatedAsync()
        {
            return Task.FromResult(_client.Auth.CurrentSession != null);
        }

        public Task<Supabase.Gotrue.User?> GetCurrentUserAsync()
        {
            return Task.FromResult(_client.Auth.CurrentUser);
        }

        public async Task<(bool Success, string? ErrorMessage, Review Review)> SubmitReviewAsync(Review review)
        {
            try
            {
                var respone = await _client.From<Review>().Insert(review);
                if (respone.Model != null)
                    return (true, null, respone.Model);
                return (false, "Sumbit failed.", review);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, review);
            }
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            var response = await _client.From<Review>().Get();
            var reviews = response.Models;
            return reviews;
        }
    }
}