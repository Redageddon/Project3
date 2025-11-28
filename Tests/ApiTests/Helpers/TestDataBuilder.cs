
using API.DataModels.Food;
using API.DataModels.Users;

namespace Tests.ApiTests.Helpers;

public static class TestDataBuilder
{
    public static RecipeModel CreateRecipe(string? name = null, string? cuisine = null)
    {
        return new RecipeModel(0,
                               1,
                               name ?? $"Recipe_{Guid.NewGuid():N}",
                               "Medium",
                               cuisine ?? "Italian",
                               ["Ingredient 1", "Ingredient 2"],
                               ["Step 1", "Step 2"],
                               ["dinner"],
                               ["Dinner"],
                               "https://example.com/recipe.jpg",
                               30,
                               45,
                               4,
                               450,
                               10,
                               4.5);
    }

    public static RecipeModel CreateInvalidRecipe()
    {
        return new RecipeModel(0, 0, "", "", "", [], [], [], [], "", 0, 0, 0, 0, 0, 0);
    }

    public static MealsModel CreateMeal(string? name = null)
    {
        return new MealsModel(0, 1, name ?? $"Meal_{Guid.NewGuid():N}", [], [], []);
    }

    public static MealsModel CreateInvalidMeal()
    {
        return new MealsModel(0, 0, null, null, null, null);
    }

    public static PlannerModel CreatePlanner()
    {
        return new PlannerModel(0, 1, DateTime.UtcNow.AddDays(1), null, null, null);
    }

    public static PlannerModel CreateInvalidPlanner()
    {
        return new PlannerModel(0, 0, default, null, null, null);
    }

    public static RegisterRequest CreateRegisterRequest(string? username = null, string? email = null, string? password = null)
    {
        return new RegisterRequest(username ?? $"user_{Guid.NewGuid():N}",
                                   email ?? $"email_{Guid.NewGuid():N}@example.com",
                                   password ?? "Password123!");
    }

    public static LoginRequest CreateLoginRequest(string email, string password)
    {
        return new LoginRequest(email, password);
    }
}
