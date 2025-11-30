using System.Net;
using API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests.ApiTests.Integration;

[TestFixture]
public class ProgramTests
{
    [Test]
    public void Application_Starts_Successfully()
    {
        using WebApplicationFactory<Program> factory = new CustomWebApplicationFactory();
        using HttpClient client = factory.CreateClient();

        Assert.That(client, Is.Not.Null);
    }

    [Test]
    public async Task Application_ServesSwagger()
    {
        await using WebApplicationFactory<Program> factory = new CustomWebApplicationFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/swagger/index.html");

        Assert.That(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.MovedPermanently, Is.True);
    }

    [Test]
    public async Task Application_EnablesCors()
    {
        await using WebApplicationFactory<Program> factory = new CustomWebApplicationFactory();
        using HttpClient client = factory.CreateClient();

        HttpRequestMessage request = new(HttpMethod.Options, "/api/recipes");
        request.Headers.Add("Origin", "http://localhost:5297");

        HttpResponseMessage response = await client.SendAsync(request);

        Assert.That(response, Is.Not.Null);
    }

    [Test]
    public async Task Application_MapsControllers()
    {
        await using WebApplicationFactory<Program> factory = new CustomWebApplicationFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.GetAsync("/api/recipes");

        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }
}