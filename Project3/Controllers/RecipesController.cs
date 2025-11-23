using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Project3.Models;
using Project3.TempAPI;

namespace Project3.Controllers;

public class RecipesController(ILogger<RecipesController> logger) : Controller
{
    // RecipesController.cs
    public IActionResult Recipes() {
        var meals = BasicApi.GetRecipes();

        return this.View(meals); // passes MealsModel to Recipes.cshtml
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