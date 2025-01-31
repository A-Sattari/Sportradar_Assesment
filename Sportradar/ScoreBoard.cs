namespace Sportradar;

public class ScoreBoard {

    public Match StartMatch(string homeTeam, string awayTeam)
    {
        throw new NotImplementedException();
    }

    public void EndMatch(int matchId)
    {
        throw new NotImplementedException();
    }

    public void UpdateScore(int matchId, int homeScore, int awayScore)
    {
        throw new NotImplementedException();
    }

    public IList<Match> GetSummary()
    {
        throw new NotImplementedException();
    }
}