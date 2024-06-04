namespace AnimeAPI.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; } // "Watched", "Watching", "Planned"
    }
}
