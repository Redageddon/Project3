using API.DataModels.Food;
using Newtonsoft.Json;

namespace API.Services;

public class MealsRepository
{
    private const string DataPath = "Data/meals.json";
    private readonly Lock mealsLock = new();

    public MealsDataModel GetAllMeals()
    {
        lock (this.mealsLock)
        {
            MealsDataModel emptyModel = new([]);

            if (!File.Exists(DataPath))
            {
                this.SaveMeals(emptyModel);

                return emptyModel;
            }

            string data = File.ReadAllText(DataPath);

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

            MealsModel mealToUpdate = updatedMeal with { MealId = id };

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

    private void SaveMeals(MealsDataModel meals)
    {
        string json = JsonConvert.SerializeObject(meals, Formatting.Indented);
        File.WriteAllText(DataPath, json);
    }
}