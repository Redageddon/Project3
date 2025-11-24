using API.DataModels.Food;
using API.Services;

namespace Tests.ApiTests.Repositories;

[TestFixture]
public class RecipeRepositoryTests
{
    private RecipeRepository repository = null!;

    [SetUp]
    public void SetUp()
    {
        this.repository = new RecipeRepository();
    }

    [Test]
    public void GetAllRecipes_ReturnsNonNull()
    {
        MealsModel result = this.repository.GetAllRecipes();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Recipes, Is.Not.Null);
    }

    [Test]
    public void GetAllRecipes_AfterCreate_ContainsNewRecipe()
    {
        RecipeModel newRecipe = new(0, 1, "Test Recipe", "Easy", "Italian", ["ingredient"], ["step"], [], [], "", 10, 20, 2, 300, 0, 0);
        RecipeModel created = this.repository.CreateRecipe(newRecipe);

        MealsModel result = this.repository.GetAllRecipes();

        Assert.That(result.Recipes.Any(r => r.Id == created.Id), Is.True);
    }

    [Test]
    public void GetAllRecipes_ReturnsList()
    {
        MealsModel result = this.repository.GetAllRecipes();

        Assert.That(result.Recipes, Is.InstanceOf<List<RecipeModel>>());
    }

    [Test]
    public void GetAllRecipes_MultipleCalls_ReturnsConsistentData()
    {
        MealsModel result1 = this.repository.GetAllRecipes();
        MealsModel result2 = this.repository.GetAllRecipes();

        Assert.That(result1.Recipes.Count, Is.EqualTo(result2.Recipes.Count));
    }

    [Test]
    public void GetAllRecipes_AfterDelete_DoesNotContainDeletedRecipe()
    {
        RecipeModel newRecipe = new(0, 1, "To Delete", "Easy", "Italian", ["ingredient"], ["step"], [], [], "", 10, 20, 2, 300, 0, 0);
        RecipeModel created = this.repository.CreateRecipe(newRecipe);

        this.repository.DeleteRecipe(created.Id);
        MealsModel result = this.repository.GetAllRecipes();

        Assert.That(result.Recipes.Any(r => r.Id == created.Id), Is.False);
    }

    [Test]
    public void GetAllRecipes_AfterUpdate_ContainsUpdatedRecipe()
    {
        RecipeModel newRecipe = new(0, 1, "Original", "Easy", "Italian", ["ingredient"], ["step"], [], [], "", 10, 20, 2, 300, 0, 0);
        RecipeModel created = this.repository.CreateRecipe(newRecipe);

        RecipeModel updated = created with { Name = "Updated" };
        this.repository.UpdateRecipe(created.Id, updated);

        MealsModel result = this.repository.GetAllRecipes();
        RecipeModel? foundRecipe = result.Recipes.FirstOrDefault(r => r.Id == created.Id);

        Assert.That(foundRecipe, Is.Not.Null);
        Assert.That(foundRecipe!.Name, Is.EqualTo("Updated"));
    }

    [Test]
    public void GetAllRecipes_ReturnsAllRecipes()
    {
        int initialCount = this.repository.GetAllRecipes().Recipes.Count;

        RecipeModel recipe1 = new(0, 1, "Recipe 1", "Easy", "Italian", ["ingredient"], ["step"], [], [], "", 10, 20, 2, 300, 0, 0);
        RecipeModel recipe2 = new(0, 1, "Recipe 2", "Medium", "French", ["ingredient"], ["step"], [], [], "", 15, 25, 3, 400, 0, 0);

        this.repository.CreateRecipe(recipe1);
        this.repository.CreateRecipe(recipe2);

        MealsModel result = this.repository.GetAllRecipes();

        Assert.That(result.Recipes.Count, Is.EqualTo(initialCount + 2));
    }
}
