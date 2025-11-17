using Microsoft.AspNetCore.Mvc;
using Project3.Models;

namespace Project3.Controllers;

public class MealController(ILogger<MealController> logger) : Controller
{
    public IActionResult Meals()
    {
        MealModel meal = new();

        return this.View(meal);
    }
}