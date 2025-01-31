namespace Sportradar.Test;

public class ScoreBoardTests
{
    private readonly ScoreBoard scoreBoard;
    private readonly IMatchRepository matchRepo;

    public ScoreBoardTests()
    {
        matchRepo = new MatchRepository();
        scoreBoard = new ScoreBoard(matchRepo);
    }

    [Fact]
    public void StartMatch_MatchWithZeroScore()
    {
        // Arrange & Act
        const string HomeName = "Mexico";
        const string AwayName = "Mexico";
        var match = scoreBoard.StartMatch(HomeName, AwayName);

        // Assert
        Assert.Equal(HomeName, match.HomeTeam);
        Assert.Equal(AwayName, match.AwayTeam);
        Assert.Equal(0, match.HomeScore);
        Assert.Equal(0, match.AwayScore);
    }

    [Fact]
    public void StartMatch_NoTeamName_ThrowsException()
    {
        Assert.Throws<ArgumentNullException>(() => scoreBoard.StartMatch("", "Canada"));
        Assert.Throws<ArgumentNullException>(() => scoreBoard.StartMatch("Mexico", ""));
        Assert.Throws<ArgumentNullException>(() => scoreBoard.StartMatch("", ""));
    }

    [Fact]
    public void EndGame_MatchIsRemovedFromBoard()
    {
        // Arrange
        var match = scoreBoard.StartMatch("UAE", "Thailand");

        // Act
        scoreBoard.EndMatch(match.Id);

        // Assert
    }

    [Fact]
    public void UpdateScore_SuccessfullyUpdated()
    {
        // Arrange
        var match = scoreBoard.StartMatch("Japan", "Iran");

        // Act
        scoreBoard.UpdateScore(match.Id, 0, 5);

        // Assert
        Assert.Equal(0, match.HomeScore);
        Assert.Equal(5, match.AwayScore);
    }

    [Fact]
    public void UpdateScore_NonExistingTeam_ThrowsException()
    {
        // Arrange
        scoreBoard.StartMatch("Japan", "Iran");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => scoreBoard.UpdateScore(505, 1, 1));
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    public void UpdateScore_NegativeScore_ThrowsException(int homeScore, int awayScore)
    {
        var match = scoreBoard.StartMatch("Mexico", "Canada");
        Assert.Throws<ArgumentException>(() => scoreBoard.UpdateScore(match.Id, homeScore, awayScore));
    }

    [Fact]
    public void GetSummary_ReturnsMatchOrderedByTotalScoreAndMostRecent()
    {
        // Arrange
        var match1 = scoreBoard.StartMatch("Espana", "Canada");
        var match2 = scoreBoard.StartMatch("Brazil", "Argentina");
        var match3 = scoreBoard.StartMatch("Germany", "Poland");
        var match4 = scoreBoard.StartMatch("Uruguay", "Italy");
        var match5 = scoreBoard.StartMatch("Iraq", "Australia");

        scoreBoard.UpdateScore(match1.Id, 0, 5);
        scoreBoard.UpdateScore(match2.Id, 10, 2); // Brazil vs. Argentina
        scoreBoard.UpdateScore(match3.Id, 2, 2);
        scoreBoard.UpdateScore(match4.Id, 6, 6); // Uruguay vs. Italy
        scoreBoard.UpdateScore(match5.Id, 3, 1);

        // Act
        var summary = scoreBoard.GetSummary().ToList();

        // Assert
        Assert.Equal(5, summary.Count);
        Assert.Equal("Uruguay", summary[0].HomeTeam);
        Assert.Equal("Italy", summary[0].AwayTeam);
        Assert.Equal("Brazil", summary[1].HomeTeam);
        Assert.Equal("Argentina", summary[1].AwayTeam);
    }
}