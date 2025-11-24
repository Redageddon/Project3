using API.DataModels.Users;
using API.Services;

namespace Tests.ApiTests.Repositories;

[TestFixture]
public class UserRepositoryTests
{
    private UserRepository repository = null!;
    private const string TestDataPath = "Data/users_test.json";

    [SetUp]
    public void SetUp()
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
    public void EnsureDataFileExists_CreatesFileIfNotExists()
    {
        UserRepository _ = new();

        Assert.That(File.Exists("Data/users.json"), Is.True);
    }

    [Test]
    public void EnsureDataFileExists_DoesNotOverwriteExistingFile()
    {
        UserModel _ = this.repository.CreateUser("testuser", "test@example.com", "hash");
        
        // Create a new instance which will call EnsureDataFileExists again
        UserRepository newRepo = new();
        
        UsersData users = newRepo.GetAllUsers();
        Assert.That(users.Users.Any(u => u.Email == "test@example.com"), Is.True);
    }

    [Test]
    public void EnsureDataFileExists_CreatesDirectoryIfNotExists()
    {
        string dataDir = Path.GetDirectoryName("Data/users.json")!;

        if (Directory.Exists(dataDir))
        {
            Directory.Delete(dataDir, true);
        }

        UserRepository _ = new();

        Assert.That(Directory.Exists(dataDir), Is.True);
    }
}
