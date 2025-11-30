using System.Net;
using System.Net.Http.Json;
using API.DataModels.Users;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Auth;

[TestFixture]
public class AuthRegisterTests : TestFixtureBase
{
    [Test]
    public async Task Register_WithValidData_ReturnsOk()
    {
        RegisterRequest request = TestDataBuilder.CreateRegisterRequest();

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.That(result!.Success, Is.True);
        Assert.That(result.Message, Is.EqualTo("Registration successful"));
        Assert.That(result.User!.Username, Is.EqualTo(request.Username));
        Assert.That(result.User.Email, Is.EqualTo(request.Email));
    }

    [Test]
    public async Task Register_WithDuplicateEmail_ReturnsBadRequest()
    {
        string email = $"duplicate_{Guid.NewGuid():N}@example.com";
        RegisterRequest first = TestDataBuilder.CreateRegisterRequest(email: email);
        RegisterRequest second = TestDataBuilder.CreateRegisterRequest(email: email);

        await this.Client.PostAsJsonAsync("/api/auth/register", first);
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", second);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.That(result!.Success, Is.False);
        Assert.That(result.Message, Is.EqualTo("Email already registered"));
    }

    [Test]
    public async Task Register_WithDuplicateUsername_ReturnsBadRequest()
    {
        string username = $"duplicate_{Guid.NewGuid():N}";
        RegisterRequest first = TestDataBuilder.CreateRegisterRequest(username);
        RegisterRequest second = TestDataBuilder.CreateRegisterRequest(username);

        await this.Client.PostAsJsonAsync("/api/auth/register", first);
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", second);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.That(result!.Success, Is.False);
        Assert.That(result.Message, Is.EqualTo("Username already taken"));
    }

    [Test]
    public async Task Register_WithInvalidEmail_ReturnsBadRequest()
    {
        RegisterRequest request = new("testuser", "not-an-email", "Password123!");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Register_WithShortPassword_ReturnsBadRequest()
    {
        RegisterRequest request = TestDataBuilder.CreateRegisterRequest(password: "12345");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Register_WithShortUsername_ReturnsBadRequest()
    {
        RegisterRequest request = TestDataBuilder.CreateRegisterRequest("ab");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Register_WithMissingEmail_ReturnsBadRequest()
    {
        var request = new
        {
            Username = "testuser",
            Password = "Test123!",
        };

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Register_WithMissingPassword_ReturnsBadRequest()
    {
        var request = new
        {
            Username = "testuser",
            Email = "test@example.com",
        };

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Register_WithEmptyUsername_ReturnsBadRequest()
    {
        RegisterRequest request = TestDataBuilder.CreateRegisterRequest("");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Register_WithEmptyEmail_ReturnsBadRequest()
    {
        RegisterRequest request = new("testuser", "", "Password123!");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/register", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}