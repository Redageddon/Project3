using System.Net;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Recipes;

[TestFixture]
public class RecipesDeleteTests : TestFixtureBase
{
    [Test]
    public async Task Delete_ExistingRecipe_ReturnsNoContent()
    {
        RecipeModel created = await this.CreateRecipe();

        HttpResponseMessage response = await this.Client.DeleteAsync($"/api/recipes/{created.RecipeId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

        HttpResponseMessage getResponse = await this.Client.GetAsync($"/api/recipes/{created.RecipeId}");
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Delete_NonExistentRecipe_ReturnsNotFound()
    {
        HttpResponseMessage response = await this.Client.DeleteAsync("/api/recipes/999999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    // NEW: Authorization tests
    [Test]
    public async Task Delete_WithoutSession_ReturnsUnauthorized()
    {
        RecipeModel created = await this.CreateRecipe();
        HttpClient clientWithoutSession = this.Factory.CreateClient();

        HttpResponseMessage response = await clientWithoutSession.DeleteAsync($"/api/recipes/{created.RecipeId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_WithEmptySession_ReturnsUnauthorized()
    {
        RecipeModel created = await this.CreateRecipe();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/recipes/{created.RecipeId}");
        request.Headers.Add("X-Session-Id", "");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_WithInvalidSession_ReturnsUnauthorized()
    {
        RecipeModel created = await this.CreateRecipe();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/recipes/{created.RecipeId}");
        request.Headers.Add("X-Session-Id", "invalid-session-id");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_RecipeOwnedByDifferentUser_ReturnsForbidden()
    {
        // Create recipe with first user
        RecipeModel created = await this.CreateRecipe();

        // Create and login as second user
        string secondUserSessionId = await this.CreateAndLoginSecondUser();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/recipes/{created.RecipeId}");
        request.Headers.Add("X-Session-Id", secondUserSessionId);

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }
}