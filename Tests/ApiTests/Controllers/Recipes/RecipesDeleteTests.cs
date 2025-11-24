
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
}
