namespace API.DataModels.Food;

public record PlannerModel(
    int PlannerId,
    int UserId,
    DateTime PlannedDate,
    List<RecipeModel> Breakfast,
    List<RecipeModel> Lunch,
    List<RecipeModel> Dinner,
    List<RecipeModel> Desert);

public record PlannersDataModel(List<PlannerModel> Planners);