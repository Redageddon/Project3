using System.Net.Http.Json;
using API.DataModels.Food;
using API.DataModels.Users;

namespace Tests.ApiTests.Helpers;

public abstract class TestFixtureBase
{
    protected HttpClient Client = null!;
    protected CustomWebApplicationFactory Factory = null!;
    private string? sessionId;

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

    [SetUp]
    public async Task SetupSession()
    {
        if (this.sessionId == null)
        {
            RegisterRequest registerRequest = TestDataBuilder.CreateRegisterRequest();
            await this.Client.PostAsJsonAsync("/api/auth/register", registerRequest);

            LoginRequest loginRequest =
                TestDataBuilder.CreateLoginRequest(registerRequest.Email, registerRequest.Password);

            HttpResponseMessage loginResponse = await this.Client.PostAsJsonAsync("/api/auth/login", loginRequest);
            LoginResponse? result = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

            this.sessionId = result!.SessionId;
            this.Client.DefaultRequestHeaders.Add("X-Session-Id", this.sessionId);
        }
    }

    protected async Task<RecipeModel> CreateRecipe(RecipeModel? recipe = null)
    {
        recipe ??= TestDataBuilder.CreateRecipe();
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/recipes", recipe);

        return (await response.Content.ReadFromJsonAsync<RecipeModel>())!;
    }

    protected async Task<MealsModel> CreateMeal(MealsModel? meal = null)
    {
        meal ??= TestDataBuilder.CreateMeal();
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/meals", meal);

        return (await response.Content.ReadFromJsonAsync<MealsModel>())!;
    }

    protected async Task<PlannerModel> CreatePlanner(PlannerModel? planner = null)
    {
        planner ??= TestDataBuilder.CreatePlanner();
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/planners", planner);

        return (await response.Content.ReadFromJsonAsync<PlannerModel>())!;
    }

    protected async Task<UserDto> CreateUser(string? username = null, string? email = null, string? password = null)
    {
        RegisterRequest request = new(username ?? $"user_{Guid.NewGuid():N}",
                                      email ?? $"email_{Guid.NewGuid():N}@example.com",
                                      password ?? "Password123!");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);
        LoginResponse? loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        return loginResponse!.User!;
    }

    // NEW: Helper for multi-user authorization tests
    protected async Task<string> CreateAndLoginSecondUser()
    {
        string email = $"seconduser_{Guid.NewGuid():N}@example.com";
        string password = "SecondUser123!";
        
        RegisterRequest registerRequest = new($"seconduser_{Guid.NewGuid():N}",
                                              email,
                                              password);

        HttpClient tempClient = this.Factory.CreateClient();
        await tempClient.PostAsJsonAsync("/api/auth/register", registerRequest);

        LoginRequest loginRequest = new(email, password);
        HttpResponseMessage loginResponse = await tempClient.PostAsJsonAsync("/api/auth/login", loginRequest);
        LoginResponse? result = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        return result!.SessionId!;
    }
}