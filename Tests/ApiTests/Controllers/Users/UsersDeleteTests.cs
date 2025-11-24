using System.Net;
using API.DataModels.Users;
using Tests.ApiTests.Helpers;

namespace Tests.ApiTests.Controllers.Users;

[TestFixture]
public class UsersDeleteTests : TestFixtureBase
{
    [Test]
    public async Task Delete_ExistingUser_ReturnsNoContent()
    {
        UserDto user = await this.CreateUser();

        HttpResponseMessage response = await this.Client.DeleteAsync($"/api/users/{user.Uid}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task Delete_NonExistentUser_ReturnsNotFound()
    {
        HttpResponseMessage response = await this.Client.DeleteAsync("/api/users/999");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}
