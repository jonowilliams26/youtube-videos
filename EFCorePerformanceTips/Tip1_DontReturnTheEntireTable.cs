namespace EFCorePerformanceTips;

internal class Tip1_DontReturnTheEntireTable(AppDbContext database)
{
    private Post[] Bad_GetAllPostsByAuthor(int authorId)
    {
        var posts = database.Posts
            .ToArray()
            .Where(post => post.Author.Id == authorId);

        return posts.ToArray();
    }

    private Post[] Good_GetAllPostsByAuthor(int authorId)
    {
        return database.Posts
            .Where(x => x.Author.Id == authorId)
            .ToArray();
    }
}