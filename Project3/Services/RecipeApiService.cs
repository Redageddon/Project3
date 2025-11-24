using API.DataModels;
using API.DataModels.Food;

namespace Project3.Services;

public class RecipeApiService(IHttpClientFactory httpClientFactory)
{
    public async Task<MealsModel> GetRecipes()
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync("/api/recipes");

        response.EnsureSuccessStatusCode();

        MealsModel? recipes = await response.Content.ReadFromJsonAsync<MealsModel>();

        return recipes ?? throw new Exception("Failed to deserialize recipes");
    }

    public async Task SaveRecipe(MealsModel recipe)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/recipes", recipe);

        response.EnsureSuccessStatusCode();
    }
}