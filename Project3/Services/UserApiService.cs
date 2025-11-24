
using API.DataModels.Users;

namespace Project3.Services;

public class UserApiService(IHttpClientFactory httpClientFactory)
{
    // GET: api/users
    public async Task<List<UserDto>> GetAllUsers()
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync("/api/users");

        response.EnsureSuccessStatusCode();

        List<UserDto>? users = await response.Content.ReadFromJsonAsync<List<UserDto>>();

        return users ?? throw new Exception("Failed to deserialize users");
    }

    // GET: api/users/{uid}
    public async Task<UserDto?> GetUserById(int uid)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.GetAsync($"/api/users/{uid}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        UserDto? user = await response.Content.ReadFromJsonAsync<UserDto>();

        return user;
    }

    // PUT: api/users/{uid}
    public async Task<UserDto?> UpdateUser(int uid, UserUpdateRequest request)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PutAsJsonAsync($"/api/users/{uid}", request);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        UserDto? user = await response.Content.ReadFromJsonAsync<UserDto>();

        return user;
    }

    // DELETE: api/users/{uid}
    public async Task<bool> DeleteUser(int uid)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.DeleteAsync($"/api/users/{uid}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();

        return true;
    }
}
