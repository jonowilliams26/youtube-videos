namespace EFCorePerformanceTips;

internal class Tip4_UseExecuteForEfficientBulkUpdatesAndDeletes(AppDbContext database)
{
    private void Bad_ArchivePostsByAuthor(int authorId)
    {
        var posts = database.Posts
            .Where(x => x.Author.Id == authorId)
            .ToList();

        posts.ForEach(x => x.Archived = true);

        database.SaveChanges();
    }

    private void Good_ArchivePostsByAuthor(int authorId)
    {
        database.Posts
            .Where(x => x.Author.Id == authorId)
            .ExecuteUpdate(x => x
                .SetProperty(post => post.Archived, true)
                .SetProperty(post => post.UpdatedAt, DateTime.UtcNow)
            );
    }
}