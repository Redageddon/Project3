using API.DataModels.Food;
using Newtonsoft.Json;

namespace API.Services;

public class RecipeRepository
{
    private const string DataPath = "Data/recipes.json";
    private readonly Lock recipesLock = new();

    public RecipesDataModel GetAllRecipes()
    {
        lock (this.recipesLock)
        {
            RecipesDataModel emptyModel = new([]);
            
            if (!File.Exists(DataPath))
            {
                this.SaveRecipes(emptyModel);

                return emptyModel;
            }

            string data = File.ReadAllText(DataPath);

            return JsonConvert.DeserializeObject<RecipesDataModel>(data) ?? emptyModel;
        }
    }

    public RecipeModel? GetRecipeById(int id)
    {
        RecipesDataModel recipesData = this.GetAllRecipes();
        
        return recipesData.Recipes.FirstOrDefault(r => r.RecipeId == id);
    }

    public RecipeModel CreateRecipe(RecipeModel recipe)
    {
        lock (this.recipesLock)
        {
            RecipesDataModel recipesData = this.GetAllRecipes();
            
            int newId = recipesData.Recipes.Count != 0
                ? recipesData.Recipes.Max(r => r.RecipeId) + 1
                : 1;

            RecipeModel newRecipe = recipe with { RecipeId = newId };
            List<RecipeModel> updatedRecipes = [..recipesData.Recipes, newRecipe];

            this.SaveRecipes(new RecipesDataModel(updatedRecipes));

            return newRecipe;
        }
    }

    public RecipeModel? UpdateRecipe(int id, RecipeModel updatedRecipe)
    {
        lock (this.recipesLock)
        {
            RecipesDataModel recipesData = this.GetAllRecipes();
            int index = recipesData.Recipes.FindIndex(r => r.RecipeId == id);

            if (index == -1)
            {
                return null;
            }

            RecipeModel recipeToUpdate = updatedRecipe with { RecipeId = id };
            List<RecipeModel> updatedRecipes = recipesData.Recipes.ToList();
            updatedRecipes[index] = recipeToUpdate;

            this.SaveRecipes(new RecipesDataModel(updatedRecipes));

            return recipeToUpdate;
        }
    }

    public bool DeleteRecipe(int id)
    {
        lock (this.recipesLock)
        {
            RecipesDataModel recipesData = this.GetAllRecipes();
            RecipeModel? recipe = recipesData.Recipes.FirstOrDefault(r => r.RecipeId == id);

            if (recipe == null)
            {
                return false;
            }

            List<RecipeModel> updatedRecipes = recipesData.Recipes.Where(r => r.RecipeId != id).ToList();
            this.SaveRecipes(new RecipesDataModel(updatedRecipes));

            return true;
        }
    }

    private void SaveRecipes(RecipesDataModel recipesData)
    {
        string json = JsonConvert.SerializeObject(recipesData, Formatting.Indented);
        File.WriteAllText(DataPath, json);
    }
}