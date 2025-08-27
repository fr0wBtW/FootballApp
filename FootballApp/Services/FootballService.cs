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

        public async Task<List<Match>> GetPremierLeagueMatchesAsync()
        {
            var response = await _httpClient.GetAsync("https://api.football-data.org/v4/competitions/PL/matches?status=SCHEDULED");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<MatchResponse>(json, options);

            var now = DateTime.UtcNow;
            var weekAhead = now.AddDays(7);

            return (result?.Matches ?? new List<Match>()).Where(m => m.UtcDate >= now && m.UtcDate <= weekAhead).ToList();
        }
    }
}
