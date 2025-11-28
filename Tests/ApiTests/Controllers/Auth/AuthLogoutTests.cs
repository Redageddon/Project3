using System.Net;
using System.Net.Http.Json;
using API.DataModels.Users;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Auth;

[TestFixture]
public class AuthLogoutTests : TestFixtureBase
{
    [Test]
    public async Task Logout_WithValidSessionId_ReturnsOk()
    {
        RegisterRequest registerRequest = TestDataBuilder.CreateRegisterRequest();
        await this.Client.PostAsJsonAsync("/api/auth/register", registerRequest);

        LoginRequest loginRequest = TestDataBuilder.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        HttpResponseMessage loginResponse = await this.Client.PostAsJsonAsync("/api/auth/login", loginRequest);
        LoginResponse? loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        LogoutRequest logoutRequest = new(loginResult!.SessionId!);
        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/logout", logoutRequest);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Logout_WithEmptySessionId_ReturnsBadRequest()
    {
        LogoutRequest request = new("");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/logout", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Logout_WithNullSessionId_ReturnsBadRequest()
    {
        LogoutRequest request = new(null!);

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/logout", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Logout_WithInvalidSessionId_ReturnsOk()
    {
        LogoutRequest request = new("invalid-session-id");

        HttpResponseMessage response = await this.Client.PostAsJsonAsync("/api/auth/logout", request);

        // Even invalid sessions should return OK (logout is idempotent)
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
