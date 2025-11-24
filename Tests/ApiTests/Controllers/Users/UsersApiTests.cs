using System.Net;
using System.Net.Http.Json;
using API.DataModels.Users;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Users;

[TestFixture]
public class UsersGetTests : TestFixtureBase
{
    [Test]
    public async Task GetAll_ReturnsListOfUsers()
    {
        await this.CreateUser();

        HttpResponseMessage response = await this.Client.GetAsync("/api/users");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        List<UserDto>? users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
        Assert.That(users, Is.Not.Null);
        Assert.That(users!.Count, Is.GreaterThan(0));
    }

    [Test]
    public async Task GetById_ExistingUser_ReturnsOk()
    {
        UserDto user = await this.CreateUser();

        HttpResponseMessage response = await this.Client.GetAsync($"/api/users/{user.Uid}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        UserDto? result = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.That(result!.Uid, Is.EqualTo(user.Uid));
    }

    [Test]
    public async Task GetById_NonExistentUser_ReturnsNotFound()
    {
        HttpResponseMessage response = await this.Client.GetAsync("/api/users/999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
