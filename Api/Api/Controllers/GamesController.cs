using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace VideoGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly List<Games> Games = new List<Games>
        {
            new Games { Id = 1, Name = "Game1", Valor = 50, ValorAluguel = 5, Genero = "Ação", TipoDeMidia = "Digital" },
            new Games { Id = 2, Name = "Game2", Valor = 40, ValorAluguel = 4, Genero = "Aventura", TipoDeMidia = "Física" },
            new Games { Id = 3, Name = "Game3", Valor = 80, ValorAluguel = 4, Genero = "Aventura", TipoDeMidia = "Física" }
        };

        private static int nextId = 3;

        // GET: api/games
        [HttpGet]
        public ActionResult<IEnumerable<Games>> GetGames()
        {
            return Games;
        }

        // GET: api/games/5
        [HttpGet("{id}")]
        public ActionResult<Games> GetGame(int id)
        {
            var game = Games.FirstOrDefault(p => p.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return game;
        }

        // POST: api/games
        [HttpPost]
        public ActionResult<Games> CreateGame(Games game)
        {
            game.Id = nextId++;
            Games.Add(game);
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
        }

        // PUT: api/games/5
        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, Games game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            var existingGame = Games.FirstOrDefault(p => p.Id == id);
            if (existingGame == null)
            {
                return NotFound();
            }

            existingGame.Name = game.Name;
            existingGame.Valor = game.Valor;
            existingGame.ValorAluguel = game.ValorAluguel;
            existingGame.Genero = game.Genero;
            existingGame.TipoDeMidia = game.TipoDeMidia;

            return NoContent();
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            var game = Games.FirstOrDefault(p => p.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            Games.Remove(game);
            return NoContent();
        }
    }
}
