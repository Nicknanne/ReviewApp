using System.Collections.ObjectModel;
using ReviewApp.Models;

namespace ReviewApp.Services
{
    public interface IGamesService
    {
        Task<Game?> GetGameByIdAsync(int gameId);
        Task<Game> AddGameAsync(Game game);
        Task<Game> UpdateGameAsync(Game game);
        Task DeleteGameAsync(int gameId);

        Task<ObservableCollection<Game>> GetGamesAsync();
    }
}
