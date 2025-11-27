using API.DataModels.Food;
using API.Services;
using NUnit.Framework;

namespace Tests.ApiTests.ServiceTests;

// PlannerRepositoryTests – PlannerId + UserId
    
[TestFixture]
public class PlannerRepositoryTests
{
    private const string PlannersDataPath = "Data/planners.json";
    private PlannerRepository plannerRepository = null!;

    [SetUp]
    public void SetUp()
    {
        if (File.Exists(PlannersDataPath))
        {
            File.Delete(PlannersDataPath);
        }

        this.plannerRepository = new PlannerRepository();
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(PlannersDataPath))
        {
            File.Delete(PlannersDataPath);
        }
    }

    private static PlannerModel CreateEmptyPlannerForUser(int userId)
    {
        return new PlannerModel(
                                PlannerId: 0, // repository will assign
                                UserId: userId,
                                PlannedDate: DateTime.UtcNow,
                                Breakfast: new List<RecipeModel>(),
                                Lunch: new List<RecipeModel>(),
                                Dinner: new List<RecipeModel>(),
                                Desert: new List<RecipeModel>());
    }

    [Test]
    public void CreatePlanner_AssignsNewPlannerId_AndKeepsUserId()
    {
        // Arrange
        PlannerModel planner = CreateEmptyPlannerForUser(userId: 42);

        // Act
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        // Assert
        Assert.That(created.UserId, Is.EqualTo(42));
        Assert.That(created.PlannerId, Is.GreaterThan(0));
    }

    [Test]
    public void GetPlannersByUserId_ReturnsOnlyThatUsersPlanners()
    {
        // Arrange
        PlannerModel plannerUser1 = CreateEmptyPlannerForUser(userId: 1);
        PlannerModel plannerUser2 = CreateEmptyPlannerForUser(userId: 2);

        this.plannerRepository.CreatePlanner(plannerUser1);
        this.plannerRepository.CreatePlanner(plannerUser2);
        this.plannerRepository.CreatePlanner(plannerUser1); // user 1 has 2 planners

        // Act
        List<PlannerModel> user1Planners = this.plannerRepository.GetPlannersByUserId(1);
        List<PlannerModel> user2Planners = this.plannerRepository.GetPlannersByUserId(2);

        // Assert
        Assert.That(user1Planners.Count, Is.EqualTo(2));
        Assert.That(user1Planners.All(p => p.UserId == 1), Is.True);

        Assert.That(user2Planners.Count, Is.EqualTo(1));
        Assert.That(user2Planners[0].UserId, Is.EqualTo(2));
    }
}
