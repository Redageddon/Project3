using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Planners;

[TestFixture]
public class PlannerUpdateTests : TestFixtureBase
{
    [Test]
    public async Task Update_WithValidData_ReturnsOk()
    {
        PlannerModel created = await this.CreatePlanner();

        PlannerModel updated = created with
        {
            BreakfastId = 100,
            LunchId = 200,
        };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/planners/{created.PlannerId}", updated);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        PlannerModel? result = await response.Content.ReadFromJsonAsync<PlannerModel>();
        Assert.That(result!.BreakfastId, Is.EqualTo(100));
        Assert.That(result.LunchId, Is.EqualTo(200));
    }

    [Test]
    public async Task Update_NonExistentPlanner_ReturnsNotFound()
    {
        PlannerModel planner = TestDataBuilder.CreatePlanner();

        HttpResponseMessage response = await this.Client.PutAsJsonAsync("/api/planners/999999", planner);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Update_WithInvalidModel_ReturnsBadRequest()
    {
        PlannerModel created = await this.CreatePlanner();
        PlannerModel invalid = TestDataBuilder.CreateInvalidPlanner();

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/planners/{created.PlannerId}", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}