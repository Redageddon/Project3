using System.Diagnostics;
using API.DataModels.Food;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class RecipesController(
    ILogger<RecipesController> logger,
    RecipeApiService recipeApiService) : Controller
{
    private bool IsUserLoggedIn()
    {
        string? sessionId = this.HttpContext.Session.GetString("SessionId");
        
        return !string.IsNullOrEmpty(sessionId);
    }

    private string? GetSessionId()
    {
        return this.HttpContext.Session.GetString("SessionId");
    }

    public async Task<IActionResult> Index([FromQuery] RecipeFilter recipeFilter)
    {
        RecipesDataModel recipesData = await recipeApiService.GetRecipes();

        recipesData = new RecipesDataModel(recipeFilter.Filter(recipesData.Recipes));
        this.ViewBag.RecipeFilter = recipeFilter;

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

    // GET: /Recipes/CreateRecipes
    [HttpGet]
    public IActionResult CreateRecipes()
    {
        bool isLoggedIn = this.IsUserLoggedIn();
        this.ViewBag.IsUserLoggedIn = isLoggedIn;

        // If logged in, give the form a real model. If not, the view will just show the modal.
        RecipeModel model = new(
                                RecipeId: 0,
                                UserId: 0,
                                Name: string.Empty,
                                Difficulty: string.Empty,
                                Cuisine: string.Empty,
                                Ingredients: new List<string>(),
                                Instructions: new List<string>(),
                                Tags: new List<string>(),
                                MealType: new List<string>(),
                                Image: string.Empty,
                                PrepTimeMinutes: 0,
                                CookTimeMinutes: 0,
                                Servings: 0,
                                CaloriesPerServing: 0,
                                ReviewCount: 0,
                                Rating: 0);

        return this.View(model); // View: CreateRecipes.cshtml
    }

    // POST: /Recipes/CreateRecipes
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRecipes(RecipeModel recipe)
    {
        if (!this.IsUserLoggedIn())
        {
            // UX: same view, but show login-required modal instead of form
            this.ViewBag.IsUserLoggedIn = false;
            
            this.ModelState.AddModelError(string.Empty, "You must be logged in to create a recipe.");
            
            return this.View(recipe);
        }

        this.ViewBag.IsUserLoggedIn = true;

        if (!this.ModelState.IsValid)
        {
            // server-side validation errors: show the form again
            return this.View(recipe);
        }

        string? sessionId = this.GetSessionId();
        
        if (string.IsNullOrEmpty(sessionId))
        {
            this.ViewBag.IsUserLoggedIn = false;
            
            this.ModelState.AddModelError(string.Empty, "Your session has expired. Please log in again.");
            
            return this.View(recipe);
        }

        // Ensure the API can handle this: UserId will be overwritten by the API
        RecipeModel recipeToSend = recipe with
        {
            RecipeId = 0,
            UserId = 0,
            ReviewCount = 0,
            Rating = 0
        };

        RecipeModel created = await recipeApiService.CreateRecipe(sessionId, recipeToSend);

        // Redirect to the detail page for the newly created recipe
        return this.RedirectToAction(nameof(this.RecipesIndividual), new { id = created.RecipeId });
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
