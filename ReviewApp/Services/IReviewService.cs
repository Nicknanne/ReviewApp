using System.Collections.ObjectModel;
using ReviewApp.Models;

namespace ReviewApp.Services
{
    public interface IReviewService
    {
        Task<ObservableCollection<Review>> GetReviewsForGameAsync(int gameId);
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review updatedReview);
        Task DeleteReviewAsync(int reviewId);
        Task<Review?> GetReviewByIdAsync(int reivewId);
        Task<ObservableCollection<Review>> GetReviewsAsync();
    }
}
