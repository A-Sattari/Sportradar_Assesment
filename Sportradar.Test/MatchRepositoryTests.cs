namespace Sportradar.Test;

public class MatchRepositoryTests
{
    private readonly MatchRepository matchRepo;

    public MatchRepositoryTests()
    {
        matchRepo = new MatchRepository();
    }

    [Fact]
    public void Add_MatchIsAdded()
    {
        // Arrange
        var match = new Match(1, "Team A", "Team B");

        // Act
        matchRepo.Add(match);

        // Assert
        var retrievedMatch = matchRepo.Get(1);
        Assert.NotNull(retrievedMatch);
        Assert.Equal("Team A", retrievedMatch.HomeTeam);
        Assert.Equal("Team B", retrievedMatch.AwayTeam);
    }

    [Fact]
    public void Remove_MatchIsRemoved()
    {
        // Arrange
        var match = new Match(1, "Team A", "Team B");
        matchRepo.Add(match);

        // Act
        matchRepo.Remove(1);

        // Assert
        var retrievedMatch = matchRepo.Get(1);
        Assert.Null(retrievedMatch);
    }

    [Fact]
    public void Get_NonExistingMatch_ReturnsNull()
    {
        // Act
        var retrievedMatch = matchRepo.Get(999);

        // Assert
        Assert.Null(retrievedMatch);
    }

    [Fact]
    public void GetAll_ReturnsAllMatches()
    {
        // Arrange
        var match1 = new Match(1, "Team A", "Team B");
        var match2 = new Match(2, "Team C", "Team D");
        matchRepo.Add(match1);
        matchRepo.Add(match2);

        // Act
        var matches = matchRepo.GetAll().ToList();

        // Assert
        Assert.Equal(2, matches.Count);
        Assert.Contains(matches, m => m.Id == 1);
        Assert.Contains(matches, m => m.Id == 2);
    }
}