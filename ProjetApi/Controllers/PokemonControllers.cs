using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProjetApi.Context;
using ProjetApi.Entities;

namespace ProjetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PokemonController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // Get All Pokemon
        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetAll()
        {
            return Ok(await _context.Pokemons.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddPokemon([FromBody] Pokemon pokemon)
        {
            if(pokemon == null){
                return BadRequest();
            }
            await _context.Pokemons.AddAsync(pokemon);
            await _context.SaveChangesAsync();
            return Ok("Pokemon créer !");
        }
    }
}