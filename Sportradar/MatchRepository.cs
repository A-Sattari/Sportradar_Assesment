namespace Sportradar;

public class MatchRepository : IMatchRepository
{
    readonly Dictionary<int, Match> matches = [];

    public void Add(Match match)
    {
        matches.Add(match.Id, match);
    }

    public void Remove(int id)
    {
        matches.Remove(id);
    }

    public Match? Get(int id)
    {
        return matches.TryGetValue(id, out var match) ? match : null;
    }

    public IEnumerable<Match> GetAll()
    {
        return matches.Values;
    }
}