using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using RequestValidationInMinimalAPIs.Filters;
using RequestValidationInMinimalAPIs.Services;

namespace RequestValidationInMinimalAPIs;

public static class Endpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/posts")
            .WithOpenApi();

        endpoints.MapGet("/", GetPosts)
            .WithSummary("Get all posts");

        endpoints.MapPost("/", CreatePost)
            .WithSummary("Create a new post")
            .WithRequestValidation<CreatePostRequest>();

        endpoints.MapPut("/", UpdatePost)
            .WithSummary("Update an existing post")
            .WithRequestValidation<UpdatePostRequest>();
    }


    public record CreatePostRequest(string Title, string Content);
    public class CreatePostValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }

    public static Ok<Guid> CreatePost(CreatePostRequest request, Database database)
    {
        var post = new Post
        {
            Title = request.Title.Trim(),
            Content = request.Content.Trim()
        };

        database.Posts.Add(post);
        return TypedResults.Ok(post.Id);
    }

    public record UpdatePostRequest(Guid Id, string Title, string Content);
    public class UpdatePostValidator : AbstractValidator<UpdatePostRequest>
    {
        public UpdatePostValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }

    public static Results<Ok, NotFound> UpdatePost(UpdatePostRequest request, Database database)
    {
        var post = database.Posts.FirstOrDefault(x => x.Id == request.Id);

        if (post is null)
        {
            return TypedResults.NotFound();
        }

        post.Title = request.Title.Trim();
        post.Content = request.Content.Trim();
        return TypedResults.Ok();
    }

    public static List<Post> GetPosts(Database database) => database.Posts;
}