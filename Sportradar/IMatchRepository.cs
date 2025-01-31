namespace Sportradar;

public interface IMatchRepository
{
    void Add(Match match);
    void Remove(int id);
    Match? Get(int id);
    IEnumerable<Match> GetAll();
}