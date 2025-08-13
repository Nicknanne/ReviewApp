using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReviewApp.Models;
using ReviewApp.Services;

namespace ReviewApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _pageLabel;
        private int _page;
        private int _maxPage;

        private ObservableCollection<Review> _allReviews;

        private readonly IReviewService _reviewService;
        private readonly ISupabaseService _supabaseService;
        public MainPageViewModel(IReviewService reviewService, ISupabaseService supabaseService)
        {
            _reviewService = reviewService;
            _supabaseService = supabaseService;
        }

        private async Task UpdateGames(int page)
        {
            Reviews.Clear();
            var user = await _supabaseService.GetCurrentUserAsync();

            await Task.Run(() =>
            {
                foreach (var review in _allReviews.Where(x => x.UserId == Guid.Parse(user!.Id!)))
                {
                    Reviews.Add(new(review, null));
                }
            });

            PageLabel = $"Page {page + 1}";
        }

        public ObservableCollection<ReviewItemViewModel> Reviews { get; set; } = new ObservableCollection<ReviewItemViewModel>();

        [RelayCommand]
        private async Task NextPage()
        {
            if (_page == _maxPage - 1)
                return;
            await UpdateGames(++_page);
        }

        [RelayCommand]
        private async Task PreviousPage()
        {
            if (_page == 0)
                return;
            await UpdateGames(--_page);
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