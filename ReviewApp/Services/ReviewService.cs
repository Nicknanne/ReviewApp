using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using ReviewApp.Models;

namespace ReviewApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly string _filePath;
        private ObservableCollection<Review> _reviews;

        public ReviewService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "reviews.json");
            _reviews = new ObservableCollection<Review>();

            _ = LoadReviewsAsync();
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
                Debug.WriteLine($"Error loading reviews: {ex.Message}");
                _reviews.Clear();
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

        public async Task<ObservableCollection<Review>> GetReviewsForGameAsync(int gameId)
        {
            return new ObservableCollection<Review>(_reviews.Where(r => r.GameId == gameId));
        }

        public async Task<Review> AddReviewAsync(Review newReview)
        {
            newReview.Id = _reviews.Any() ? _reviews.Max(r => r.Id) + 1 : 1;
            _reviews.Add(newReview);
            await SaveReviewsAsync();
            return newReview;
        }

        public async Task<Review> UpdateReviewAsync(Review updatedReview)
        {
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
            var reviewToRemove = _reviews.FirstOrDefault(r => r.Id == reviewId);
            if (reviewToRemove != null)
            {
                _reviews.Remove(reviewToRemove);
                await SaveReviewsAsync();
            }
        }

        public async Task<Review?> GetReviewByIdAsync(int reviewId)
        {
            return _reviews.FirstOrDefault(r => r.Id == reviewId);
        }

        public async Task<ObservableCollection<Review>> GetReviewsAsync() => _reviews;
    }
}
