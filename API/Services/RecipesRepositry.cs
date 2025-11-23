using API.DataModels;
using Newtonsoft.Json;

namespace API.Services;

public class RecipeRepository
{
    private const string DataPath = "Data/recipes.json";
    private readonly Lock recipesLock = new();

    public MealsModel GetAllRecipes()
    {
        lock (this.recipesLock)
        {
            MealsModel emptyModel = new([]);
            
            if (!File.Exists(DataPath))
            {
                this.SaveRecipes(emptyModel);

                return emptyModel;
            }

            string data = File.ReadAllText(DataPath);

            return JsonConvert.DeserializeObject<MealsModel>(data) ?? emptyModel;
        }
    }

    public RecipeModel? GetRecipeById(int id)
    {
        MealsModel meals = this.GetAllRecipes();
        
        return meals.Recipes.FirstOrDefault(r => r.Id == id);
    }

    public RecipeModel CreateRecipe(RecipeModel recipe)
    {
        lock (this.recipesLock)
        {
            MealsModel meals = this.GetAllRecipes();
            
            int newId = meals.Recipes.Count != 0 
                ? meals.Recipes.Max(r => r.Id) + 1 
                : 1;

            RecipeModel newRecipe = recipe with { Id = newId };
            List<RecipeModel> updatedRecipes = [..meals.Recipes, newRecipe];

            this.SaveRecipes(new MealsModel(updatedRecipes));

            return newRecipe;
        }
    }

    public RecipeModel? UpdateRecipe(int id, RecipeModel updatedRecipe)
    {
        lock (this.recipesLock)
        {
            MealsModel meals = this.GetAllRecipes();
            int index = meals.Recipes.FindIndex(r => r.Id == id);

            if (index == -1)
            {
                return null;
            }

            RecipeModel recipeToUpdate = updatedRecipe with { Id = id };
            List<RecipeModel> updatedRecipes = meals.Recipes.ToList();
            updatedRecipes[index] = recipeToUpdate;

            this.SaveRecipes(new MealsModel(updatedRecipes));

            return recipeToUpdate;
        }
    }

    public bool DeleteRecipe(int id)
    {
        lock (this.recipesLock)
        {
            MealsModel meals = this.GetAllRecipes();
            RecipeModel? recipe = meals.Recipes.FirstOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                return false;
            }

            List<RecipeModel> updatedRecipes = meals.Recipes.Where(r => r.Id != id).ToList();
            this.SaveRecipes(new MealsModel(updatedRecipes));

            return true;
        }
    }

    private void SaveRecipes(MealsModel meals)
    {
        string json = JsonConvert.SerializeObject(meals, Formatting.Indented);
        File.WriteAllText(DataPath, json);
    }
}