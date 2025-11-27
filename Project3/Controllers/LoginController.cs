using System.Diagnostics;
using API.DataModels.Users;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> logger;
    private readonly AuthApiService authApiService;

    public LoginController(ILogger<LoginController> logger, AuthApiService authApiService)
    {
        this.logger = logger;
        this.authApiService = authApiService;
    }

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
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        LoginRequest request = new(email, password);

        if (!this.ModelState.IsValid)
        {
            return this.View("Index");
        }

        LoginResponse response = await this.authApiService.Login(request);

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
    public async Task<IActionResult> Register([FromForm] string username, [FromForm] string email, [FromForm] string password)
    {
        RegisterRequest request = new(username, email, password);

        if (!this.ModelState.IsValid)
        {
            return this.View("Index");
        }

        LoginResponse response = await this.authApiService.Register(request);

        if (!response.Success)
        {
            this.ModelState.AddModelError(string.Empty, response.Message);
            
            return this.View("Index");
        }

        // No session here – user must log in with their new account
        this.TempData["RegisterSuccess"] = "Account created. Please log in.";

        return this.RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ErrorViewModel errorViewModel = new()
        {
            RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
        };

        this.logger.LogError("Error");

        return this.View(errorViewModel);
    }
}
