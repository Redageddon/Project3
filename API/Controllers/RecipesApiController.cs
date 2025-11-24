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
    public ActionResult<MealsModel> GetAll()
    {
        return this.Ok(repository.GetAllRecipes());
    }

    // GET: api/recipes/5
    [HttpGet("{id:int}")]
    public ActionResult<RecipeModel> GetById(int id)
    {
        RecipeModel? recipe = repository.GetRecipeById(id);

        if (recipe == null)
        {
            return this.NotFound(new { message = $"Recipe with ID {id} not found" });
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

        return this.CreatedAtAction(nameof(this.GetById), new { id = createdRecipe.Id }, createdRecipe);
    }

    // PUT: api/recipes/5
    [HttpPut("{id:int}")]
    public ActionResult<RecipeModel> Update(int id, [FromBody] RecipeModel recipe)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        RecipeModel? updatedRecipe = repository.UpdateRecipe(id, recipe);

        if (updatedRecipe == null)
        {
            return this.NotFound(new { message = $"Recipe with ID {id} not found" });
        }

        return this.Ok(updatedRecipe);
    }

    // DELETE: api/recipes/5
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        bool success = repository.DeleteRecipe(id);

        if (!success)
        {
            return this.NotFound(new { message = $"Recipe with ID {id} not found" });
        }

        return this.NoContent();
    }
}