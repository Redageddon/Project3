using API.DataModels.Food;
using API.Services;

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
        return new PlannerModel(PlannerId: 0, // repository will assign
                                UserId: userId,
                                PlannedDate: DateTime.UtcNow,
                                BreakfastId: null,
                                LunchId: null,
                                DinnerId: null);
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

    [Test]
    public void GetPlannerById_ExistingPlanner_ReturnsPlanner()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(userId: 1);
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        PlannerModel? result = this.plannerRepository.GetPlannerById(created.PlannerId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.PlannerId, Is.EqualTo(created.PlannerId));
        Assert.That(result.UserId, Is.EqualTo(1));
    }

    [Test]
    public void GetPlannerById_NonExistentPlanner_ReturnsNull()
    {
        PlannerModel? result = this.plannerRepository.GetPlannerById(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void UpdatePlanner_ExistingPlanner_UpdatesSuccessfully()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(userId: 1);
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        PlannerModel updated = created with 
        { 
            BreakfastId = 10, 
            LunchId = 20, 
            DinnerId = 30,
        };

        PlannerModel? result = this.plannerRepository.UpdatePlanner(created.PlannerId, updated);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.BreakfastId, Is.EqualTo(10));
        Assert.That(result.LunchId, Is.EqualTo(20));
        Assert.That(result.DinnerId, Is.EqualTo(30));
        Assert.That(result.UserId, Is.EqualTo(1)); // UserId preserved
    }

    [Test]
    public void UpdatePlanner_PreservesOriginalUserId()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(userId: 1);
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        // Try to change userId (should be preserved)
        PlannerModel updated = created with { UserId = 999 };

        PlannerModel? result = this.plannerRepository.UpdatePlanner(created.PlannerId, updated);

        Assert.That(result!.UserId, Is.EqualTo(1)); // Original userId preserved
    }

    [Test]
    public void UpdatePlanner_NonExistentPlanner_ReturnsNull()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(userId: 1);

        PlannerModel? result = this.plannerRepository.UpdatePlanner(999, planner);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void DeletePlanner_ExistingPlanner_ReturnsTrue()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(userId: 1);
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        bool result = this.plannerRepository.DeletePlanner(created.PlannerId);

        Assert.That(result, Is.True);
        Assert.That(this.plannerRepository.GetPlannerById(created.PlannerId), Is.Null);
    }

    [Test]
    public void DeletePlanner_NonExistentPlanner_ReturnsFalse()
    {
        bool result = this.plannerRepository.DeletePlanner(999);

        Assert.That(result, Is.False);
    }

    [Test]
    public void GetAllPlanners_ReturnsNonNull()
    {
        PlannersDataModel result = this.plannerRepository.GetAllPlanners();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Planners, Is.Not.Null);
    }

    [Test]
    public void GetAllPlanners_AfterDelete_DoesNotContainDeletedPlanner()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(userId: 1);
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        this.plannerRepository.DeletePlanner(created.PlannerId);
        PlannersDataModel result = this.plannerRepository.GetAllPlanners();

        Assert.That(result.Planners.Any(p => p.PlannerId == created.PlannerId), Is.False);
    }
}
