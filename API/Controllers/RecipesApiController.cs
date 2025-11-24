using API.DataModels.Food;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesApiController(RecipeRepository repository) : ControllerBase
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
    public ActionResult<RecipeModel> Create([FromBody] RecipeModel recipe)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        RecipeModel createdRecipe = repository.CreateRecipe(recipe);

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