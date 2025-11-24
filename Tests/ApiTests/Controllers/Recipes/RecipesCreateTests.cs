using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Recipes;

[TestFixture]
public class RecipesCreateTests : TestFixtureBase
{
    [Test]
    public async Task Create_WithValidData_ReturnsCreated()
    {
        RecipeModel recipe = TestDataBuilder.CreateRecipe();

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/recipes", recipe);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        RecipeModel? created = await response.Content.ReadFromJsonAsync<RecipeModel>();
        Assert.That(created!.RecipeId, Is.GreaterThan(0));
        Assert.That(created.Name, Is.EqualTo(recipe.Name));
        Assert.That(created.Cuisine, Is.EqualTo(recipe.Cuisine));
        Assert.That(response.Headers.Location!.ToString(), Does.Contain($"/api/recipes/{created.RecipeId}"));
    }

    [Test]
    public async Task Create_WithInvalidModel_ReturnsBadRequest()
    {
        RecipeModel invalid = TestDataBuilder.CreateInvalidRecipe();

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/recipes", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Create_WithNull_ReturnsBadRequest()
    {
        HttpResponseMessage response = await this.Client.PostAsJsonAsync<RecipeModel?>("/api/recipes", null);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Create_MultipleTimes_GeneratesUniqueIds()
    {
        RecipeModel recipe1 = TestDataBuilder.CreateRecipe();
        RecipeModel recipe2 = TestDataBuilder.CreateRecipe();

        HttpResponseMessage response1 = await this.Client.PostAsJsonAsync("/api/recipes", recipe1);
        HttpResponseMessage response2 = await this.Client.PostAsJsonAsync("/api/recipes", recipe2);

        RecipeModel? created1 = await response1.Content.ReadFromJsonAsync<RecipeModel>();
        RecipeModel? created2 = await response2.Content.ReadFromJsonAsync<RecipeModel>();

        Assert.That(created1!.RecipeId, Is.Not.EqualTo(created2!.RecipeId));
    }

    [Test]
    public async Task Create_WithNegativePrepTime_ReturnsBadRequest()
    {
        RecipeModel recipe = TestDataBuilder.CreateRecipe() with { PrepTimeMinutes = -10 };

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/recipes", recipe);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Create_WithInvalidRating_ReturnsBadRequest()
    {
        RecipeModel recipe = TestDataBuilder.CreateRecipe() with { Rating = 6 };

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/recipes", recipe);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Create_WithEmptyName_ReturnsBadRequest()
    {
        RecipeModel recipe = TestDataBuilder.CreateRecipe() with { Name = "" };

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/recipes", recipe);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
