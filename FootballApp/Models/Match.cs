namespace FootballApp.Models
{
    public class Match
    {
        public DateTime UtcDate { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}
