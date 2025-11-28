using API.DataModels.Users;
using API.Services;

namespace Tests.ApiTests.ServiceTests;

// UserAuthTests – Register + Login

[TestFixture]
public class UserAuthTests
{
    private const string UsersDataPath = "Data/users.json";

    private UserRepository userRepository = null!;
    private PasswordHasher passwordHasher = null!;
    private UserAuth userAuth = null!;

    [SetUp]
    public void SetUp()
    {
        if (File.Exists(UsersDataPath))
        {
            File.Delete(UsersDataPath);
        }

        this.userRepository = new UserRepository();
        this.passwordHasher = new PasswordHasher();
        this.userAuth = new UserAuth(this.userRepository, this.passwordHasher);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(UsersDataPath))
        {
            File.Delete(UsersDataPath);
        }
    }

    [Test]
    public void Register_ThenLogin_WithSameCredentials_Succeeds_AndReturnsSameUserId()
    {
        // Arrange
        RegisterRequest registerRequest = new(
                                              Username: "alice",
                                              Email: "alice@example.com",
                                              Password: "Password123!");

        // Act – register
        LoginResponse registerResponse = this.userAuth.Register(registerRequest);

        // Assert register
        Assert.That(registerResponse.Success, Is.True);
        Assert.That(registerResponse.User, Is.Not.Null);
        Assert.That(registerResponse.User!.Username, Is.EqualTo("alice"));
        Assert.That(registerResponse.User.Email, Is.EqualTo("alice@example.com"));

        // Act – login
        LoginRequest loginRequest = new(
                                        Email: "alice@example.com",
                                        Password: "Password123!");

        LoginResponse loginResponse = this.userAuth.Login(loginRequest);

        // Assert login
        Assert.That(loginResponse.Success, Is.True);
        Assert.That(loginResponse.User, Is.Not.Null);
        Assert.That(loginResponse.User!.Uid, Is.EqualTo(registerResponse.User!.Uid));
    }

    [Test]
    public void Login_WithWrongPassword_Fails()
    {
        // Arrange
        RegisterRequest registerRequest = new(
                                              Username: "bob",
                                              Email: "bob@example.com",
                                              Password: "CorrectPassword1!");

        this.userAuth.Register(registerRequest);

        LoginRequest wrongPasswordLogin = new(
                                              Email: "bob@example.com",
                                              Password: "WrongPassword!");

        // Act
        LoginResponse response = this.userAuth.Login(wrongPasswordLogin);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.User, Is.Null);
        Assert.That(response.Message, Is.EqualTo("Invalid email or password"));
    }
}
