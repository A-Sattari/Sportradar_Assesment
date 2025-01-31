namespace Sportradar;

public class Match(int id, string homeTeam, string awayTeam)
{
    public int Id { get; set; } = id;
    public string HomeTeam { get; private set; } = homeTeam;
    public string AwayTeam { get; private set; } = awayTeam;
    public int HomeScore { get; private set; } = 0;
    public int AwayScore { get; private set; } = 0;

    public void UpdateScore(int homeTeamScore, int awayTeamScore)
    {
        HomeScore = homeTeamScore;
        AwayScore = awayTeamScore;
    }
}