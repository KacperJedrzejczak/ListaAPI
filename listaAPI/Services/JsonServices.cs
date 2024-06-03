using System.Collections.Generic;
using System.IO;
using AnimeAPI.Models;
using Newtonsoft.Json;

namespace YourProjectName.Services
{
    public class AnimeService
    {
        private readonly string _filePath;

        public AnimeService(string filePath)
        {
            _filePath = filePath;
        }

        public List<Anime> GetAnimeList()
        {
            // Wczytaj dane z pliku JSON
            string json = File.ReadAllText(_filePath);

            // Deserializuj dane JSON do listy obiektów Anime
            List<Anime> animeList = JsonConvert.DeserializeObject<List<Anime>>(json);

            return animeList;
        }
    }
}
