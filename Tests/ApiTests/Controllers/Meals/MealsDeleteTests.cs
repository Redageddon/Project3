using System.Net;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Meals;

[TestFixture]
public class MealsDeleteTests : TestFixtureBase
{
    [Test]
    public async Task Delete_ExistingMeal_ReturnsNoContent()
    {
        MealsModel created = await this.CreateMeal();

        HttpResponseMessage response = await this.Client.DeleteAsync($"/api/meals/{created.MealId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

        HttpResponseMessage getResponse = await this.Client.GetAsync($"/api/meals/{created.MealId}");
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Delete_NonExistentMeal_ReturnsNotFound()
    {
        HttpResponseMessage response = await this.Client.DeleteAsync("/api/meals/999999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Delete_WithoutSession_ReturnsUnauthorized()
    {
        MealsModel created = await this.CreateMeal();
        HttpClient clientWithoutSession = this.Factory.CreateClient();

        HttpResponseMessage response = await clientWithoutSession.DeleteAsync($"/api/meals/{created.MealId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_WithEmptySession_ReturnsUnauthorized()
    {
        MealsModel created = await this.CreateMeal();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/meals/{created.MealId}");
        request.Headers.Add("X-Session-Id", "");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_WithInvalidSession_ReturnsUnauthorized()
    {
        MealsModel created = await this.CreateMeal();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/meals/{created.MealId}");
        request.Headers.Add("X-Session-Id", "invalid-session-id");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_MealOwnedByDifferentUser_ReturnsForbidden()
    {
        MealsModel created = await this.CreateMeal();
        string secondUserSessionId = await this.CreateAndLoginSecondUser();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/meals/{created.MealId}");
        request.Headers.Add("X-Session-Id", secondUserSessionId);

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }
}