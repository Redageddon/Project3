using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using API.DataModels.Food;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class PlannerController(
    ILogger<PlannerController> logger,
    MealsApiService mealsApiService,
    PlannerApiService plannerApiService) : Controller
{
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

    public async Task<IActionResult> Index()
    {
        bool isLoggedIn = this.IsUserLoggedIn();
        this.ViewBag.IsUserLoggedIn = isLoggedIn;

        if (!isLoggedIn)
        {
            return this.View(new PlannersDataModel([]));
        }

        int userId = this.GetUserIdOrThrow();
        PlannersDataModel planners = await plannerApiService.GetPlannersByUserId(userId);

        // Also load meals to be able to display names for selected planner
        MealsDataModel allMeals = await mealsApiService.GetAllMeals();
        // Filter to current user's meals for clarity
        MealsDataModel userMeals = new(allMeals.Meals.Where(m => m.UserId == userId).ToList());
        this.ViewBag.AllMeals = userMeals;

        return this.View(planners);
    }

    // ==== Create Planner ====
    [HttpGet]
    public async Task<IActionResult> CreatePlanner()
    {
        bool isLoggedIn = this.IsUserLoggedIn();
        this.ViewBag.IsUserLoggedIn = isLoggedIn;

        // Prepare meal lists (use user's meals) for selects
        MealsDataModel mealsData = await mealsApiService.GetAllMeals();
        int? userIdOpt = this.HttpContext.Session.GetInt32("UserId");
        int currentUserId = userIdOpt ?? -1;
        List<MealsModel> userMeals = mealsData.Meals
            .Where(m => m.UserId == currentUserId)
            .OrderBy(m => m.Name ?? string.Empty)
            .ToList();

        // We use the same meals list for Breakfast/Lunch/Dinner selections
        this.ViewBag.MealOptions = userMeals;

        PlannerModel model = new(
                                 PlannerId: 0,
                                 UserId: 0,
                                 PlannedDate: DateTime.Today,
                                 BreakfastId: null,
                                 LunchId: null,
                                 DinnerId: null);

        return this.View(model); // Views/Planner/CreatePlanner.cshtml
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePlanner(PlannerModel model)
    {
        bool isLoggedIn = this.IsUserLoggedIn();
        this.ViewBag.IsUserLoggedIn = isLoggedIn;

        if (!isLoggedIn)
        {
            this.ModelState.AddModelError(string.Empty, "You must be logged in to create a planner.");
            return this.View(model);
        }

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

        // Set the real userId before validation and ignore incoming UserId
        this.ModelState.Remove("UserId");
        PlannerModel toSend = model with { UserId = userId };

        // Re-populate options for re-render (after potential validation)
        MealsDataModel mealsData = await mealsApiService.GetAllMeals();
        List<MealsModel> userMeals = mealsData.Meals
            .Where(m => m.UserId == userId)
            .OrderBy(m => m.Name ?? string.Empty)
            .ToList();
        this.ViewBag.MealOptions = userMeals;

        if (!this.ModelState.IsValid)
        {
            return this.View(toSend);
        }

        PlannerModel created = await plannerApiService.CreatePlanner(toSend, sessionId);

        return this.RedirectToAction(nameof(this.Index));
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