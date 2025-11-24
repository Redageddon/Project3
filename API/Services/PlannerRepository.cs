using API.DataModels.Food;
using Newtonsoft.Json;

namespace API.Services;

public class PlannerRepository
{
    private const string DataPath = "Data/planners.json";
    private readonly Lock plannersLock = new();

    public PlannersDataModel GetAllPlanners()
    {
        lock (this.plannersLock)
        {
            PlannersDataModel emptyModel = new([]);

            if (!File.Exists(DataPath))
            {
                this.SavePlanners(emptyModel);

                return emptyModel;
            }

            string data = File.ReadAllText(DataPath);

            return JsonConvert.DeserializeObject<PlannersDataModel>(data) ?? emptyModel;
        }
    }

    public List<PlannerModel> GetPlannersByUserId(int userId)
    {
        PlannersDataModel planners = this.GetAllPlanners();

        return planners.Planners.Where(p => p.UserId == userId).ToList();
    }

    public PlannerModel? GetPlannerById(int id)
    {
        PlannersDataModel planners = this.GetAllPlanners();

        return planners.Planners.FirstOrDefault(p => p.Id == id);
    }

    public PlannerModel CreatePlanner(PlannerModel planner)
    {
        lock (this.plannersLock)
        {
            PlannersDataModel planners = this.GetAllPlanners();

            int newId = planners.Planners.Count != 0
                ? planners.Planners.Max(p => p.Id) + 1
                : 1;

            PlannerModel newPlanner = planner with
            {
                Id = newId,
            };

            List<PlannerModel> updatedPlanners = [..planners.Planners, newPlanner];
            this.SavePlanners(new PlannersDataModel(updatedPlanners));

            return newPlanner;
        }
    }

    public PlannerModel? UpdatePlanner(int id, PlannerModel updatedPlanner)
    {
        lock (this.plannersLock)
        {
            PlannersDataModel planners = this.GetAllPlanners();
            int index = planners.Planners.FindIndex(p => p.Id == id);

            if (index == -1)
            {
                return null;
            }

            // Preserve original planned date and user id
            PlannerModel original = planners.Planners[index];

            PlannerModel plannerToUpdate = updatedPlanner with
            {
                Id = id,
                UserId = original.UserId,
            };

            List<PlannerModel> updatedPlannersList = planners.Planners.ToList();
            updatedPlannersList[index] = plannerToUpdate;

            this.SavePlanners(new PlannersDataModel(updatedPlannersList));

            return plannerToUpdate;
        }
    }

    public bool DeletePlanner(int id)
    {
        lock (this.plannersLock)
        {
            PlannersDataModel planners = this.GetAllPlanners();
            PlannerModel? planner = planners.Planners.FirstOrDefault(p => p.Id == id);

            if (planner == null)
            {
                return false;
            }

            List<PlannerModel> updatedPlanners = planners.Planners.Where(p => p.Id != id).ToList();
            this.SavePlanners(new PlannersDataModel(updatedPlanners));

            return true;
        }
    }

    private void SavePlanners(PlannersDataModel planners)
    {
        string json = JsonConvert.SerializeObject(planners, Formatting.Indented);
        File.WriteAllText(DataPath, json);
    }
}