namespace EFCorePerformanceTips;

internal class Tip3_AvoidTheCartesianExplosion(AppDbContext database)
{
    private Author? Bad_GetAuthorWithPosts(int authorId)
    {
        var author = database.Authors
            .Include(x => x.Posts)
            .Where(x => x.Id == authorId)
            .SingleOrDefault();

        return author;
    }

    private Author? Good_GetAuthorWithPosts(int authorId)
    {
        var author = database.Authors
            .AsSplitQuery()
            .Include(x => x.Posts)
            .Where(x => x.Id == authorId)
            .SingleOrDefault();

        return author;
    }
}