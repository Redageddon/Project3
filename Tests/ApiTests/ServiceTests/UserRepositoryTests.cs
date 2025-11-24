using API.DataModels.Users;
using API.Services;

namespace Tests.ApiTests.ServiceTests;

[TestFixture]
public class UserRepositoryTests
{
    private UserRepository repository = null!;
    private const string TestDataPath = "Data/users.json";

    [SetUp]
    public void Setup()
    {
        if (File.Exists(TestDataPath))
        {
            File.Delete(TestDataPath);
        }

        this.repository = new UserRepository();
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(TestDataPath))
        {
            File.Delete(TestDataPath);
        }
    }

    [Test]
    public void UpdateUser_NonExistentUser_ReturnsNull()
    {
        UserModel? result = this.repository.UpdateUser(999, "newname");

        Assert.That(result, Is.Null);
    }

    [Test]
    public void UpdateUser_UpdateUsername_UpdatesOnlyUsername()
    {
        UserModel user = this.repository.CreateUser("oldname", "test@example.com", "hash123");

        UserModel? result = this.repository.UpdateUser(user.Uid, username: "newname");

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Username, Is.EqualTo("newname"));
        Assert.That(result.Email, Is.EqualTo("test@example.com"));
        Assert.That(result.PasswordHash, Is.EqualTo("hash123"));
    }

    [Test]
    public void UpdateUser_UpdateEmail_UpdatesOnlyEmail()
    {
        UserModel user = this.repository.CreateUser("testuser", "old@example.com", "hash123");

        UserModel? result = this.repository.UpdateUser(user.Uid, email: "new@example.com");

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Email, Is.EqualTo("new@example.com"));
        Assert.That(result.Username, Is.EqualTo("testuser"));
        Assert.That(result.PasswordHash, Is.EqualTo("hash123"));
    }

    [Test]
    public void UpdateUser_UpdatePassword_UpdatesOnlyPassword()
    {
        UserModel user = this.repository.CreateUser("testuser", "test@example.com", "oldhash");

        UserModel? result = this.repository.UpdateUser(user.Uid, passwordHash: "newhash");

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.PasswordHash, Is.EqualTo("newhash"));
        Assert.That(result.Username, Is.EqualTo("testuser"));
        Assert.That(result.Email, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void UpdateUser_UpdateMultipleFields_UpdatesAll()
    {
        UserModel user = this.repository.CreateUser("oldname", "old@example.com", "oldhash");

        UserModel? result = this.repository.UpdateUser(user.Uid, "newname", "new@example.com", "newhash");

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Username, Is.EqualTo("newname"));
        Assert.That(result.Email, Is.EqualTo("new@example.com"));
        Assert.That(result.PasswordHash, Is.EqualTo("newhash"));
    }
}
