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
    public ActionResult<List<MealsModel>> GetAll()
    {
        MealsDataModel meals = repository.GetAllMeals();
        
        return this.Ok(meals.Meals);
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
    public ActionResult<MealsModel> Create(
        [FromHeader(Name = "X-Session-Id")] string? sessionId,
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

        return this.CreatedAtAction(
                                    nameof(this.GetById),
                                    new { mealId = createdMeal.MealId },
                                    createdMeal);
    }

    // PUT: api/meals/{id}
    [HttpPut("{mealId:int}")]
    public ActionResult<MealsModel> Update(int mealId, [FromBody] MealsModel meal)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        MealsModel? updatedMeal = repository.UpdateMeal(mealId, meal);

        if (updatedMeal == null)
        {
            return this.NotFound(new { message = $"Meal with ID {mealId} not found" });
        }

        return this.Ok(updatedMeal);
    }

    // DELETE: api/meals/{id}
    [HttpDelete("{mealId:int}")]
    public ActionResult Delete(int mealId)
    {
        bool success = repository.DeleteMeal(mealId);

        if (!success)
        {
            return this.NotFound(new { message = $"Meal with ID {mealId} not found" });
        }

        return this.NoContent();
    }
}
