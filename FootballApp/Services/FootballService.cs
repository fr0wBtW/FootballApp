using FootballApp.Models;
using System.Text.Json;

namespace FootballApp.Services
{
    public class FootballService
    {
        private readonly HttpClient _httpClient;

        public FootballService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Match>> GetMatchesFromLeagueAsync(List<string> competitionCodes)
        {
            var now = DateTime.UtcNow;
            var weekAhead = now.AddDays(7);

            var allMatches = new List<Match>();

            foreach (var code in competitionCodes)
            {
                var url = $"https://api.football-data.org/v4/competitions/{code}/matches?status=SCHEDULED";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<MatchResponse>(json, options);

                if (result?.Matches != null)
                {
                    var filtered = result.Matches.Where(m => m.UtcDate >= now && m.UtcDate <= weekAhead).ToList();

                    allMatches.AddRange(filtered);
                }
            }
            return allMatches.OrderBy(m => competitionCodes.IndexOf(m.Competition.Code) == -1 ? int.MaxValue :
             competitionCodes.IndexOf(m.Competition.Code)).ThenBy(m => m.UtcDate).ToList();
        }
    }
}
