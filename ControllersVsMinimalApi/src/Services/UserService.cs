namespace ControllersVsMinimalApi.Services;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public interface IUserService
{
    User CreateUser(string name);
    bool DeleteUser(int userId);
    User? GetUser(int userId);
    List<User> GetUsers();
    User? UpdateUser(User updated);
}

public class UserService : IUserService
{
    public List<User> GetUsers()
    {
        return
        [
            new User { Id = 1, Name = "User 1" },
            new User { Id = 2, Name = "User 2" },
            new User { Id = 3, Name = "User 3" }
        ];
    }

    public User? GetUser(int userId) => new()
    {
        Id = userId,
        Name = $"User {userId}"
    };

    public User CreateUser(string name)
    {
        return new() { Name = name };
    }

    public User? UpdateUser(User updated)
    {
        return new User() { Id = updated.Id, Name = updated.Name };
    }

    public bool DeleteUser(int userId)
    {
        return true;
    }
}