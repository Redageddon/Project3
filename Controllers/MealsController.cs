using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Project3.API;
using Project3.Models;

namespace Project3.Controllers;

public class MealsController(ILogger<MealsController> logger) : Controller
{
    public IActionResult Meals()
    {
        MealsModel meals = BasicAPI.GetRecipes();

        return this.View(meals);
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