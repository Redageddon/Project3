using API.DataModels.Food;

namespace Project3.Services;

public class RecipeApiService(IHttpClientFactory httpClientFactory)
{
    public async Task<RecipesDataModel> GetRecipes()
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync("/api/recipes");

        response.EnsureSuccessStatusCode();

        RecipesDataModel? recipes = await response.Content.ReadFromJsonAsync<RecipesDataModel>();

        return recipes ?? throw new Exception("Failed to deserialize recipes");
    }

    public async Task SaveRecipe(RecipesDataModel recipe)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/recipes", recipe);

        response.EnsureSuccessStatusCode();
    }
}