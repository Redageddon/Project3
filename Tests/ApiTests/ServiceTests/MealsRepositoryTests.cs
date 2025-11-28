using API.DataModels.Food;
using API.Services;

namespace Tests.ApiTests.ServiceTests;

[TestFixture]
public class MealsRepositoryTests
{
    private MealsRepository repository = null!;
    private const string TestDataPath = "Data/meals.json";

    [SetUp]
    public void Setup()
    {
        if (File.Exists(TestDataPath))
        {
            File.Delete(TestDataPath);
        }

        this.repository = new MealsRepository();
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(TestDataPath))
        {
            File.Delete(TestDataPath);
        }
    }

    private static MealsModel CreateTestMeal(string name = "Test Meal")
    {
        return new MealsModel(0, 1, name, [], [], []);
    }

    [Test]
    public void GetAllMeals_ReturnsNonNull()
    {
        MealsDataModel result = this.repository.GetAllMeals();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Meals, Is.Not.Null);
    }

    [Test]
    public void GetAllMeals_AfterCreate_ContainsNewMeal()
    {
        MealsModel newMeal = CreateTestMeal();
        MealsModel created = this.repository.CreateMeal(newMeal);

        MealsDataModel result = this.repository.GetAllMeals();

        Assert.That(result.Meals.Any(m => m.MealId == created.MealId), Is.True);
    }

    [Test]
    public void GetMealById_ExistingMeal_ReturnsMeal()
    {
        MealsModel meal = CreateTestMeal("Breakfast");
        MealsModel created = this.repository.CreateMeal(meal);

        MealsModel? result = this.repository.GetMealById(created.MealId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.MealId, Is.EqualTo(created.MealId));
        Assert.That(result.Name, Is.EqualTo("Breakfast"));
    }

    [Test]
    public void GetMealById_NonExistentMeal_ReturnsNull()
    {
        MealsModel? result = this.repository.GetMealById(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetMealsByIds_ReturnsMatchingMeals()
    {
        MealsModel meal1 = this.repository.CreateMeal(CreateTestMeal("Meal 1"));
        MealsModel meal2 = this.repository.CreateMeal(CreateTestMeal("Meal 2"));
        MealsModel meal3 = this.repository.CreateMeal(CreateTestMeal("Meal 3"));

        List<MealsModel> result = this.repository.GetMealsByIds([meal1.MealId, meal3.MealId]);

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.Any(m => m.MealId == meal1.MealId), Is.True);
        Assert.That(result.Any(m => m.MealId == meal3.MealId), Is.True);
        Assert.That(result.Any(m => m.MealId == meal2.MealId), Is.False);
    }

    [Test]
    public void CreateMeal_AssignsUniqueId()
    {
        MealsModel meal = CreateTestMeal();

        MealsModel created = this.repository.CreateMeal(meal);

        Assert.That(created.MealId, Is.GreaterThan(0));
    }

    [Test]
    public void UpdateMeal_NonExistentMeal_ReturnsNull()
    {
        MealsModel meal = CreateTestMeal("New Meal");

        MealsModel? result = this.repository.UpdateMeal(999, meal);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void UpdateMeal_ExistingMeal_UpdatesSuccessfully()
    {
        MealsModel meal = CreateTestMeal("Original");
        MealsModel created = this.repository.CreateMeal(meal);
        MealsModel updated = created with { Name = "Updated" };

        MealsModel? result = this.repository.UpdateMeal(created.MealId, updated);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("Updated"));
        Assert.That(result.MealId, Is.EqualTo(created.MealId));
    }

    [Test]
    public void DeleteMeal_NonExistentMeal_ReturnsFalse()
    {
        bool result = this.repository.DeleteMeal(999);

        Assert.That(result, Is.False);
    }

    [Test]
    public void DeleteMeal_ExistingMeal_ReturnsTrue()
    {
        MealsModel meal = CreateTestMeal();
        MealsModel created = this.repository.CreateMeal(meal);

        bool result = this.repository.DeleteMeal(created.MealId);

        Assert.That(result, Is.True);
        Assert.That(this.repository.GetMealById(created.MealId), Is.Null);
    }

    [Test]
    public void GetAllMeals_AfterDelete_DoesNotContainDeletedMeal()
    {
        MealsModel meal = CreateTestMeal("To Delete");
        MealsModel created = this.repository.CreateMeal(meal);

        this.repository.DeleteMeal(created.MealId);
        MealsDataModel result = this.repository.GetAllMeals();

        Assert.That(result.Meals.Any(m => m.MealId == created.MealId), Is.False);
    }
}
