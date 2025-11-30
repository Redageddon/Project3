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

    [Test]
    public async Task Update_WithNull_ReturnsBadRequest()
    {
        PlannerModel created = await this.CreatePlanner();

        HttpResponseMessage response =
            await this.Client.PutAsJsonAsync($"/api/planners/{created.PlannerId}", (PlannerModel?)null);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    // NEW: Authorization tests
    [Test]
    public async Task Update_WithoutSession_ReturnsUnauthorized()
    {
        PlannerModel created = await this.CreatePlanner();
        HttpClient clientWithoutSession = this.Factory.CreateClient();
        
        PlannerModel updated = created with { BreakfastId = 100 };

        HttpResponseMessage response = await clientWithoutSession.PutAsJsonAsync($"/api/planners/{created.PlannerId}", updated);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Update_WithEmptySession_ReturnsUnauthorized()
    {
        PlannerModel created = await this.CreatePlanner();
        PlannerModel updated = created with { BreakfastId = 100 };

        HttpRequestMessage request = new(HttpMethod.Put, $"/api/planners/{created.PlannerId}")
        {
            Content = JsonContent.Create(updated),
        };
        
        request.Headers.Add("X-Session-Id", "");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Update_WithInvalidSession_ReturnsUnauthorized()
    {
        PlannerModel created = await this.CreatePlanner();
        PlannerModel updated = created with { BreakfastId = 100 };

        HttpRequestMessage request = new(HttpMethod.Put, $"/api/planners/{created.PlannerId}")
        {
            Content = JsonContent.Create(updated),
        };
        
        request.Headers.Add("X-Session-Id", "invalid-session-id");

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Update_PlannerOwnedByDifferentUser_ReturnsForbidden()
    {
        // Create planner with first user
        PlannerModel created = await this.CreatePlanner();

        // Create and login as second user
        string secondUserSessionId = await this.CreateAndLoginSecondUser();

        PlannerModel updated = created with { BreakfastId = 999 };

        HttpRequestMessage request = new(HttpMethod.Put, $"/api/planners/{created.PlannerId}")
        {
            Content = JsonContent.Create(updated),
        };
        
        request.Headers.Add("X-Session-Id", secondUserSessionId);

        HttpResponseMessage response = await this.Client.SendAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [Test]
    public async Task Update_PreservesUserId()
    {
        PlannerModel created = await this.CreatePlanner();
        int originalUserId = created.UserId;

        // Try to change userId in the request (should be ignored)
        PlannerModel updated = created with
        {
            UserId = 99999,
            BreakfastId = 100,
        };

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/planners/{created.PlannerId}", updated);

        PlannerModel? result = await response.Content.ReadFromJsonAsync<PlannerModel>();
        
        // UserId should remain unchanged
        Assert.That(result!.UserId, Is.EqualTo(originalUserId));
        Assert.That(result.BreakfastId, Is.EqualTo(100));
    }
}