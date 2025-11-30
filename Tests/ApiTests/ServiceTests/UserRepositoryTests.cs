using API.DataModels.Users;
using API.Services;

namespace Tests.ApiTests.ServiceTests;

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

        // Create a new instance which will call EnsureDataFileExists again
        UserRepository newRepo = new(this.testDataPath);

        UsersData users = newRepo.GetAllUsers();
        Assert.That(users.Users.Any(u => u.Email == "test@example.com"), Is.True);
    }

    [Test]
    public void EnsureDataFileExists_CreatesDirectory_WhenItDoesNotExist()
    {
        // Arrange
        string nestedPath = Path.Combine(Path.GetTempPath(), $"test_dir_{Guid.NewGuid()}", "nested", "users.json");

        try
        {
            // Act
            UserRepository repoWithNestedPath = new(nestedPath);
            repoWithNestedPath.CreateUser("testuser", "test@example.com", "hash");

            // Assert
            Assert.That(File.Exists(nestedPath), Is.True);
            Assert.That(Directory.Exists(Path.GetDirectoryName(nestedPath)), Is.True);
        }
        finally
        {
            // Cleanup
            if (File.Exists(nestedPath))
            {
                File.Delete(nestedPath);
            }

            string? directory = Path.GetDirectoryName(nestedPath);

            if (directory != null
             && Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }
    }

    [Test]
    public void EnsureDataFileExists_HandlesExistingDirectory()
    {
        // Arrange
        string tempDir = Path.Combine(Path.GetTempPath(), $"test_dir_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempDir);
        string dataPath = Path.Combine(tempDir, "users.json");

        try
        {
            // Act
            UserRepository repoWithExistingDir = new(dataPath);
            repoWithExistingDir.CreateUser("testuser", "test@example.com", "hash");

            // Assert
            Assert.That(File.Exists(dataPath), Is.True);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}