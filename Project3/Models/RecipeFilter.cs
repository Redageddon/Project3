using API.DataModels.Food;
using Project3.Services;

namespace Project3.Models;

public record RecipeFilter(
    string? Name,
    List<string>? MealType,
    List<string>? Tag,
    List<string>? Ingredients,
    List<string>? Difficulty,
    List<string>? Cuisine,
    int? ServingsLower,
    int? ServingsUpper,
    int? ReviewCountLower,
    int? ReviewCountUpper,
    double? RatingLower,
    double? RatingUpper,
    double? PrepTimeMinutesLower,
    double? PrepTimeMinutesUpper,
    double? CookTimeMinutesLower,
    double? CookTimeMinutesUpper,
    double? CaloriesLower,
    double? CaloriesUpper)
{
    public List<RecipeModel> Filter(IEnumerable<RecipeModel> recipes)
    {
        return recipes
               .Where(r => r.Name.MatchesText(this.Name))
               .Where(r => r.MealType.MatchesAny(this.MealType))
               .Where(r => r.Tags.MatchesAll(this.Tag))
               .Where(r => r.Ingredients.ContainsSubstring(this.Ingredients))
               .Where(r => r.Difficulty.MatchesList(this.Difficulty))
               .Where(r => r.Cuisine.MatchesList(this.Cuisine))
               .Where(r => r.Servings.MatchesRange(this.ServingsLower, this.ServingsUpper))
               .Where(r => r.Rating.MatchesRange(this.RatingLower, this.RatingUpper))
               .Where(r => r.ReviewCount.MatchesRange(this.ReviewCountLower, this.ReviewCountUpper))
               .Where(r => r.PrepTimeMinutes.MatchesRange(this.PrepTimeMinutesLower, this.PrepTimeMinutesUpper))
               .Where(r => r.CookTimeMinutes.MatchesRange(this.CookTimeMinutesLower, this.CookTimeMinutesUpper))
               .Where(r => r.CaloriesPerServing.MatchesRange(this.CaloriesLower, this.CaloriesUpper))
               .ToList();
    }
}
