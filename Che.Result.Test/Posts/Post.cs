namespace Che.Result.Test.Posts;
internal class Post
{
    public string Title { get; }
    public string Body { get; }
    public DateTimeOffset CreateAt { get; }

    private Post(string title, string body, DateTimeOffset createAt)
    {
        Title = title;
        Body = body;
        CreateAt = createAt;
    }

    public static Result<Post> Crete(string title, string body, DateTimeOffset createAt)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result<Post>.Failure(PostErrors.TitleAreEmpty);
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            return Result<Post>.Failure(PostErrors.BodyAreEmpty);
        }

        if (createAt.UtcDateTime > DateTime.UtcNow)
        {
            return Result<Post>.Failure(PostErrors.CreateAtInvalid);
        }

        return Result<Post>.Success(new(title.Trim(), body.Trim(), createAt));
    }
}
