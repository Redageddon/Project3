using API.DataModels.Food;
using API.Services;

namespace Tests.ApiTests.ServiceTests;

[TestFixture]
public class RecipeRepositoryTests
{
    [SetUp]
    public void Setup()
    {
        this.testDataPath = Path.Combine(Path.GetTempPath(), $"recipes_test_{Guid.NewGuid()}.json");
        this.repository = new RecipeRepository(this.testDataPath);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after tests
        if (File.Exists(this.testDataPath))
        {
            File.Delete(this.testDataPath);
        }
    }

    private RecipeRepository repository = null!;
    private string testDataPath = null!;

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

    [Test]
    public void EnsureDataFileExists_DoesNotOverwriteExistingFile()
    {
        RecipeModel recipe = this.CreateTestRecipe("Pasta");
        RecipeModel created = this.repository.CreateRecipe(recipe);

        // Create a new instance which will call EnsureDataFileExists again
        RecipeRepository newRepo = new(this.testDataPath);

        RecipesDataModel recipes = newRepo.GetAllRecipes();
        Assert.That(recipes.Recipes.Any(r => r.RecipeId == created.RecipeId), Is.True);
    }

    [Test]
    public void EnsureDataFileExists_CreatesDirectory_WhenItDoesNotExist()
    {
        // Arrange
        string nestedPath = Path.Combine(Path.GetTempPath(), $"test_dir_{Guid.NewGuid()}", "nested", "recipes.json");

        try
        {
            // Act
            RecipeRepository repoWithNestedPath = new(nestedPath);
            repoWithNestedPath.CreateRecipe(this.CreateTestRecipe());

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
        string dataPath = Path.Combine(tempDir, "recipes.json");

        try
        {
            // Act
            RecipeRepository repoWithExistingDir = new(dataPath);
            repoWithExistingDir.CreateRecipe(this.CreateTestRecipe());

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