using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Project3.Models;

namespace Project3.Controllers;

public class PlannerController(ILogger<PlannerController> logger) : Controller
{
    public IActionResult Planner()
    {
        return this.View();
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