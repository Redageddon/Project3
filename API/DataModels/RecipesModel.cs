using System.ComponentModel.DataAnnotations;

namespace API.DataModels;

public record RecipeModel(
    int Id,
    int UserId,
    [Required][MinLength(1)] string Name,
    [Required][MinLength(1)] string Difficulty,
    [Required][MinLength(1)] string Cuisine,
    [Required] List<string> Ingredients,
    [Required] List<string> Instructions,
    [Required] List<string> Tags,
    [Required] List<string> MealType,
    [Required] string Image,
    [Range(0, double.MaxValue)] double PrepTimeMinutes,
    [Range(0, double.MaxValue)] double CookTimeMinutes,
    [Range(0, double.MaxValue)] double Servings,
    [Range(0, double.MaxValue)] double CaloriesPerServing,
    [Range(0, int.MaxValue)] int ReviewCount,
    [Range(0, 5)] double Rating);