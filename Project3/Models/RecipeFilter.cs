namespace Project3.Models;

public record RecipeFilter(
    string? Name,
    List<string>? MealType,
    List<string>? Tag,
    List<string>? Ingredients,
    List<string>? Difficulty,
    List<string>? Cuisine,
    int? ServingsLower,
    int? ServingsUpper,
    int? ReviewCountLower,
    int? ReviewCountUpper,
    double? RatingLower,
    double? RatingUpper,
    double? PrepTimeMinutesLower,
    double? PrepTimeMinutesUpper,
    double? CookTimeMinutesLower,
    double? CookTimeMinutesUpper,
    double? CaloriesLower,
    double? CaloriesUpper);
