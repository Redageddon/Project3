using System.ComponentModel.DataAnnotations;

namespace API.DataModels.Food;

public record MealsModel(
    int MealId,
    [Required][Range(1, int.MaxValue)] int UserId,
    string? Name,
    List<RecipeModel>? Dishes,
    List<RecipeModel>? Drinks,
    List<RecipeModel>? Desserts
);

public record MealsDataModel(List<MealsModel> Meals);