using System.Diagnostics;
using API.DataModels.Food;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class RecipesController(ILogger<RecipesController> logger, RecipeApiService recipeApiService) : Controller
{
    public async Task<IActionResult> Index([FromQuery] RecipeFilter recipeFilter)
    {
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();

        recipesData = new RecipesDataModel(recipeFilter.Filter(recipesData.Recipes));

        return this.View(recipesData);
    }

    [Route("Recipes/{id:int}")]
    public async Task<IActionResult> RecipesIndividual(int id)
    {
        RecipeModel? recipe = await recipeApiService.GetRecipeById(id);

        if (recipe == null)
        {
            return this.NotFound();
        }

        return this.View(recipe);
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