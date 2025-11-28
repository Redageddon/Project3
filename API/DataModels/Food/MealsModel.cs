namespace API.DataModels.Food;

public record MealsModel(
    int MealId,
    int? UserId,
    string? Name,
    List<RecipeModel>? Dishes,
    List<RecipeModel>? Drinks,
    List<RecipeModel>? Deserts
);

public record MealsDataModel(List<MealsModel> Meals);