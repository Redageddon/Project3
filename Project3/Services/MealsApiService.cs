using API.DataModels.Food;

namespace Project3.Services;

public class MealsApiService(IHttpClientFactory httpClientFactory)
{
    // GET: api/meals
    public async Task<List<MealsModel>> GetAllMeals()
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync("/api/meals");

        response.EnsureSuccessStatusCode();

        List<MealsModel>? meals = await response.Content.ReadFromJsonAsync<List<MealsModel>>();

        return meals ?? throw new Exception("Failed to deserialize meals");
    }

    // GET: api/meals/{id}
    public async Task<MealsModel?> GetMealById(int id)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync($"/api/meals/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        MealsModel? meal = await response.Content.ReadFromJsonAsync<MealsModel>();

        return meal;
    }

    // POST: api/meals
    public async Task<MealsModel> CreateMeal(MealsModel meal)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/meals", meal);

        response.EnsureSuccessStatusCode();

        MealsModel? createdMeal = await response.Content.ReadFromJsonAsync<MealsModel>();

        return createdMeal ?? throw new Exception("Failed to deserialize created meal");
    }

    // PUT: api/meals/{id}
    public async Task<MealsModel?> UpdateMeal(int id, MealsModel meal)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PutAsJsonAsync($"/api/meals/{id}", meal);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        MealsModel? updatedMeal = await response.Content.ReadFromJsonAsync<MealsModel>();

        return updatedMeal;
    }

    // DELETE: api/meals/{id}
    public async Task<bool> DeleteMeal(int id)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.DeleteAsync($"/api/meals/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();

        return true;
    }
}
