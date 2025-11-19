using Newtonsoft.Json;
using Project3.Models;

namespace Project3.API;

public static class BasicAPI
{
    private const string Path = "Data/recipes.json";

    public static MealsModel GetRecipes()
    {
        string data = File.ReadAllText(Path);
        MealsModel? recipes = JsonConvert.DeserializeObject<MealsModel>(data);

        return recipes ?? throw new Exception("Something went wrong with deserialization"); // temporary
    }

    public static void SaveRecipes(string data)
    {
        File.WriteAllText(Path, JsonConvert.SerializeObject(data));
    }
}