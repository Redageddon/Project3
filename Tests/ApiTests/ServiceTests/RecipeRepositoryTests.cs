using API.DataModels.Food;
using API.Services;

namespace Tests.ApiTests.ServiceTests;

[TestFixture]
public class RecipeRepositoryTests
{
    private RecipeRepository repository = null!;
    private const string TestDataPath = "Data/recipes.json";

    [SetUp]
    public void Setup()
    {
        // Clean up test data before each test
        if (File.Exists(TestDataPath))
        {
            File.Delete(TestDataPath);
        }

        this.repository = new RecipeRepository();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after tests
        if (File.Exists(TestDataPath))
        {
            File.Delete(TestDataPath);
        }
    }

    private RecipeModel CreateTestRecipe(string name = "Test Recipe")
    {
        return new RecipeModel(0,
                               1,
                               name,
                               "Medium",
                               "Italian",
                               ["Ingredient 1"],
                               ["Step 1"],
                               ["dinner"],
                               ["Dinner"],
                               "https://example.com/image.jpg",
                               30,
                               45,
                               4,
                               450,
                               10,
                               4.5);
    }

    [Test]
    public void GetRecipeById_ExistingRecipe_ReturnsRecipe()
    {
        RecipeModel recipe = this.CreateTestRecipe("Pasta");
        RecipeModel created = this.repository.CreateRecipe(recipe);

        RecipeModel? result = this.repository.GetRecipeById(created.RecipeId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.RecipeId, Is.EqualTo(created.RecipeId));
        Assert.That(result.Name, Is.EqualTo("Pasta"));
    }

    [Test]
    public void GetRecipeById_NonExistentRecipe_ReturnsNull()
    {
        RecipeModel? result = this.repository.GetRecipeById(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void UpdateRecipe_NonExistentRecipe_ReturnsNull()
    {
        RecipeModel recipe = this.CreateTestRecipe("New Recipe");

        RecipeModel? result = this.repository.UpdateRecipe(999, recipe);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void UpdateRecipe_ExistingRecipe_UpdatesSuccessfully()
    {
        RecipeModel recipe = this.CreateTestRecipe("Original");
        RecipeModel created = this.repository.CreateRecipe(recipe);
        RecipeModel updated = created with { Name = "Updated" };

        RecipeModel? result = this.repository.UpdateRecipe(created.RecipeId, updated);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("Updated"));
        Assert.That(result.RecipeId, Is.EqualTo(created.RecipeId));
    }

    [Test]
    public void DeleteRecipe_NonExistentRecipe_ReturnsFalse()
    {
        bool result = this.repository.DeleteRecipe(999);

        Assert.That(result, Is.False);
    }

    [Test]
    public void DeleteRecipe_ExistingRecipe_ReturnsTrue()
    {
        RecipeModel recipe = this.CreateTestRecipe();
        RecipeModel created = this.repository.CreateRecipe(recipe);

        bool result = this.repository.DeleteRecipe(created.RecipeId);

        Assert.That(result, Is.True);
        Assert.That(this.repository.GetRecipeById(created.RecipeId), Is.Null);
    }
}
