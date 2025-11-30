using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Meals;

[TestFixture]
public class MealsGetTests : TestFixtureBase
{
    [Test]
    public async Task GetAll_ReturnsListOfMeals()
    {
        HttpResponseMessage response = await this.Client.GetAsync("/api/meals");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        MealsDataModel? meals = await response.Content.ReadFromJsonAsync<MealsDataModel>();
        Assert.That(meals?.Meals, Is.Not.Null);
    }

    [Test]
    public async Task GetById_ExistingMeal_ReturnsOk()
    {
        MealsModel created = await this.CreateMeal();

        HttpResponseMessage response = await this.Client.GetAsync($"/api/meals/{created.MealId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        MealsModel? result = await response.Content.ReadFromJsonAsync<MealsModel>();
        Assert.That(result!.MealId, Is.EqualTo(created.MealId));
        Assert.That(result.Name, Is.EqualTo(created.Name));
    }

    [Test]
    public async Task GetById_NonExistentMeal_ReturnsNotFound()
    {
        HttpResponseMessage response = await this.Client.GetAsync("/api/meals/999999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}