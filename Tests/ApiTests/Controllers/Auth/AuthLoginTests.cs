using System.Net;
using System.Net.Http.Json;
using API.DataModels.Users;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Auth;

[TestFixture]
public class AuthLoginTests : TestFixtureBase
{
    [Test]
    public async Task Login_WithValidCredentials_ReturnsOk()
    {
        RegisterRequest registerRequest = TestDataBuilder.CreateRegisterRequest();
        await this.Client.PostAsJsonAsync("/api/auth/register", registerRequest);

        LoginRequest loginRequest = TestDataBuilder.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", loginRequest);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.That(result!.Success, Is.True);
        Assert.That(result.Message, Is.EqualTo("Login successful"));
        Assert.That(result.User!.Email, Is.EqualTo(registerRequest.Email));
    }

    [Test]
    public async Task Login_WithInvalidEmail_ReturnsUnauthorized()
    {
        LoginRequest request = TestDataBuilder.CreateLoginRequest("nonexistent@example.com", "Password123!");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.That(result!.Success, Is.False);
        Assert.That(result.Message, Is.EqualTo("Invalid email or password"));
    }

    [Test]
    public async Task Login_WithWrongPassword_ReturnsUnauthorized()
    {
        RegisterRequest registerRequest = TestDataBuilder.CreateRegisterRequest();
        await this.Client.PostAsJsonAsync("/api/auth/register", registerRequest);

        LoginRequest loginRequest = TestDataBuilder.CreateLoginRequest(registerRequest.Email, "WrongPassword!");
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", loginRequest);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.That(result!.Success, Is.False);
        Assert.That(result.Message, Is.EqualTo("Invalid email or password"));
    }

    [Test]
    public async Task Login_WithInvalidEmailFormat_ReturnsBadRequest()
    {
        LoginRequest request = new("not-an-email", "Password123!");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Login_WithShortPassword_ReturnsBadRequest()
    {
        LoginRequest request = TestDataBuilder.CreateLoginRequest("test@example.com", "12345");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Login_WithMissingEmail_ReturnsBadRequest()
    {
        var request = new { Password = "Test123!" };

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Login_WithMissingPassword_ReturnsBadRequest()
    {
        var request = new { Email = "test@example.com" };

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Login_WithEmptyEmail_ReturnsBadRequest()
    {
        LoginRequest request = new("", "Password123!");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Login_WithEmptyPassword_ReturnsBadRequest()
    {
        LoginRequest request = new("test@example.com", "");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/login", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
