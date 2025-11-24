namespace API.DataModels.Food;

// /api/planner post
public record PlannerModel(
    List<RecipeModel> Breakfast,
    List<RecipeModel> Lunch,
    List<RecipeModel> Dinner,
    List<RecipeModel> Desert);