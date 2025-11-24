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
    [HttpGet("{id:int}")]
    public ActionResult<PlannerModel> GetById(int id)
    {
        PlannerModel? planner = repository.GetPlannerById(id);

        if (planner == null)
        {
            return this.NotFound(new { message = $"Planner with ID {id} not found" });
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
                                    new { id = createdPlanner.Id },
                                    createdPlanner);
    }

    // PUT: api/planners/{id}
    [HttpPut("{id:int}")]
    public ActionResult<PlannerModel> Update(int id, [FromBody] PlannerModel planner)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        PlannerModel? updatedPlanner = repository.UpdatePlanner(id, planner);

        if (updatedPlanner == null)
        {
            return this.NotFound(new { message = $"Planner with ID {id} not found" });
        }

        return this.Ok(updatedPlanner);
    }

    // DELETE: api/planners/{id}
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        bool success = repository.DeletePlanner(id);

        if (!success)
        {
            return this.NotFound(new { message = $"Planner with ID {id} not found" });
        }

        return this.NoContent();
    }
}