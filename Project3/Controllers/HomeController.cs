using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;

namespace Project3.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
        return this.View();
    }

    public IActionResult Privacy()
    {
        return this.View();
    }

    public IActionResult Recipes()
    {
        return this.View();
    }

    // public IActionResult Meals()
    // {
    //     return this.View();
    // }
    //
    // public IActionResult Login()
    // {
    //     return this.View();
    // }
    // public IActionResult Planner()
    // {
    //     return this.View();
    // }

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