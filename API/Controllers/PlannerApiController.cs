using API.DataModels.Food;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/planners")]
public class PlannerApiController(PlannerRepository repository) : ControllerBase
{
    // GET: api/planners/user/{userId}
    [HttpGet("user/{userId:int}")]
    public ActionResult<List<PlannerModel>> GetByUserId(int userId)
    {
        List<PlannerModel> planners = repository.GetPlannersByUserId(userId);

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
    public ActionResult<PlannerModel> Create([FromBody] PlannerModel planner)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        PlannerModel createdPlanner = repository.CreatePlanner(planner);

        return this.CreatedAtAction(nameof(this.GetById),
                                    new { plannerId = createdPlanner.PlannerId },
                                    createdPlanner);
    }

    // PUT: api/planners/{id}
    [HttpPut("{plannerId:int}")]
    public ActionResult<PlannerModel> Update(int plannerId, [FromBody] PlannerModel planner)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        PlannerModel? updatedPlanner = repository.UpdatePlanner(plannerId, planner);

        if (updatedPlanner == null)
        {
            return this.NotFound(new { message = $"Planner with ID {plannerId} not found" });
        }

        return this.Ok(updatedPlanner);
    }

    // DELETE: api/planners/{id}
    [HttpDelete("{plannerId:int}")]
    public ActionResult Delete(int plannerId)
    {
        bool success = repository.DeletePlanner(plannerId);

        if (!success)
        {
            return this.NotFound(new { message = $"Planner with ID {plannerId} not found" });
        }

        return this.NoContent();
    }
}