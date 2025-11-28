using System.Diagnostics;
using API.DataModels.Food;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Services;

namespace Project3.Controllers;

public class MealsController(ILogger<MealsController> logger, MealsApiService mealsApiService) : Controller
{
    public async Task<IActionResult> Index()
    {
        MealsDataModel mealsData = await mealsApiService.GetAllMeals();
        
        return this.View(mealsData);
    }

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