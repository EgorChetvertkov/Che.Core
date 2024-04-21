namespace Che.Result.Test.Posts;
public static class PostErrors
{
    public static Error TitleAreEmpty => new(
        nameof(TitleAreEmpty),
        "Title are empty");
    public static Error BodyAreEmpty => new(
        nameof(BodyAreEmpty),
        "Body are empty");
    public static Error CreateAtInvalid => new(
        nameof(CreateAtInvalid),
        "Post created in the feature");
}
