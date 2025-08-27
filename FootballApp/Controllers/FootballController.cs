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
            var matches = await _footballService.GetPremierLeagueMatchesAsync();
            return View(matches);
        }
    }
}
