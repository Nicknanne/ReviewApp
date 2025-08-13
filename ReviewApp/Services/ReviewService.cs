using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using ReviewApp.Models;

namespace ReviewApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ISupabaseService _supabaseService;
        private readonly string _filePath;
        private ObservableCollection<Review> _reviews;
        private bool _isLoaded;

        public ReviewService(ISupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "reviews.json");
            _reviews = new ObservableCollection<Review>();
            _isLoaded = false;
        }

        private async Task LoadReviewsAsync()
        {
            if (!File.Exists(_filePath))
            {
                await SaveReviewsAsync();
                return;
            }

            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var loadedReviews = JsonSerializer.Deserialize<List<Review>>(json);
                if (loadedReviews != null)
                {
                    _reviews.Clear();
                    foreach (var review in loadedReviews)
                    {
                        _reviews.Add(review);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading reviews: {ex.Message}\n{_filePath}");
                _reviews.Clear();
            }

            if (await _supabaseService.IsUserAuthenticatedAsync())
            {
                var user = await _supabaseService.GetCurrentUserAsync();
                var tempReviews = new ObservableCollection<Review>();
                try
                {
                    var reviews = await _supabaseService.GetReviewsAsync();
                    foreach (var review in reviews)
                    {
                        tempReviews.Add(review);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading reviews: {ex.Message}");
                    tempReviews.Clear();
                }

                foreach (var review in tempReviews)
                {
                    _reviews.Add(review);
                }
            }
        }

        private async Task SaveReviewsAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(_reviews.ToList());
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving reviews: {ex.Message}");
            }
        }

        private async Task EnsureReviewsLoadedAsync()
        {
            if (_isLoaded)
                return;

            await LoadReviewsAsync();
            _isLoaded = true;
        }

        public async Task<ObservableCollection<Review>> GetReviewsForGameAsync(int gameId)
        {
            await EnsureReviewsLoadedAsync();
            return new ObservableCollection<Review>(_reviews.Where(r => r.GameId == gameId));
        }

        public async Task<Review> AddReviewAsync(Review newReview)
        {
            await EnsureReviewsLoadedAsync();
            newReview.Id = _reviews.Any() ? _reviews.Max(r => r.Id) + 1 : 1;
            _reviews.Add(newReview);
            await SaveReviewsAsync();
            return newReview;
        }

        public async Task<Review> UpdateReviewAsync(Review updatedReview)
        {
            await EnsureReviewsLoadedAsync();
            var existingReview = _reviews.FirstOrDefault(r => r.Id == updatedReview.Id);
            if (existingReview != null)
            {
                existingReview.UserId = updatedReview.UserId;
                existingReview.GameId = updatedReview.GameId;
                existingReview.Title = updatedReview.Title;
                existingReview.Comment = updatedReview.Comment;
                existingReview.GameStatus = updatedReview.GameStatus;
                existingReview.Graphics = updatedReview.Graphics;
                existingReview.Gameplay = updatedReview.Gameplay;
                existingReview.Sound = updatedReview.Sound;
                existingReview.PlotAndLore = updatedReview.PlotAndLore;
                existingReview.Impression = updatedReview.Impression;
                existingReview.Immersive = updatedReview.Immersive;
                existingReview.Replayability = updatedReview.Replayability;

                await SaveReviewsAsync();
                return existingReview;
            }
            return null;
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            await EnsureReviewsLoadedAsync();
            var reviewToRemove = _reviews.FirstOrDefault(r => r.Id == reviewId);
            if (reviewToRemove != null)
            {
                _reviews.Remove(reviewToRemove);
                await SaveReviewsAsync();
            }
        }

        public async Task<Review?> GetReviewByIdAsync(int reviewId)
        {
            await EnsureReviewsLoadedAsync();
            return _reviews.FirstOrDefault(r => r.Id == reviewId);
        }

        public async Task<ObservableCollection<Review>> GetReviewsAsync()
        {
            await EnsureReviewsLoadedAsync();
            return _reviews;
        }
    }
}
