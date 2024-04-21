using Che.Result.Test.Posts;

namespace Che.Result.Test;
public class ResultTests
{
    [Fact]
    public void CreatePost_Success()
    {
        // Arrange
        var postResult = Post.Crete("Not empty title", "Not empty body", DateTimeOffset.UtcNow.AddSeconds(-5));

        // Act
        Post? post = postResult.Value;
        Error error = postResult.Error;

        // Assert
        Assert.True(postResult.IsSuccess);
        Assert.Equal(error, Error.None);
        Assert.NotNull(post);
    }

    [Fact]
    public void CreatePost_Failure_Title()
    {
        // Arrange
        var postResult = Post.Crete("      ", "    ", DateTimeOffset.UtcNow.AddMinutes(5));

        // Act
        Post? post = postResult.Value;
        Error error = postResult.Error;

        // Assert
        Assert.True(postResult.IsFailure);
        Assert.Equal(error, PostErrors.TitleAreEmpty);
        Assert.Null(post);
    }

    [Fact]
    public void CreatePost_Failure_Body()
    {
        // Arrange
        var postResult = Post.Crete("   some title  ", "   ", DateTimeOffset.UtcNow.AddMinutes(5));

        // Act
        Post? post = postResult.Value;
        Error error = postResult.Error;

        // Assert
        Assert.True(postResult.IsFailure);
        Assert.Equal(error, PostErrors.BodyAreEmpty);
        Assert.Null(post);
    }

    [Fact]
    public void CreatePost_Failure_CreateAt()
    {
        // Arrange
        var postResult = Post.Crete("   some title  ", " some body   ", DateTimeOffset.UtcNow.AddMinutes(5));

        // Act
        Post? post = postResult.Value;
        Error error = postResult.Error;

        // Assert
        Assert.True(postResult.IsFailure);
        Assert.Equal(error, PostErrors.CreateAtInvalid);
        Assert.Null(post);
    }
}
