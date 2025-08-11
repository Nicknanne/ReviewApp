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

        public async Task<bool> IsUserAuthenticatedAsync()
        {
            return _client.Auth.CurrentSession != null;
        }
    }
}