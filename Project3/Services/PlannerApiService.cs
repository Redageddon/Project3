
using API.DataModels.Food;

namespace Project3.Services;

public class PlannerApiService(IHttpClientFactory httpClientFactory)
{
    // GET: api/planners/user/{userId}
    public async Task<PlannersDataModel> GetPlannersByUserId(int userId)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync($"/api/planners/user/{userId}");

        response.EnsureSuccessStatusCode();

        PlannersDataModel? planners = await response.Content.ReadFromJsonAsync<PlannersDataModel>();

        return planners ?? throw new Exception("Failed to deserialize planners");
    }

    // GET: api/planners/{id}
    public async Task<PlannerModel?> GetPlannerById(int id)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync($"/api/planners/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        PlannerModel? planner = await response.Content.ReadFromJsonAsync<PlannerModel>();

        return planner;
    }

    // POST: api/planners
    public async Task<PlannerModel> CreatePlanner(PlannerModel planner)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/planners", planner);

        response.EnsureSuccessStatusCode();

        PlannerModel? createdPlanner = await response.Content.ReadFromJsonAsync<PlannerModel>();

        return createdPlanner ?? throw new Exception("Failed to deserialize created planner");
    }

    // PUT: api/planners/{id}
    public async Task<PlannerModel?> UpdatePlanner(int id, PlannerModel planner)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PutAsJsonAsync($"/api/planners/{id}", planner);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        PlannerModel? updatedPlanner = await response.Content.ReadFromJsonAsync<PlannerModel>();

        return updatedPlanner;
    }

    // DELETE: api/planners/{id}
    public async Task<bool> DeletePlanner(int id)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.DeleteAsync($"/api/planners/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();

        return true;
    }
}
