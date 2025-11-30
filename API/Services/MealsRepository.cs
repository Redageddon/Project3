using API.DataModels.Food;
using Newtonsoft.Json;

namespace API.Services;

public class MealsRepository
{
    private readonly string dataPath;
    private readonly Lock mealsLock = new();

    public MealsRepository(string? dataPath = null)
    {
        this.dataPath = dataPath ?? Path.Combine(AppContext.BaseDirectory, "Data", "meals.json");
        this.EnsureDataFileExists();
    }

    public MealsDataModel GetAllMeals()
    {
        lock (this.mealsLock)
        {
            MealsDataModel emptyModel = new([]);

            if (!File.Exists(this.dataPath))
            {
                this.SaveMeals(emptyModel);

                return emptyModel;
            }

            string data = File.ReadAllText(this.dataPath);

            return JsonConvert.DeserializeObject<MealsDataModel>(data) ?? emptyModel;
        }
    }

    public MealsModel? GetMealById(int id)
    {
        MealsDataModel meals = this.GetAllMeals();

        return meals.Meals.FirstOrDefault(m => m.MealId == id);
    }

    public List<MealsModel> GetMealsByIds(List<int> ids)
    {
        MealsDataModel meals = this.GetAllMeals();

        return meals.Meals.Where(m => ids.Contains(m.MealId)).ToList();
    }

    public MealsModel CreateMeal(MealsModel meal)
    {
        lock (this.mealsLock)
        {
            MealsDataModel meals = this.GetAllMeals();

            int newId = meals.Meals.Count != 0
                ? meals.Meals.Max(m => m.MealId) + 1
                : 1;

            MealsModel newMeal = meal with { MealId = newId };

            List<MealsModel> updatedMeals = [..meals.Meals, newMeal];
            this.SaveMeals(new MealsDataModel(updatedMeals));

            return newMeal;
        }
    }

    public MealsModel? UpdateMeal(int id, MealsModel updatedMeal)
    {
        lock (this.mealsLock)
        {
            MealsDataModel meals = this.GetAllMeals();
            int index = meals.Meals.FindIndex(m => m.MealId == id);

            if (index == -1)
            {
                return null;
            }

            MealsModel original = meals.Meals[index];

            MealsModel mealToUpdate = updatedMeal with 
            { 
                MealId = id,
                UserId = original.UserId,
            };

            List<MealsModel> updatedMealsList = meals.Meals.ToList();
            updatedMealsList[index] = mealToUpdate;

            this.SaveMeals(new MealsDataModel(updatedMealsList));

            return mealToUpdate;
        }
    }

    public bool DeleteMeal(int id)
    {
        lock (this.mealsLock)
        {
            MealsDataModel meals = this.GetAllMeals();
            MealsModel? meal = meals.Meals.FirstOrDefault(m => m.MealId == id);

            if (meal == null)
            {
                return false;
            }

            List<MealsModel> updatedMeals = meals.Meals.Where(m => m.MealId != id).ToList();
            this.SaveMeals(new MealsDataModel(updatedMeals));

            return true;
        }
    }

    private void EnsureDataFileExists()
    {
        string? directory = Path.GetDirectoryName(this.dataPath);

        if (directory != null
         && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private void SaveMeals(MealsDataModel meals)
    {
        this.EnsureDataFileExists();
        string json = JsonConvert.SerializeObject(meals, Formatting.Indented);
        File.WriteAllText(this.dataPath, json);
    }
}