using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ReviewApp.Models;
using ReviewApp.Services;

namespace ReviewApp.ViewModels
{
    public partial class AllPublicReviewsViewModel : ObservableObject
    {
        private readonly IReviewService _reviewService;

        [ObservableProperty]
        private string _pageLabel;
        private int _page;
        private int _maxPage;

        private ObservableCollection<Review> _allReviews;

        public ObservableCollection<ReviewItemViewModel> Reviews { get; set; } = new ObservableCollection<ReviewItemViewModel>();

        public AllPublicReviewsViewModel(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        private async Task UpdateGames(int page)
        {
            Reviews.Clear();

            await Task.Run(() =>
            {
                foreach (var review in _allReviews)
                {
                    Reviews.Add(new(review, null));
                }
            });

            PageLabel = $"Page {page + 1}";
        }

        public async Task OnAppearing()
        {
            _allReviews = await _reviewService.GetReviewsAsync();

            _page = 0;
            _maxPage = (int)MathF.Ceiling(_allReviews.Count / 10f);

            await UpdateGames(_page);
        }
    }
}