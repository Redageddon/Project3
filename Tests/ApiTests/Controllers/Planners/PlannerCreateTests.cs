using System.Net;
using System.Net.Http.Json;
using API.DataModels.Food;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Planners;

[TestFixture]
public class PlannerCreateTests : TestFixtureBase
{
    [Test]
    public async Task Create_WithValidData_ReturnsCreated()
    {
        PlannerModel planner = TestDataBuilder.CreatePlanner();

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/planners", planner);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        PlannerModel? created = await response.Content.ReadFromJsonAsync<PlannerModel>();
        Assert.That(created!.PlannerId, Is.GreaterThan(0));
        Assert.That(response.Headers.Location!.ToString(), Does.Contain($"/api/planners/{created.PlannerId}"));
    }

    [Test]
    public async Task Create_WithInvalidModel_ReturnsBadRequest()
    {
        PlannerModel invalid = TestDataBuilder.CreateInvalidPlanner();

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/planners", invalid);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Create_WithoutSession_ReturnsUnauthorized()
    {
        PlannerModel planner = TestDataBuilder.CreatePlanner();
        HttpClient clientWithoutSession = this.Factory.CreateClient();

        HttpResponseMessage response = await clientWithoutSession.PostAsJsonAsync("/api/planners", planner);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Create_WithInvalidSession_ReturnsUnauthorized()
    {
        PlannerModel planner = TestDataBuilder.CreatePlanner();

        HttpRequestMessage request = new(HttpMethod.Post, "/api/planners")
        {
            Content = JsonContent.Create(planner),
        };

        request.Headers.Add("X-Session-Id", "invalid-session-id");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
}