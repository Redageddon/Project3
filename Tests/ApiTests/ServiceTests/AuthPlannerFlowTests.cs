using API.DataModels.Food;
using API.DataModels.Users;
using API.Services;
using Microsoft.AspNetCore.Identity;

namespace Tests.ApiTests.ServiceTests;

// AuthPlannerFlowTests – Chain Pipeline: Register -> Login -> Session -> Planner

[TestFixture]
public class AuthPlannerFlowTests
{
    private const string UsersDataPath = "Data/users.json";
    private const string PlannersDataPath = "Data/planners.json";

    [SetUp]
    public void SetUp()
    {
        if (File.Exists(UsersDataPath))
        {
            File.Delete(UsersDataPath);
        }

        if (File.Exists(PlannersDataPath))
        {
            File.Delete(PlannersDataPath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(UsersDataPath))
        {
            File.Delete(UsersDataPath);
        }

        if (File.Exists(PlannersDataPath))
        {
            File.Delete(PlannersDataPath);
        }
    }

    private static PlannerModel CreateEmptyPlannerForUser(int userId)
    {
        return new PlannerModel(PlannerId: 0,
                                UserId: userId,
                                PlannedDate: DateTime.UtcNow,
                                BreakfastId: 0,
                                LunchId: 0,
                                DinnerId: 0);
    }

    [Test]
    public void Register_Login_CreatePlanner_ChainsCorrectly()
    {
        // Arrange – build real services
        UserRepository userRepository = new();
        PasswordHasher<UserModel> passwordHasher = new();
        UserAuth userAuth = new(userRepository, passwordHasher);
        SessionService sessionService = new();
        PlannerRepository plannerRepository = new();

        // 1) Register
        RegisterRequest registerRequest = new(
                                              Username: "chainedUser",
                                              Email: "chain@example.com",
                                              Password: "ChainPassword123!");

        LoginResponse registerResponse = userAuth.Register(registerRequest);
        Assert.That(registerResponse.Success, Is.True);
        Assert.That(registerResponse.User, Is.Not.Null);

        // 2) Login
        LoginRequest loginRequest = new(
                                        Email: "chain@example.com",
                                        Password: "ChainPassword123!");

        LoginResponse loginResponse = userAuth.Login(loginRequest);
        Assert.That(loginResponse.Success, Is.True);
        Assert.That(loginResponse.User, Is.Not.Null);

        int userId = loginResponse.User!.Uid;

        // 3) Create a session for logged-in user
        string sessionId = sessionService.CreateSession(userId);
        int? userIdFromSession = sessionService.GetUserId(sessionId);

        Assert.That(userIdFromSession, Is.Not.Null);
        Assert.That(userIdFromSession!.Value, Is.EqualTo(userId));

        // 4) Create a planner for this user
        PlannerModel planner = CreateEmptyPlannerForUser(userIdFromSession.Value);
        PlannerModel createdPlanner = plannerRepository.CreatePlanner(planner);

        // 5) Validate planner
        Assert.That(createdPlanner.UserId, Is.EqualTo(userIdFromSession.Value));
        Assert.That(createdPlanner.PlannerId, Is.GreaterThan(0));
    }
}
