using API.DataModels.Food;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesApiController(RecipeRepository repository, SessionService sessionService) : ControllerBase
{
    // GET: api/recipes
    [HttpGet]
    public ActionResult<RecipesDataModel> GetAll()
    {
        return this.Ok(repository.GetAllRecipes());
    }

    // GET: api/recipes/5
    [HttpGet("{recipeId:int}")]
    public ActionResult<RecipeModel> GetById(int recipeId)
    {
        RecipeModel? recipe = repository.GetRecipeById(recipeId);

        if (recipe == null)
        {
            return this.NotFound(new { message = $"Recipe with ID {recipeId} not found" });
        }

        return this.Ok(recipe);
    }

    // POST: api/recipes
    [HttpPost]
    public ActionResult<RecipeModel> Create(
        [FromHeader(Name = "X-Session-Id")] string? sessionId,
        [FromBody] RecipeModel recipe)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            return this.Unauthorized(new { message = "Session is null or empty" });
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

        RecipeModel recipeWithUser = recipe with
        {
            UserId = userId.Value,
        };

        RecipeModel createdRecipe = repository.CreateRecipe(recipeWithUser);

        return this.CreatedAtAction(nameof(this.GetById), new { recipeId = createdRecipe.RecipeId }, createdRecipe);
    }

    // PUT: api/recipes/5
    [HttpPut("{recipeId:int}")]
    public ActionResult<RecipeModel> Update(int recipeId, [FromBody] RecipeModel recipe)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        RecipeModel? updatedRecipe = repository.UpdateRecipe(recipeId, recipe);

        if (updatedRecipe == null)
        {
            return this.NotFound(new { message = $"Recipe with ID {recipeId} not found" });
        }

        return this.Ok(updatedRecipe);
    }

    // DELETE: api/recipes/5
    [HttpDelete("{recipeId:int}")]
    public ActionResult Delete(int recipeId)
    {
        bool success = repository.DeleteRecipe(recipeId);

        if (!success)
        {
            return this.NotFound(new { message = $"Recipe with ID {recipeId} not found" });
        }

        return this.NoContent();
    }
}