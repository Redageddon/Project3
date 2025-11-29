using System.ComponentModel.DataAnnotations;
using API.DataModels.Food;

namespace Project3.Models;

public class MealCreateViewModel
{
    [Required]
    [MinLength(1)]
    public string Name { get; set; } = string.Empty;

    // Up to 2 Dishes
    public int? Dish1Id { get; set; }
    
    public int? Dish2Id { get; set; }

    // Up to 2 Drinks
    public int? Drink1Id { get; set; }
    
    public int? Drink2Id { get; set; }

    // Up to 2 Desserts
    public int? Dessert1Id { get; set; }
    
    public int? Dessert2Id { get; set; }

    // Recipes for populating the selects
    public List<RecipeModel> AllRecipes { get; set; } = new();
}