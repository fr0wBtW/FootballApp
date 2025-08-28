using FootballApp.Data;
using FootballApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FootballApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult>AddFavorite(int matchId)
        {
            if (!_context.FavoriteMatches.Any(f => f.MatchId == matchId)) 
            {
                _context.FavoriteMatches.Add(new FavoriteMatches { MatchId = matchId });
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFavorite(int matchId)
        {
            var favorite = _context.FavoriteMatches.FirstOrDefault(f => f.MatchId == matchId);
            if (favorite != null)
            {
                _context.FavoriteMatches.Remove(favorite);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetFavorites()
        {
            var favoriteMatchIds = _context.FavoriteMatches.Select(f => f.MatchId).ToList();
            return Ok(favoriteMatchIds);
        }
    }
}
