using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AnimeAPI.Models;

namespace AnimeAPI.Services
{
    public class AnimeService
    {
        private readonly string _filePath;

        public AnimeService()
        {
            // Ustawienie ścieżki do pliku animes.json w katalogu data
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            _filePath = Path.Combine(directory, "anime.json");

            // Upewnij się, że plik istnieje, jeśli nie, utwórz go
            if (!File.Exists(_filePath))
            {
                Directory.CreateDirectory(directory); // Utwórz katalog, jeśli nie istnieje
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Anime> GetAnimes()
        {
            var json = File.ReadAllText(_filePath);
            var animeList = JsonSerializer.Deserialize<List<Anime>>(json);

            // Utwórz nową listę Anime zawierającą tylko pola Id, Title i Status
            var simplifiedAnimeList = animeList.Select(a => new Anime
            {
                Id = a.Id,
                Title = a.Title,
                Status = a.Status
            }).ToList();

            return simplifiedAnimeList;
        }

        public Anime GetAnime(int id)
        {
            var animes = GetAnimes();
            return animes.FirstOrDefault(a => a.Id == id);
        }

        public void AddAnime(Anime anime)
        {
            var animes = GetAnimes();
            anime.Id = animes.Count == 0 ? 1 : animes.Max(a => a.Id) + 1;
            animes.Add(anime);
            SaveAnimes(animes);
        }

        public void UpdateAnime(int id, Anime updatedAnime)
        {
            var animes = GetAnimes();
            var anime = animes.FirstOrDefault(a => a.Id == id);
            if (anime != null)
            {
                anime.Title = updatedAnime.Title;
                anime.Status = updatedAnime.Status;
                SaveAnimes(animes);
            }
        }

        public void DeleteAnime(int id)
        {
            var animes = GetAnimes();
            var anime = animes.FirstOrDefault(a => a.Id == id);
            if (anime != null)
            {
                animes.Remove(anime);
                SaveAnimes(animes);
            }
        }

        private void SaveAnimes(List<Anime> animes)
        {
            var json = JsonSerializer.Serialize(animes);
            File.WriteAllText(_filePath, json);
        }
    }
}
