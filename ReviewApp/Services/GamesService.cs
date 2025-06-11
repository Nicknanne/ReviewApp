using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using ReviewApp.Models;

namespace ReviewApp.Services
{
    public class GamesService : IGamesService
    {
        private readonly string _filePath;
        private ObservableCollection<Game> _games;

        public GamesService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "games.json");
            _games = new();

            _ = LoadGamesAsync();
        }

        private async Task LoadGamesAsync()
        {
            if (!File.Exists(_filePath))
            {
                await SaveGamesAsync();
                return;
            }

            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var loadedGames = JsonSerializer.Deserialize<ObservableCollection<Game>>(json);

                if (loadedGames != null)
                {
                    _games.Clear();
                    foreach (var games in loadedGames)
                    {
                        _games.Add(games);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error load games: {ex.Message}");
                _games.Clear();
            }
        }

        private async Task SaveGamesAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(_games.ToList());
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error save games: {ex.Message}");
            }
        }

        public async Task<Game> AddGameAsync(Game game)
        {
            game.Id = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
            _games.Add(game);
            await SaveGamesAsync();
            return game;
        }

        public async Task DeleteGameAsync(int gameId)
        {
            var gameToRemove = _games.FirstOrDefault(g => g.Id == gameId);
            if (gameToRemove != null)
            {
                _games.Remove(gameToRemove);
                await SaveGamesAsync();
            }
        }

        public async Task<Game?> GetGameByIdAsync(int gameId)
        {
            return _games.FirstOrDefault(g => g.Id == gameId);
        }

        public async Task<Game> UpdateGameAsync(Game game)
        {
            var existingGame = _games.FirstOrDefault(g => g.Id == game.Id);
            if (existingGame != null)
            {
                existingGame.Title = game.Title;
                existingGame.ReleaseDate = game.ReleaseDate;
                existingGame.Platforms = game.Platforms;
                existingGame.Genre = game.Genre;
                existingGame.Developer = game.Developer;
                existingGame.Description = game.Description;

                await SaveGamesAsync();
                return game;
            }
            return null;
        }
    }
}
