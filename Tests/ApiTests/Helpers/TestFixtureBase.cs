
using System.Net.Http.Json;
using API.DataModels.Food;
using API.DataModels.Users;

namespace Tests.ApiTests.Helpers;

public abstract class TestFixtureBase
{
    protected CustomWebApplicationFactory Factory = null!;
    protected HttpClient Client = null!;

    [OneTimeSetUp]
    public void BaseSetUp()
    {
        this.Factory = new CustomWebApplicationFactory();
        this.Client = this.Factory.CreateClient();
    }

    [OneTimeTearDown]
    public void BaseTearDown()
    {
        this.Client.Dispose();
        this.Factory.Dispose();
    }

    protected async Task<RecipeModel> CreateRecipe(RecipeModel? recipe = null)
    {
        recipe ??= TestDataBuilder.CreateRecipe();
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/recipes", recipe);
        return (await response.Content.ReadFromJsonAsync<RecipeModel>())!;
    }

    protected async Task<UserDto> CreateUser(string? username = null, string? email = null, string? password = null)
    {
        RegisterRequest request = new(
            username ?? $"user_{Guid.NewGuid():N}",
            email ?? $"email_{Guid.NewGuid():N}@example.com",
            password ?? "Password123!"
        );

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);
        LoginResponse? loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        return loginResponse!.User!;
    }
}
