namespace API.DataModels.Food;

public record PlannerModel(
    int PlannerId,
    int UserId,
    DateTime PlannedDate,
    int? BreakfastId,
    int? LunchId,
    int? DinnerId
);

public record PlannersDataModel(List<PlannerModel> Planners);