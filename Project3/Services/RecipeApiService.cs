using API.DataModels.Food;

namespace Project3.Services;

public class RecipeApiService(IHttpClientFactory httpClientFactory)
{
    // GET: api/recipes
    public async Task<RecipesDataModel> GetRecipes()
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync("/api/recipes");

        response.EnsureSuccessStatusCode();

        RecipesDataModel? recipes = await response.Content.ReadFromJsonAsync<RecipesDataModel>();

        return recipes ?? throw new Exception("Failed to deserialize recipes");
    }

    // GET: api/recipes/{id}
    public async Task<RecipeModel?> GetRecipeById(int id)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync($"/api/recipes/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        RecipeModel? recipe = await response.Content.ReadFromJsonAsync<RecipeModel>();

        return recipe;
    }

    // POST: api/recipes
    public async Task<RecipeModel> CreateRecipe(RecipeModel recipe)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/recipes", recipe);

        response.EnsureSuccessStatusCode();

        RecipeModel? createdRecipe = await response.Content.ReadFromJsonAsync<RecipeModel>();

        return createdRecipe ?? throw new Exception("Failed to deserialize created recipe");
    }

    // PUT: api/recipes/{id}
    public async Task<RecipeModel?> UpdateRecipe(int id, RecipeModel recipe)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PutAsJsonAsync($"/api/recipes/{id}", recipe);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        RecipeModel? updatedRecipe = await response.Content.ReadFromJsonAsync<RecipeModel>();

        return updatedRecipe;
    }

    // DELETE: api/recipes/{id}
    public async Task<bool> DeleteRecipe(int id)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.DeleteAsync($"/api/recipes/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();

        return true;
    }
}