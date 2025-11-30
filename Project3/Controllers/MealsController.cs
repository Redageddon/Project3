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
    // ==== Auth helpers ====

    private bool IsUserLoggedIn()
    {
        string? sessionId = this.HttpContext.Session.GetString("SessionId");
        int? userId = this.HttpContext.Session.GetInt32("UserId");

        return !string.IsNullOrEmpty(sessionId) && userId.HasValue;
    }

    private string GetSessionIdOrThrow()
    {
        string? sessionId = this.HttpContext.Session.GetString("SessionId");
        
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new InvalidOperationException("User must be logged in to perform this action.");
        }

        return sessionId;
    }

    private int GetUserIdOrThrow()
    {
        int? userId = this.HttpContext.Session.GetInt32("UserId");
        
        if (!userId.HasValue)
        {
            throw new InvalidOperationException("User must be logged in to perform this action.");
        }

        return userId.Value;
    }

    // ==== List & details ====

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

    // ==== Create ====

    // GET: /Meals/CreateMeals
    [HttpGet]
    public async Task<IActionResult> CreateMeals()
    {
        bool isLoggedIn = this.IsUserLoggedIn();
        this.ViewBag.IsUserLoggedIn = isLoggedIn;

        // If not logged in, the view will just show the login modal (no form)
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();

        MealCreateViewModel model = new()
        {
            AllRecipes = recipesData.Recipes,
        };

        return this.View(model); // Views/Meals/CreateMeals.cshtml
    }

    // POST: /Meals/CreateMeals
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMeals(MealCreateViewModel model)
    {
        if (!this.IsUserLoggedIn())
        {
            // UX: same as Recipes -> show login modal instead of redirect
            this.ViewBag.IsUserLoggedIn = false;
            this.ModelState.AddModelError(string.Empty, "You must be logged in to create a meal.");
            
            return this.View(model);
        }

        this.ViewBag.IsUserLoggedIn = true;

        // Re-fetch recipes so we can re-render the selects if validation fails
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();
        model.AllRecipes = recipesData.Recipes;

        if (!this.ModelState.IsValid)
        {
            return this.View(model);
        }

        // Hard requirement: logged in with real UserId + SessionId
        int userId;
        string sessionId;
       
        try
        {
            userId = this.GetUserIdOrThrow();
            sessionId = this.GetSessionIdOrThrow();
        }
        catch
        {
            this.ViewBag.IsUserLoggedIn = false;
            this.ModelState.AddModelError(string.Empty, "Your session has expired. Please log in again.");
            
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

        List<RecipeModel> allRecipes = recipesData.Recipes;

        List<RecipeModel>? dishes = dishIds.Count == 0
            ? null
            : allRecipes.Where(r => dishIds.Contains(r.RecipeId))
                        .Select(r => r with { UserId = userId }) // Fix the UserId
                        .ToList();

        List<RecipeModel>? drinks = drinkIds.Count == 0
            ? null
            : allRecipes.Where(r => drinkIds.Contains(r.RecipeId))
                        .Select(r => r with { UserId = userId }) // Fix the UserId
                        .ToList();

        List<RecipeModel>? desserts = dessertIds.Count == 0
            ? null
            : allRecipes.Where(r => dessertIds.Contains(r.RecipeId))
                        .Select(r => r with { UserId = userId }) // Fix the UserId
                        .ToList();

        // No guest: we always send the real userId from session
        MealsModel mealToSend = new(
                                    MealId: 0,
                                    UserId: userId,
                                    Name: model.Name,
                                    Dishes: dishes,
                                    Drinks: drinks,
                                    Desserts: desserts);

        MealsModel created = await mealsApiService.CreateMeal(mealToSend, sessionId);

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
