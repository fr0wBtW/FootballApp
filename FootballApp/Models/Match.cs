namespace FootballApp.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime UtcDate { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Competition Competition { get; set; }
    }
}
