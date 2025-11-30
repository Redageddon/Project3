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

    // NEW: Authorization tests
    [Test]
    public async Task Delete_WithoutSession_ReturnsUnauthorized()
    {
        PlannerModel created = await this.CreatePlanner();
        HttpClient clientWithoutSession = this.Factory.CreateClient();

        HttpResponseMessage response = await clientWithoutSession.DeleteAsync($"/api/planners/{created.PlannerId}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_WithEmptySession_ReturnsUnauthorized()
    {
        PlannerModel created = await this.CreatePlanner();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/planners/{created.PlannerId}");
        request.Headers.Add("X-Session-Id", "");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_WithInvalidSession_ReturnsUnauthorized()
    {
        PlannerModel created = await this.CreatePlanner();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/planners/{created.PlannerId}");
        request.Headers.Add("X-Session-Id", "invalid-session-id");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Delete_PlannerOwnedByDifferentUser_ReturnsForbidden()
    {
        // Create planner with first user
        PlannerModel created = await this.CreatePlanner();

        // Create and login as second user
        string secondUserSessionId = await this.CreateAndLoginSecondUser();

        HttpRequestMessage request = new(HttpMethod.Delete, $"/api/planners/{created.PlannerId}");
        request.Headers.Add("X-Session-Id", secondUserSessionId);

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [Test]
    public async Task Delete_VerifyResourceActuallyDeleted()
    {
        PlannerModel created = await this.CreatePlanner();

        HttpResponseMessage deleteResponse = await this.Client.DeleteAsync($"/api/planners/{created.PlannerId}");
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

        // Verify it's actually gone
        HttpResponseMessage getResponse = await this.Client.GetAsync($"/api/planners/{created.PlannerId}");
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        // Verify second delete also returns NotFound
        HttpResponseMessage secondDeleteResponse = await this.Client.DeleteAsync($"/api/planners/{created.PlannerId}");
        Assert.That(secondDeleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}