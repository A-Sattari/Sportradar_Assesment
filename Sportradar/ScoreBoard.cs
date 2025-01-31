namespace Sportradar;

public class ScoreBoard(IMatchRepository matchRepository)
{
    private readonly IMatchRepository matchRepo = matchRepository;
    private int nextMatchId = 1;

    public Match StartMatch(string homeTeam, string awayTeam)
    {
        if (string.IsNullOrWhiteSpace(homeTeam) || string.IsNullOrWhiteSpace(awayTeam))
        {
            throw new ArgumentNullException("Team names cannot be empty");
        }
        var match = new Match(nextMatchId++, homeTeam, awayTeam);
        matchRepo.Add(match);
        return match;
    }

    public void EndMatch(int matchId)
    {
        matchRepo.Remove(matchId);
    }

    public void UpdateScore(int matchId, int homeScore, int awayScore)
    {
        if (homeScore < 0 || awayScore < 0)
        {
            throw new ArgumentException("Score cannot be negative");
        }

        var match = matchRepo.Get(matchId) ?? throw new ArgumentException("Match not found");
        match?.UpdateScore(homeScore, awayScore);
    }

    public IEnumerable<Match> GetSummary()
    {
        return matchRepo.GetAll()
            .OrderByDescending(m => m.HomeScore + m.AwayScore)
            .ThenByDescending(m => m.Id);
    }
}