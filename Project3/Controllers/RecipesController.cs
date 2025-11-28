using System.Diagnostics;
using API.DataModels.Food;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class RecipesController(ILogger<RecipesController> logger, RecipeApiService recipeApiService) : Controller
{
    public async Task<IActionResult> Index([FromQuery] RecipeFilter filter)
    {
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();

        List<RecipeModel> filteredRecipes = FilterRecipes(recipesData.Recipes, filter);

        recipesData = new RecipesDataModel(filteredRecipes);

        return this.View(recipesData);
    }

    private static List<RecipeModel> FilterRecipes(IEnumerable<RecipeModel> recipes, RecipeFilter filter)
    {
        return recipes
               .Where(r => r.Name.MatchesText(filter.Name))
               .Where(r => r.MealType.MatchesAny(filter.MealType))
               .Where(r => r.Tags.MatchesAll(filter.Tag))
               .Where(r => r.Ingredients.ContainsSubstring(filter.Ingredients))
               .Where(r => r.Difficulty.MatchesList(filter.Difficulty))
               .Where(r => r.Cuisine.MatchesList(filter.Cuisine))
               .Where(r => r.Servings.MatchesRange(filter.ServingsLower, filter.ServingsUpper))
               .Where(r => r.Rating.MatchesRange(filter.RatingLower, filter.RatingUpper))
               .Where(r => r.ReviewCount.MatchesRange(filter.ReviewCountLower, filter.ReviewCountUpper))
               .Where(r => r.PrepTimeMinutes.MatchesRange(filter.PrepTimeMinutesLower, filter.PrepTimeMinutesUpper))
               .Where(r => r.CookTimeMinutes.MatchesRange(filter.CookTimeMinutesLower, filter.CookTimeMinutesUpper))
               .Where(r => r.CaloriesPerServing.MatchesRange(filter.CaloriesLower, filter.CaloriesUpper))
               .ToList();
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