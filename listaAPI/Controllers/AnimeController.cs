using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimeAPI.Models;
using AnimeAPI.Services;

namespace ListaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeService _animeService;

        public AnimeController()
        {
            _animeService = new AnimeService();
        }

        // GET: api/Anime
        [HttpGet]
        public ActionResult<IEnumerable<Anime>> GetAnimes()
        {
            return _animeService.GetAnimes();
        }

        // GET: api/Anime/5
        [HttpGet("{id}")]
        public ActionResult<Anime> GetAnime(int id)
        {
            var anime = _animeService.GetAnime(id);
            if (anime == null)
            {
                return NotFound();
            }
            return anime;
        }

        // POST: api/Anime
        [HttpPost]
        public ActionResult<Anime> PostAnime(Anime anime)
        {
            _animeService.AddAnime(anime);
            return CreatedAtAction(nameof(GetAnime), new { id = anime.Id }, anime);
        }

        // PUT: api/Anime/5
        [HttpPut("{id}")]
        public IActionResult PutAnime(int id, Anime anime)
        {
            if (id != anime.Id)
            {
                return BadRequest();
            }

            var existingAnime = _animeService.GetAnime(id);
            if (existingAnime == null)
            {
                return NotFound();
            }

            _animeService.UpdateAnime(id, anime);
            return NoContent();
        }

        // DELETE: api/Anime/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAnime(int id)
        {
            var existingAnime = _animeService.GetAnime(id);
            if (existingAnime == null)
            {
                return NotFound();
            }

            _animeService.DeleteAnime(id);
            return NoContent();
        }
    }
}
