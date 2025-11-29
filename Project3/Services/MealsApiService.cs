using API.DataModels.Food;

namespace Project3.Services;

public class MealsApiService(IHttpClientFactory httpClientFactory)
{
    // GET: api/meals
    public async Task<MealsDataModel> GetAllMeals()
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync("/api/meals");

        response.EnsureSuccessStatusCode();

        MealsDataModel? meals = await response.Content.ReadFromJsonAsync<MealsDataModel>();

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
    public async Task<MealsModel> CreateMeal(MealsModel meal, string? sessionId)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");

        using HttpRequestMessage request = new(HttpMethod.Post, "/api/meals");

        request.Content = JsonContent.Create(meal);

        if (!string.IsNullOrEmpty(sessionId))
        {
            request.Headers.Add("X-Session-Id", sessionId);
        }

        HttpResponseMessage response = await client.SendAsync(request);
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
