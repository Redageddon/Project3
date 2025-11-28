using System.Net;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Planners;

[TestFixture]
public class PlannerDeleteTests : TestFixtureBase
{
    [Test]
    public async Task Delete_ExistingPlanner_ReturnsNoContent()
    {
        PlannerModel created = await this.CreatePlanner();

        HttpResponseMessage response = await this.Client.DeleteAsync($"/api/planners/{created.PlannerId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

        HttpResponseMessage getResponse = await this.Client.GetAsync($"/api/planners/{created.PlannerId}");
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Delete_NonExistentPlanner_ReturnsNotFound()
    {
        HttpResponseMessage response = await this.Client.DeleteAsync("/api/planners/999999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
