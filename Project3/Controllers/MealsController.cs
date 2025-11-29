using System.Diagnostics;
using API.DataModels.Food;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class MealsController(
    ILogger<MealsController> logger,
    MealsApiService mealsApiService,
    RecipeApiService recipeApiService) : Controller
{
    // Helper: get logged-in userId or guest
    private int GetUserIdOrGuest()
    {
        int? userId = this.HttpContext.Session.GetInt32("UserId");
        
        return userId ?? 999999; // Guest
    }

    // GET: /Meals
    public async Task<IActionResult> Index()
    {
        MealsDataModel mealsData = await mealsApiService.GetAllMeals();
        
        return this.View(mealsData);
    }

    // GET: /Meals/{id}
    [Route("Meals/{id:int}")]
    public async Task<IActionResult> MealsIndividual(int id)
    {
        MealsModel? meal = await mealsApiService.GetMealById(id);

        if (meal == null)
        {
            return this.NotFound();
        }

        return this.View(meal);
    }

    // GET: /Meals/CreateMeals
    [HttpGet]
    public async Task<IActionResult> CreateMeals()
    {
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();

        MealCreateViewModel model = new()
        {
            AllRecipes = recipesData.Recipes,
        };

        // main view: Views/Meals/CreateMeals.cshtml
        return this.View(model);
    }

    // POST: /Meals/CreateMeals
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMeals(MealCreateViewModel model)
    {
        // Re-fetch recipes so we can re-render the selects if validation fails
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();
        model.AllRecipes = recipesData.Recipes;

        if (!this.ModelState.IsValid)
        {
            return this.View(model);
        }

        // Collect selected IDs
        List<int> dishIds = new[] { model.Dish1Id, model.Dish2Id }
                            .Where(id => id.HasValue && id.Value > 0)
                            .Select(id => id!.Value)
                            .ToList();

        List<int> drinkIds = new[] { model.Drink1Id, model.Drink2Id }
                             .Where(id => id.HasValue && id.Value > 0)
                             .Select(id => id!.Value)
                             .ToList();

        List<int> dessertIds = new[] { model.Dessert1Id, model.Dessert2Id }
                               .Where(id => id.HasValue && id.Value > 0)
                               .Select(id => id!.Value)
                               .ToList();

        // At least one recipe across all categories
        if (dishIds.Count + drinkIds.Count + dessertIds.Count == 0)
        {
            this.ModelState.AddModelError(string.Empty, "Please select at least one recipe for this meal.");
            
            return this.View(model);
        }

        // No duplicates
        List<int> allSelected = dishIds.Concat(drinkIds).Concat(dessertIds).ToList();
        
        if (allSelected.Count != allSelected.Distinct().Count())
        {
            this.ModelState.AddModelError(string.Empty, "Each recipe can only be used once in the meal.");
            
            return this.View(model);
        }

        // We only have 2 slots per category in the UI, so "max 2" is already enforced structurally.

        List<RecipeModel> allRecipes = recipesData.Recipes;

        List<RecipeModel>? dishes = dishIds.Count == 0
            ? null
            : allRecipes.Where(r => dishIds.Contains(r.RecipeId)).ToList();

        List<RecipeModel>? drinks = drinkIds.Count == 0
            ? null
            : allRecipes.Where(r => drinkIds.Contains(r.RecipeId)).ToList();

        List<RecipeModel>? desserts = dessertIds.Count == 0
            ? null
            : allRecipes.Where(r => dessertIds.Contains(r.RecipeId)).ToList();

        int userId = this.GetUserIdOrGuest();
        string? sessionId = this.HttpContext.Session.GetString("SessionId");
        
        MealsModel mealToSend = new(
                                    MealId: 0,
                                    UserId: userId,
                                    Name: model.Name,
                                    Dishes: dishes,
                                    Drinks: drinks,
                                    Deserts: desserts);
        
        //  pass sessionId as the second argument
        MealsModel created = await mealsApiService.CreateMeal(mealToSend, sessionId);
        
        // You can later create a dedicated Details view for Meals
        return this.RedirectToAction(nameof(this.MealsIndividual), new { id = created.MealId });
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
