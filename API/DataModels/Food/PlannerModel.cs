using System.ComponentModel.DataAnnotations;

namespace API.DataModels.Food;

public record PlannerModel(
    int PlannerId,
    [Required][Range(1, int.MaxValue)] int UserId,
    [Required] DateTime PlannedDate,
    int? BreakfastId,
    int? LunchId,
    int? DinnerId
);

public record PlannersDataModel(List<PlannerModel> Planners);