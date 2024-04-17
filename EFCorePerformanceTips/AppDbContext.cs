global using Microsoft.EntityFrameworkCore;

namespace EFCorePerformanceTips;

internal class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Post> Posts { get; set; }
}

internal class Author
{
    public int Id { get; private set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public List<Post> Posts { get; init; } = [];
}

internal class Post
{
    public int Id { get; private set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public required Author Author { get; init; }
    public bool Archived { get; set; }
}