using API.DataModels.Food;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/meals")]
public class MealsApiController(MealsRepository repository, SessionService sessionService) : ControllerBase
{
    // GET: api/meals
    [HttpGet]
    public ActionResult<MealsDataModel> GetAll()
    {
        MealsDataModel meals = repository.GetAllMeals();

        return this.Ok(meals);
    }

    // GET: api/meals/{id}
    [HttpGet("{mealId:int}")]
    public ActionResult<MealsModel> GetById(int mealId)
    {
        MealsModel? meal = repository.GetMealById(mealId);

        if (meal == null)
        {
            return this.NotFound(new { message = $"Meal with ID {mealId} not found" });
        }

        return this.Ok(meal);
    }

    // POST: api/meals
    [HttpPost]
    public ActionResult<MealsModel> Create([FromHeader(Name = "X-Session-Id")] string? sessionId,
                                           [FromBody] MealsModel meal)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            return this.Unauthorized(new { message = "Session is null or empty" });
        }

        int? userId = sessionService.GetUserId(sessionId);

        if (userId is null)
        {
            return this.Unauthorized(new { message = "Invalid or expired session" });
        }

        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        MealsModel mealWithUser = meal with
        {
            UserId = userId.Value,
        };

        MealsModel createdMeal = repository.CreateMeal(mealWithUser);

        return this.CreatedAtAction(nameof(this.GetById),
                                    new { mealId = createdMeal.MealId },
                                    createdMeal);
    }

    // PUT: api/meals/{id}
    [HttpPut("{mealId:int}")]
    public ActionResult<MealsModel> Update([FromHeader(Name = "X-Session-Id")] string? sessionId,
                                           int mealId,
                                           [FromBody] MealsModel meal)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            return this.Unauthorized(new { message = "Session is required" });
        }

        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        int? userId = sessionService.GetUserId(sessionId);

        if (userId is null)
        {
            return this.Unauthorized(new { message = "Invalid or expired session" });
        }

        MealsModel? existing = repository.GetMealById(mealId);

        if (existing == null)
        {
            return this.NotFound(new { message = $"Meal with ID {mealId} not found" });
        }

        if (existing.UserId != userId.Value)
        {
            return this.Forbid();
        }

        MealsModel? updatedMeal = repository.UpdateMeal(mealId, meal);

        return this.Ok(updatedMeal);
    }

    // DELETE: api/meals/{id}
    [HttpDelete("{mealId:int}")]
    public ActionResult Delete([FromHeader(Name = "X-Session-Id")] string? sessionId,
                               int mealId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            return this.Unauthorized(new { message = "Session is required" });
        }

        int? userId = sessionService.GetUserId(sessionId);

        if (userId is null)
        {
            return this.Unauthorized(new { message = "Invalid or expired session" });
        }

        MealsModel? existing = repository.GetMealById(mealId);

        if (existing == null)
        {
            return this.NotFound(new { message = $"Meal with ID {mealId} not found" });
        }

        if (existing.UserId != userId.Value)
        {
            return this.Forbid();
        }

        bool _ = repository.DeleteMeal(mealId);

        return this.NoContent();
    }
}