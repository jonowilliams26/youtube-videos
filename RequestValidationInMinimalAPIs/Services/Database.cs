namespace RequestValidationInMinimalAPIs.Services;

public class Post
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public required string Title { get; set; }
    public required string Content { get; set; }
}

public class Database
{
    private static readonly List<Post> posts = [];
    public List<Post> Posts => posts;
}