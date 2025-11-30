using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Recipes;

[TestFixture]
public class RecipesUpdateTests : TestFixtureBase
{
    [Test]
    public async Task Update_WithValidData_ReturnsOk()
    {
        RecipeModel created = await this.CreateRecipe();

        RecipeModel updated = created with
        {
            Name = "Updated Name",
            Cuisine = "French",
        };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", updated);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        RecipeModel? result = await response.Content.ReadFromJsonAsync<RecipeModel>();
        Assert.That(result!.Name, Is.EqualTo("Updated Name"));
        Assert.That(result.Cuisine, Is.EqualTo("French"));
    }

    [Test]
    public async Task Update_PartialFields_UpdatesOnlySpecified()
    {
        RecipeModel created = await this.CreateRecipe();
        string originalCuisine = created.Cuisine;
        RecipeModel updated = created with { Name = "New Name Only" };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", updated);

        RecipeModel? result = await response.Content.ReadFromJsonAsync<RecipeModel>();
        Assert.That(result!.Name, Is.EqualTo("New Name Only"));
        Assert.That(result.Cuisine, Is.EqualTo(originalCuisine));
    }

    [Test]
    public async Task Update_NonExistentRecipe_ReturnsNotFound()
    {
        RecipeModel recipe = TestDataBuilder.CreateRecipe();

        HttpResponseMessage response = await this.Client.PutAsJsonAsync("/api/recipes/999999", recipe);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Update_WithInvalidModel_ReturnsBadRequest()
    {
        RecipeModel created = await this.CreateRecipe();
        RecipeModel invalid = created with { Name = "" };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_WithNull_ReturnsBadRequest()
    {
        RecipeModel created = await this.CreateRecipe();

        HttpResponseMessage response =
            await this.Client.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", (RecipeModel?)null);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_EmptyDifficulty_ReturnsBadRequest()
    {
        RecipeModel created = await this.CreateRecipe();
        RecipeModel invalid = created with { Difficulty = "" };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_EmptyCuisine_ReturnsBadRequest()
    {
        RecipeModel created = await this.CreateRecipe();
        RecipeModel invalid = created with { Cuisine = "" };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_WithNegativeCookTime_ReturnsBadRequest()
    {
        RecipeModel created = await this.CreateRecipe();
        RecipeModel invalid = created with { CookTimeMinutes = -5 };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_WithInvalidRating_ReturnsBadRequest()
    {
        RecipeModel created = await this.CreateRecipe();
        RecipeModel invalid = created with { Rating = 10 };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    // NEW: Authorization tests
    [Test]
    public async Task Update_WithoutSession_ReturnsUnauthorized()
    {
        RecipeModel created = await this.CreateRecipe();
        HttpClient clientWithoutSession = this.Factory.CreateClient();
        
        RecipeModel updated = created with { Name = "Updated" };

        HttpResponseMessage response = await clientWithoutSession.PutAsJsonAsync($"/api/recipes/{created.RecipeId}", updated);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Update_WithEmptySession_ReturnsUnauthorized()
    {
        RecipeModel created = await this.CreateRecipe();
        RecipeModel updated = created with { Name = "Updated" };

        HttpRequestMessage request = new(HttpMethod.Put, $"/api/recipes/{created.RecipeId}")
        {
            Content = JsonContent.Create(updated),
        };
        
        request.Headers.Add("X-Session-Id", "");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Update_WithInvalidSession_ReturnsUnauthorized()
    {
        RecipeModel created = await this.CreateRecipe();
        RecipeModel updated = created with { Name = "Updated" };

        HttpRequestMessage request = new(HttpMethod.Put, $"/api/recipes/{created.RecipeId}")
        {
            Content = JsonContent.Create(updated),
        };
        
        request.Headers.Add("X-Session-Id", "invalid-session-id");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Update_RecipeOwnedByDifferentUser_ReturnsForbidden()
    {
        // Create recipe with first user
        RecipeModel created = await this.CreateRecipe();

        // Create and login as second user
        string secondUserSessionId = await this.CreateAndLoginSecondUser();

        RecipeModel updated = created with { Name = "Hacked Recipe" };

        HttpRequestMessage request = new(HttpMethod.Put, $"/api/recipes/{created.RecipeId}")
        {
            Content = JsonContent.Create(updated),
        };
        
        request.Headers.Add("X-Session-Id", secondUserSessionId);

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }
}