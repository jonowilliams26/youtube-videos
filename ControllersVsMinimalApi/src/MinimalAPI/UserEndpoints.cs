using Microsoft.AspNetCore.Http.HttpResults;

namespace ControllersVsMinimalApi.MinimalAPI;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/minimal/users")
            .WithOpenApi();

        endpoints.MapGet("/", GetAll)
            .WithSummary("Gets all users");
        endpoints.MapGet("/{id}", Get)
            .WithSummary("Gets a user by id");
        endpoints.MapPost("/", Post)
            .WithSummary("Creates a user");
        endpoints.MapPut("/", Put)
            .WithSummary("Updates a user");
        endpoints.MapDelete("/{id}", Delete)
            .WithSummary("Deletes a user");
    }

    static List<User> GetAll(IUserService userService)
    {
        return userService.GetUsers();
    }

    static Results<Ok<User>, NotFound> Get(int id, IUserService userService)
    {
        var user = userService.GetUser(id);
        return user is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(user);
    }

    static Created Post(User user, IUserService userService)
    {
        userService.CreateUser(user.Name);
        return TypedResults.Created();
    }

    static Results<Ok, NotFound> Put(User updatedUser, IUserService userService)
    {
        var user = userService.UpdateUser(updatedUser);
        return user is null
            ? TypedResults.NotFound()
            : TypedResults.Ok();
    }

    static Results<Ok, NotFound> Delete(int id, IUserService userService)
    {
        var deleted = userService.DeleteUser(id);
        return deleted
            ? TypedResults.NotFound()
            : TypedResults.Ok();
    }
}
