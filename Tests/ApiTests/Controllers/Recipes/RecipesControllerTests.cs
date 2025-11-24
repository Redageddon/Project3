using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Recipes;

[TestFixture]
public class RecipesGetTests : TestFixtureBase
{
    [Test]
    public async Task GetAll_ReturnsListOfRecipes()
    {
        HttpResponseMessage response = await this.Client.GetAsync("/api/recipes");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        RecipesDataModel? meals = await response.Content.ReadFromJsonAsync<RecipesDataModel>();
        Assert.That(meals, Is.Not.Null);
        Assert.That(meals!.Recipes, Is.Not.Null);
    }

    [Test]
    public async Task GetById_ExistingRecipe_ReturnsOk()
    {
        RecipeModel created = await this.CreateRecipe();

        HttpResponseMessage response = await this.Client.GetAsync($"/api/recipes/{created.Id}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        RecipeModel? result = await response.Content.ReadFromJsonAsync<RecipeModel>();
        Assert.That(result!.Id, Is.EqualTo(created.Id));
        Assert.That(result.Name, Is.EqualTo(created.Name));
    }

    [Test]
    public async Task GetById_NonExistentRecipe_ReturnsNotFound()
    {
        HttpResponseMessage response = await this.Client.GetAsync("/api/recipes/999999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
