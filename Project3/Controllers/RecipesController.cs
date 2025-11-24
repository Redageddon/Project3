using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using API.DataModels.Food;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class RecipesController(ILogger<RecipesController> logger, RecipeApiService recipeApiService) : Controller
{
    public async Task<IActionResult> Recipes()
    {
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();

        return this.View(recipesData);
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