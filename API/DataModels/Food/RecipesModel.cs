using System.ComponentModel.DataAnnotations;

namespace API.DataModels.Food;

public record RecipeModel(
    int RecipeId,
    int UserId,
    [Required][MinLength(1)] string Name,
    [Required][MinLength(1)] string Difficulty,
    [Required][MinLength(1)] string Cuisine,
    [Required][Length(1, int.MaxValue)] List<string> Ingredients,
    [Required][Length(1, int.MaxValue)] List<string> Instructions,
    [Required][Length(1, int.MaxValue)] List<string> Tags,
    [Required][Length(1, 4)] List<string> MealType,
    [Required] string Image,
    [Range(0, double.MaxValue)] double PrepTimeMinutes,
    [Range(0, double.MaxValue)] double CookTimeMinutes,
    [Range(0, double.MaxValue)] double Servings,
    [Range(0, double.MaxValue)] double CaloriesPerServing,
    [Range(0, int.MaxValue)] int ReviewCount,
    [Range(0, 5)] double Rating);

public record RecipesDataModel(List<RecipeModel> Recipes);