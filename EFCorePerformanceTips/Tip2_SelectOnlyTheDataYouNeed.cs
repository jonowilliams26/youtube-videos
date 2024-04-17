namespace EFCorePerformanceTips;

internal class Tip2_SelectOnlyTheDataYouNeed(AppDbContext database)
{
    record PostSummary(int Id, string Title, string Author);

    private PostSummary[] Bad_GetPostSummaries()
    {
        var posts = database.Posts
            .AsNoTracking()
            .Include(x => x.Author)
            .ToArray();

        return posts.Select(x => new PostSummary(
            Id: x.Id,
            Title: x.Title,
            Author: $"{x.Author.FirstName} {x.Author.LastName}"
        ))
        .ToArray();
    }

    private PostSummary[] Good_GetPostSummaries()
    {
        return database.Posts
            .Select(post => new PostSummary(
                post.Id,
                post.Title,
                post.Author.FirstName + " " + post.Author.LastName
            ))
            .ToArray();
    }
}