using System.Diagnostics;
using API.DataModels.Users;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class LoginController(ILogger<LoginController> logger, AuthApiService authApiService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return this.View();
    }

    // LOGIN – this is where we create a session (only for authenticated users)
    [HttpPost]
    
    // [ValidateAntiForgeryToken] makes sure a POST request really came from your own form (not a malicious site),
    //          protecting you from Cross-Site Request Forgery attacks.
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View("Index");
        }

        LoginResponse response = await authApiService.Login(request);

        if (!response.Success || response.User is null || response.SessionId is null)
        {
            this.ModelState.AddModelError(string.Empty, response.Message);
    
            return this.View("Index");
        }

        // Store the API session and user info in MVC session
        this.HttpContext.Session.SetString("SessionId", response.SessionId);
        this.HttpContext.Session.SetInt32("UserId", response.User.Uid);
        this.HttpContext.Session.SetString("Username", response.User.Username);

        // Redirect to Home/dashboard (or wherever a logged-in user should go)
        return this.RedirectToAction("Index", "Home");
    }

    // REGISTER – create user ONLY, does NOT create a session here
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View("Index");
        }

        LoginResponse response = await authApiService.Register(request);

        if (!response.Success)
        {
            this.ModelState.AddModelError(string.Empty, response.Message);
    
            return this.View("Index");
        }
        
        LoginRequest newUser = new(request.Email, request.Password);
        
        return await this.Login(newUser);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        string? sessionId = this.HttpContext.Session.GetString("SessionId");

        if (!string.IsNullOrEmpty(sessionId))
        {
            LogoutRequest request = new(sessionId);

            bool apiSuccess = false;

            try
            {
                apiSuccess = await authApiService.Logout(request);
            }
            catch (Exception ex)
            {
                // Only thrown if something goes wrong calling the API (network, etc.)
                logger.LogError("{Ex} : Error calling Auth API logout for session {SessionId}", ex, sessionId);
            }

            if (!apiSuccess)
            {
                // API responded but NOT with a success status code (e.g. 400, 401, 500)
                logger.LogWarning("Auth API logout returned a non-success status for session {SessionId}", sessionId);
            }
            else
            {
                this.HttpContext.Session.Clear();
                
                return this.RedirectToAction("Index", "Home");
            }
        }
        
        return this.View("Index");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ErrorViewModel errorViewModel = new()
        {
            RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
        };

        logger.LogError("Error");

        return this.View(errorViewModel);
    }
}
