using FootballApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballApp.Controllers
{
    public class FootballController : Controller
    {
        private readonly FootballService _footballService;

        public FootballController(FootballService footballService)
        {
            _footballService = footballService;
        }

        public async Task<IActionResult> UpcomingMatches()
        {
            var leagues = new List<string> { "PL", "PD", "SA", "CL", "DED", "BL1" };
            var matches = await _footballService.GetMatchesFromLeagueAsync(leagues);
            return View(matches);
        }
    }
}
