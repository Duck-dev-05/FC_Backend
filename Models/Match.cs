namespace FootballClubBE.Models
{
    public class Match
    {
        public string Id { get; set; } = string.Empty; // Default value to avoid warnings
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty; // e.g., "1-0", "2-2", etc.
    }
} 