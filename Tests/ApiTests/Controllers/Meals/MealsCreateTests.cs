using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Meals;

[TestFixture]
public class MealsCreateTests : TestFixtureBase
{
    [Test]
    public async Task Create_WithValidData_ReturnsCreated()
    {
        MealsModel meal = TestDataBuilder.CreateMeal();

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/meals", meal);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        MealsModel? created = await response.Content.ReadFromJsonAsync<MealsModel>();
        Assert.That(created!.MealId, Is.GreaterThan(0));
        Assert.That(created.Name, Is.EqualTo(meal.Name));
        Assert.That(response.Headers.Location!.ToString(), Does.Contain($"/api/meals/{created.MealId}"));
    }

    [Test]
    public async Task Create_WithInvalidModel_ReturnsBadRequest()
    {
        MealsModel invalid = TestDataBuilder.CreateInvalidMeal();

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/meals", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Create_WithNull_ReturnsBadRequest()
    {
        HttpResponseMessage response = await this.Client.PostAsJsonAsync<MealsModel?>("/api/meals", null);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Create_MultipleTimes_GeneratesUniqueIds()
    {
        MealsModel meal1 = TestDataBuilder.CreateMeal();
        MealsModel meal2 = TestDataBuilder.CreateMeal();

        HttpResponseMessage response1 = await this.Client.PostAsJsonAsync("/api/meals", meal1);
        HttpResponseMessage response2 = await this.Client.PostAsJsonAsync("/api/meals", meal2);

        MealsModel? created1 = await response1.Content.ReadFromJsonAsync<MealsModel>();
        MealsModel? created2 = await response2.Content.ReadFromJsonAsync<MealsModel>();

        Assert.That(created1!.MealId, Is.Not.EqualTo(created2!.MealId));
    }

    [Test]
    public async Task Create_WithoutSession_ReturnsUnauthorized()
    {
        MealsModel meal = TestDataBuilder.CreateMeal();
        HttpClient clientWithoutSession = this.Factory.CreateClient();

        HttpResponseMessage response = await clientWithoutSession.PostAsJsonAsync("/api/meals", meal);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Create_WithInvalidSession_ReturnsUnauthorized()
    {
        MealsModel meal = TestDataBuilder.CreateMeal();

        HttpRequestMessage request = new(HttpMethod.Post, "/api/meals")
        {
            Content = JsonContent.Create(meal),
        };

        request.Headers.Add("X-Session-Id", "invalid-session-id");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
}
