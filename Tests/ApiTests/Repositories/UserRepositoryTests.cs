using API.DataModels.Users;
using API.Services;

namespace Tests.ApiTests.Repositories;

[TestFixture]
public class UserRepositoryTests
{
    [SetUp]
    public void SetUp()
    {
        this.testDataPath = Path.Combine(Path.GetTempPath(), $"users_test_{Guid.NewGuid()}.json");
        this.repository = new UserRepository(this.testDataPath);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(this.testDataPath))
        {
            File.Delete(this.testDataPath);
        }
    }

    private UserRepository repository = null!;
    private string testDataPath = null!;

    [Test]
    public void EnsureDataFileExists_DoesNotOverwriteExistingFile()
    {
        UserModel _ = this.repository.CreateUser("testuser", "test@example.com", "hash");

        // Create a new instance pointing to same file
        UserRepository newRepo = new(this.testDataPath);

        UsersData users = newRepo.GetAllUsers();
        Assert.That(users.Users.Any(u => u.Email == "test@example.com"), Is.True);
    }
}