
using System.Net;
using System.Net.Http.Json;
using API.DataModels.Users;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Users;

[TestFixture]
public class UsersUpdateTests : TestFixtureBase
{
    [Test]
    public async Task Update_WithValidData_ReturnsOk()
    {
        UserDto user = await this.CreateUser();
        UserUpdateRequest request = new("newusername", null, null);

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/users/{user.Uid}", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        UserDto? result = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.That(result!.Username, Is.EqualTo("newusername"));
    }

    [Test]
    public async Task Update_Email_UpdatesSuccessfully()
    {
        UserDto user = await this.CreateUser();
        string newEmail = $"new_{Guid.NewGuid():N}@example.com";
        UserUpdateRequest request = new(null, newEmail, null);

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/users/{user.Uid}", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        UserDto? result = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.That(result!.Email, Is.EqualTo(newEmail));
    }

    [Test]
    public async Task Update_Password_UpdatesSuccessfully()
    {
        UserDto user = await this.CreateUser();
        UserUpdateRequest request = new(null, null, "newhash123");

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/users/{user.Uid}", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Update_NonExistentUser_ReturnsNotFound()
    {
        UserUpdateRequest request = new("newusername", null, null);

        HttpResponseMessage response = await this.Client.PutAsJsonAsync("/api/users/999999", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Update_WithNull_ReturnsBadRequest()
    {
        UserDto user = await this.CreateUser();

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/users/{user.Uid}", (UserUpdateRequest?)null);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_AllFieldsNull_ReturnsOk()
    {
        UserDto user = await this.CreateUser();
        UserUpdateRequest request = new(null, null, null);

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/users/{user.Uid}", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        UserDto? result = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.That(result!.Username, Is.EqualTo(user.Username));
        Assert.That(result.Email, Is.EqualTo(user.Email));
    }

    [Test]
    public async Task Update_AllFields_UpdatesSuccessfully()
    {
        UserDto user = await this.CreateUser();
        string newEmail = $"updated_{Guid.NewGuid():N}@example.com";
        UserUpdateRequest request = new("updateduser", newEmail, "newhash");

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/users/{user.Uid}", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        UserDto? result = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.That(result!.Username, Is.EqualTo("updateduser"));
        Assert.That(result.Email, Is.EqualTo(newEmail));
    }

    [Test]
    public async Task Update_WithShortUsername_ReturnsBadRequest()
    {
        UserDto user = await this.CreateUser();
        UserUpdateRequest request = new("ab", null, null);

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/users/{user.Uid}", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_WithInvalidEmail_ReturnsBadRequest()
    {
        UserDto user = await this.CreateUser();
        UserUpdateRequest request = new(null, "not-an-email", null);

        HttpResponseMessage response = await this.Client.PutAsJsonAsync($"/api/users/{user.Uid}", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
