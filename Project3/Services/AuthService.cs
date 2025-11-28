using API.DataModels.Users;

namespace Project3.Services;

public class AuthApiService(IHttpClientFactory httpClientFactory)
{
    // POST: api/auth/register
    public async Task<LoginResponse> Register(RegisterRequest request)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/auth/register", request);

        LoginResponse? loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        return loginResponse ?? throw new Exception("Failed to deserialize registration response");
    }

    // POST: api/auth/login
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/auth/login", request);

        LoginResponse? loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        return loginResponse ?? throw new Exception("Failed to deserialize login response");
    }

    // POST: api/auth/logout
    public async Task<bool> Logout(LogoutRequest logoutRequest)
    {
        HttpClient client = httpClientFactory.CreateClient("RecipeAPI");
        HttpResponseMessage response = await client.PostAsJsonAsync("/api/auth/logout", logoutRequest);

        return response.IsSuccessStatusCode;
    }
}
