using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AnimeAPI.Models;
using AnimeAPI.Services;
using Xunit;

namespace AnimeAPI.Tests
{
    public class AnimeServiceTests
    {
        private string GetTempFilePath()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".json");
        }

        private void InitializeTempFile(string filePath, List<Anime> initialData)
        {
            var json = JsonSerializer.Serialize(initialData);
            File.WriteAllText(filePath, json);
        }

        [Fact]
        public void GetAnimes_ReturnsAllAnimes()
        {
            var filePath = GetTempFilePath();
            InitializeTempFile(filePath, new List<Anime>
            {
                new Anime { Id = 1, Title = "Naruto", Status = "Skoñczone" },
                new Anime { Id = 2, Title = "One Piece", Status = "W trakcie" }
            });

            var service = new AnimeService(filePath);
            var animes = service.GetAnimes();

            Assert.Equal(2, animes.Count);
            Assert.Equal("Naruto", animes[0].Title);
            Assert.Equal("One Piece", animes[1].Title);

            File.Delete(filePath);
        }

        [Fact]
        public void GetAnime_ReturnsCorrectAnime()
        {
            var filePath = GetTempFilePath();
            InitializeTempFile(filePath, new List<Anime>
            {
                new Anime { Id = 1, Title = "Naruto", Status = "Skoñczone" },
                new Anime { Id = 2, Title = "One Piece", Status = "W trakcie" }
            });

            var service = new AnimeService(filePath);
            var anime = service.GetAnime(1);

            Assert.NotNull(anime);
            Assert.Equal("Naruto", anime.Title);

            File.Delete(filePath);
        }

        [Fact]
        public void AddAnime_AddsNewAnime()
        {
            var filePath = GetTempFilePath();
            InitializeTempFile(filePath, new List<Anime>());

            var service = new AnimeService(filePath);
            var newAnime = new Anime { Title = "Naruto", Status = "Skoñczone" };

            service.AddAnime(newAnime);
            var animes = service.GetAnimes();

            Assert.Single(animes);
            Assert.Equal("Naruto", animes[0].Title);

            File.Delete(filePath);
        }

        [Fact]
        public void DeleteAnime_RemovesAnime()
        {
            var filePath = GetTempFilePath();
            InitializeTempFile(filePath, new List<Anime>
            {
                new Anime { Id = 1, Title = "Naruto", Status = "Skoñczone" },
                new Anime { Id = 2, Title = "One Piece", Status = "W trakcie" }
            });

            var service = new AnimeService(filePath);
            service.DeleteAnime(1);
            var animes = service.GetAnimes();

            Assert.Single(animes);
            Assert.Equal("One Piece", animes[0].Title);

            File.Delete(filePath);
        }
    }
}
