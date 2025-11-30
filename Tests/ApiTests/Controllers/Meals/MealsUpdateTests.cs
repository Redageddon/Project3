using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Meals;

[TestFixture]
public class MealsUpdateTests : TestFixtureBase
{
    [Test]
    public async Task Update_WithValidData_ReturnsOk()
    {
        MealsModel created = await this.CreateMeal();
        MealsModel updated = created with { Name = "Updated Meal Name" };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/meals/{created.MealId}", updated);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        MealsModel? result = await response.Content.ReadFromJsonAsync<MealsModel>();
        Assert.That(result!.Name, Is.EqualTo("Updated Meal Name"));
    }

    [Test]
    public async Task Update_NonExistentMeal_ReturnsNotFound()
    {
        MealsModel meal = TestDataBuilder.CreateMeal();

        HttpResponseMessage response = await this.Client.PutAsJsonAsync("/api/meals/999999", meal);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Update_WithInvalidModel_ReturnsBadRequest()
    {
        MealsModel created = await this.CreateMeal();
        MealsModel invalid = created with { UserId = 0 };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/meals/{created.MealId}", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_WithNull_ReturnsBadRequest()
    {
        MealsModel created = await this.CreateMeal();

        HttpResponseMessage response =
            await this.Client.PutAsJsonAsync($"/api/meals/{created.MealId}", (MealsModel?)null);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_DishesAndDrinks_UpdatesSuccessfully()
    {
        MealsModel created = await this.CreateMeal();
        RecipeModel dish = await this.CreateRecipe();
        RecipeModel drink = await this.CreateRecipe(TestDataBuilder.CreateRecipe("Coffee"));

        MealsModel updated = created with
        {
            Dishes = [dish],
            Drinks = [drink],
        };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/meals/{created.MealId}", updated);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        MealsModel? result = await response.Content.ReadFromJsonAsync<MealsModel>();
        Assert.That(result!.Dishes, Has.Count.EqualTo(1));
        Assert.That(result.Drinks, Has.Count.EqualTo(1));
    }
}