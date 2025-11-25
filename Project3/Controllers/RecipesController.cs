using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using API.DataModels.Food;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class RecipesController(ILogger<RecipesController> logger, RecipeApiService recipeApiService) : Controller
{
    public async Task<IActionResult> Index(string? mealType, string? name)
    {
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();

        List<RecipeModel> filteredRecipes = FilterRecipes(mealType, name, recipesData.Recipes, recipesData);

        recipesData = new RecipesDataModel(filteredRecipes);
        
        return this.View(recipesData);
    }

    private static List<RecipeModel> FilterRecipes(string? mealType,
                                                   string? name,
                                                   IEnumerable<RecipeModel> recipeModels,
                                                   RecipesDataModel recipesData)
    {
        if (!string.IsNullOrEmpty(mealType))
        {
            recipeModels = recipeModels.Where(recipe => recipe.MealType.Any(type => type.Equals(mealType, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrEmpty(name))
        {
            recipeModels = recipesData.Recipes.Where(recipe => recipe.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        return recipeModels.ToList();
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