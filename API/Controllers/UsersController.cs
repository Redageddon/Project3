using API.DataModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(UserRepository userRepository) : ControllerBase
{
    // GET: api/users
    [HttpGet]
    public ActionResult<List<UserDto>> GetAll()
    {
        UsersData users = userRepository.GetAllUsers();
        List<UserDto> userDtos = users.Users.Select(u => new UserDto(u.Uid, u.Username, u.Email)).ToList();

        return this.Ok(userDtos);
    }

    // GET: api/users/5
    [HttpGet("{uid:int}")]
    public ActionResult<UserDto> GetById(int uid)
    {
        UserModel? user = userRepository.GetUserById(uid);

        if (user == null)
        {
            return this.NotFound(new { message = $"User with ID {uid} not found" });
        }

        UserDto userDto = new(user.Uid, user.Username, user.Email);

        return this.Ok(userDto);
    }

    // DELETE: api/users/5
    [HttpDelete("{uid:int}")]
    public ActionResult Delete(int uid)
    {
        bool success = userRepository.DeleteUser(uid);

        if (!success)
        {
            return this.NotFound(new { message = $"User with ID {uid} not found" });
        }

        return this.NoContent();
    }
}