using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Planners;

[TestFixture]
public class PlannerGetTests : TestFixtureBase
{
    [Test]
    public async Task GetByUserId_ReturnsListOfPlanners()
    {
        PlannerModel created = await this.CreatePlanner();

        HttpResponseMessage response = await this.Client.GetAsync($"/api/planners/user/{created.UserId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        PlannersDataModel? planners = await response.Content.ReadFromJsonAsync<PlannersDataModel>();
        Assert.That(planners?.Planners, Is.Not.Null);
        Assert.That(planners.Planners.Any(p => p.PlannerId == created.PlannerId), Is.True);
    }

    [Test]
    public async Task GetById_ExistingPlanner_ReturnsOk()
    {
        PlannerModel created = await this.CreatePlanner();

        HttpResponseMessage response = await this.Client.GetAsync($"/api/planners/{created.PlannerId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        PlannerModel? result = await response.Content.ReadFromJsonAsync<PlannerModel>();
        Assert.That(result!.PlannerId, Is.EqualTo(created.PlannerId));
    }

    [Test]
    public async Task GetById_NonExistentPlanner_ReturnsNotFound()
    {
        HttpResponseMessage response = await this.Client.GetAsync("/api/planners/999999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetByUserId_NoPlannersForUser_ReturnsEmptyList()
    {
        HttpResponseMessage response = await this.Client.GetAsync("/api/planners/user/999999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        PlannersDataModel? planners = await response.Content.ReadFromJsonAsync<PlannersDataModel>();
        Assert.That(planners?.Planners, Is.Empty);
    }
}