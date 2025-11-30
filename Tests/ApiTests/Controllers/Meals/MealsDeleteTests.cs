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
}