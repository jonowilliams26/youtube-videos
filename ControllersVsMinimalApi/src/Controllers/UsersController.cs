using Microsoft.AspNetCore.Mvc;

namespace ControllersVsMinimalApi.Controllers;

[ApiController]
[Route("controllers/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService userService = userService;

    /// <summary>
    /// Gets all users
    /// </summary>
    [HttpGet]
    public List<User> Get()
    {
        return userService.GetUsers();
    }

    /// <summary>
    /// Gets a user by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        var user = userService.GetUser(id);
        return user is null
            ? NotFound()
            : Ok(user);
    }

    /// <summary>
    /// Creates a user
    /// </summary>
    [HttpPost]
    public CreatedResult Post(User user)
    {
        userService.CreateUser(user.Name);
        return Created();
    }

    /// <summary>
    /// Updates a User
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(User updatedUser)
    {
        var user = userService.UpdateUser(updatedUser);
        return user is null
            ? NotFound()
            : Ok();
    }

    /// <summary>
    /// Deletes a User
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var deleted = userService.DeleteUser(id);
        return deleted
            ? NotFound()
            : Ok();
    }
}