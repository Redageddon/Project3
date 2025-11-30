using API.DataModels.Food;
using API.Services;

namespace Tests.ApiTests.ServiceTests;

// PlannerRepositoryTests – PlannerId + UserId

[TestFixture]
public class PlannerRepositoryTests
{
    [SetUp]
    public void SetUp()
    {
        this.testDataPath = Path.Combine(Path.GetTempPath(), $"planners_test_{Guid.NewGuid()}.json");
        this.plannerRepository = new PlannerRepository(this.testDataPath);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(this.testDataPath))
        {
            File.Delete(this.testDataPath);
        }
    }

    private string testDataPath = null!;
    private PlannerRepository plannerRepository = null!;

    private static PlannerModel CreateEmptyPlannerForUser(int userId)
    {
        return new PlannerModel(0, // repository will assign
                                userId,
                                DateTime.UtcNow,
                                null,
                                null,
                                null);
    }

    [Test]
    public void CreatePlanner_AssignsNewPlannerId_AndKeepsUserId()
    {
        // Arrange
        PlannerModel planner = CreateEmptyPlannerForUser(42);

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
        PlannerModel plannerUser1 = CreateEmptyPlannerForUser(1);
        PlannerModel plannerUser2 = CreateEmptyPlannerForUser(2);

        this.plannerRepository.CreatePlanner(plannerUser1);
        this.plannerRepository.CreatePlanner(plannerUser2);
        this.plannerRepository.CreatePlanner(plannerUser1); // user 1 has 2 planners

        // Act
        PlannersDataModel user1Planners = this.plannerRepository.GetPlannersByUserId(1);
        PlannersDataModel user2Planners = this.plannerRepository.GetPlannersByUserId(2);

        // Assert
        Assert.That(user1Planners.Planners.Count, Is.EqualTo(2));
        Assert.That(user1Planners.Planners.All(p => p.UserId == 1), Is.True);

        Assert.That(user2Planners.Planners.Count, Is.EqualTo(1));
        Assert.That(user2Planners.Planners[0].UserId, Is.EqualTo(2));
    }

    [Test]
    public void GetPlannerById_ExistingPlanner_ReturnsPlanner()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(1);
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
        PlannerModel planner = CreateEmptyPlannerForUser(1);
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
        PlannerModel planner = CreateEmptyPlannerForUser(1);
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        // Try to change userId (should be preserved)
        PlannerModel updated = created with { UserId = 999 };

        PlannerModel? result = this.plannerRepository.UpdatePlanner(created.PlannerId, updated);

        Assert.That(result!.UserId, Is.EqualTo(1)); // Original userId preserved
    }

    [Test]
    public void UpdatePlanner_NonExistentPlanner_ReturnsNull()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(1);

        PlannerModel? result = this.plannerRepository.UpdatePlanner(999, planner);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void DeletePlanner_ExistingPlanner_ReturnsTrue()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(1);
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
        PlannerModel planner = CreateEmptyPlannerForUser(1);
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        this.plannerRepository.DeletePlanner(created.PlannerId);
        PlannersDataModel result = this.plannerRepository.GetAllPlanners();

        Assert.That(result.Planners.Any(p => p.PlannerId == created.PlannerId), Is.False);
    }

    [Test]
    public void EnsureDataFileExists_DoesNotOverwriteExistingFile()
    {
        PlannerModel planner = CreateEmptyPlannerForUser(1);
        PlannerModel created = this.plannerRepository.CreatePlanner(planner);

        // Create a new instance which will call EnsureDataFileExists again
        PlannerRepository newRepo = new(this.testDataPath);

        PlannersDataModel planners = newRepo.GetAllPlanners();
        Assert.That(planners.Planners.Any(p => p.PlannerId == created.PlannerId), Is.True);
    }

    [Test]
    public void EnsureDataFileExists_CreatesDirectory_WhenItDoesNotExist()
    {
        // Arrange
        string nestedPath = Path.Combine(Path.GetTempPath(), $"test_dir_{Guid.NewGuid()}", "nested", "planners.json");

        try
        {
            // Act
            PlannerRepository repoWithNestedPath = new(nestedPath);
            repoWithNestedPath.CreatePlanner(CreateEmptyPlannerForUser(1));

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
        string dataPath = Path.Combine(tempDir, "planners.json");

        try
        {
            // Act
            PlannerRepository repoWithExistingDir = new(dataPath);
            repoWithExistingDir.CreatePlanner(CreateEmptyPlannerForUser(1));

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