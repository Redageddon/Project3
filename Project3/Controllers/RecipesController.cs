using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Project3.Models;
using Project3.Services;
using Project3.TempAPI;

namespace Project3.Controllers;

public class RecipesController(ILogger<RecipesController> logger, RecipeApiService recipeApiService) : Controller
{
    public async Task<IActionResult> Recipes()
    {
        MealsModel meals = await recipeApiService.GetRecipes();

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