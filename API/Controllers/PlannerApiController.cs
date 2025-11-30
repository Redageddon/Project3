using API.DataModels.Food;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/planners")]
public class PlannerApiController(PlannerRepository repository, SessionService sessionService) : ControllerBase
{
    // GET: api/planners/user/{userId}
    [HttpGet("user/{userId:int}")]
    public ActionResult<PlannersDataModel> GetByUserId(int userId)
    {
        PlannersDataModel planners = repository.GetPlannersByUserId(userId);

        return this.Ok(planners);
    }

    // GET: api/planners/{id}
    [HttpGet("{plannerId:int}")]
    public ActionResult<PlannerModel> GetById(int plannerId)
    {
        PlannerModel? planner = repository.GetPlannerById(plannerId);

        if (planner == null)
        {
            return this.NotFound(new { message = $"Planner with ID {plannerId} not found" });
        }

        return this.Ok(planner);
    }

    // POST: api/planners
    [HttpPost]
    public ActionResult<PlannerModel> Create([FromHeader(Name = "X-Session-Id")] string? sessionId,
                                             [FromBody] PlannerModel planner)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            return this.Unauthorized(new { message = "Session is null or empty" });
        }

        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        int? userId = sessionService.GetUserId(sessionId);

        if (userId is null)
        {
            return this.Unauthorized(new { message = "Invalid or expired session" });
        }

        PlannerModel plannerWithUser = planner with
        {
            UserId = userId.Value,
        };

        PlannerModel createdPlanner = repository.CreatePlanner(plannerWithUser);

        return this.CreatedAtAction(nameof(this.GetById),
                                    new { plannerId = createdPlanner.PlannerId },
                                    createdPlanner);
    }

    // PUT: api/planners/{id}
    [HttpPut("{plannerId:int}")]
    public ActionResult<PlannerModel> Update([FromHeader(Name = "X-Session-Id")] string? sessionId,
                                             int plannerId, 
                                             [FromBody] PlannerModel planner)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            return this.Unauthorized(new { message = "Session is required" });
        }
        
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }
        
        int? userId = sessionService.GetUserId(sessionId);

        if (userId is null)
        {
            return this.Unauthorized(new { message = "Invalid or expired session" });
        }

        PlannerModel? existing = repository.GetPlannerById(plannerId);
        
        if (existing == null)
        {
            return this.NotFound(new { message = $"Planner with ID {plannerId} not found" });
        }
        
        if (existing.UserId != userId.Value)
        {
            return this.Forbid();
        }

        PlannerModel? updatedPlanner = repository.UpdatePlanner(plannerId, planner);

        return this.Ok(updatedPlanner);
    }

    // DELETE: api/planners/{id}
    [HttpDelete("{plannerId:int}")]
    public ActionResult Delete([FromHeader(Name = "X-Session-Id")] string? sessionId,
                               int plannerId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            return this.Unauthorized(new { message = "Session is required" });
        }

        int? userId = sessionService.GetUserId(sessionId);

        if (userId is null)
        {
            return this.Unauthorized(new { message = "Invalid or expired session" });
        }

        PlannerModel? existing = repository.GetPlannerById(plannerId);

        if (existing == null)
        {
            return this.NotFound(new { message = $"Planner with ID {plannerId} not found" });
        }

        if (existing.UserId != userId.Value)
        {
            return this.Forbid();
        }

        bool _ = repository.DeletePlanner(plannerId);

        return this.NoContent();
    }
}