using API.DataModels.Users;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserAuth userAuth) : ControllerBase
{
    // POST: api/auth/register
    [HttpPost("register")]
    public ActionResult<LoginResponse> Register([FromBody] RegisterRequest request)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        LoginResponse response = userAuth.Register(request);

        if (!response.Success)
        {
            return this.BadRequest(response);
        }

        // In a real app, you'd create a session/token here
        return this.Ok(response);
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        LoginResponse response = userAuth.Login(request);

        return !response.Success 
            ? this.StatusCode(401, response) 
            : this.Ok(response);
    }
}