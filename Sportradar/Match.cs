namespace Sportradar;

public class Match(int id, string homeTeam, string awayTeam)
{
    public int Id { get; set; } = id;
    public string HomeTeam { get; private set; } = homeTeam;
    public string AwayTeam { get; private set; } = awayTeam;
    public int HomeTeamScore { get; private set; } = 0;
    public int AwayTeamScore { get; private set; } = 0;
    public bool IsOver { get; set; } = false;

    public void UpdateScore(int homeTeamScore, int awayTeamScore)
    {
        HomeTeamScore = homeTeamScore;
        AwayTeamScore = awayTeamScore;
    }
}