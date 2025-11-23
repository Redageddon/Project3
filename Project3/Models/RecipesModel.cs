namespace Project3.Models;

public record RecipeModel(
    int Id,
    string Name,
    List<string> Ingredients,
    List<string> Instructions,
    double PrepTimeMinutes,
    double CookTimeMinutes,
    double Servings,
    string Difficulty,
    string Cuisine,
    double CaloriesPerServing,
    List<string> Tags,
    int UserId,
    string Image,
    double Rating,
    int ReviewCount,
    List<string> MealType
);