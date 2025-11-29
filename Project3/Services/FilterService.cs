using System.Numerics;

namespace Project3.Services;

public static class FilterService
{
    public static bool MatchesText(this string value, string? filter)
    {
        return string.IsNullOrEmpty(filter) || 
               value.Contains(filter, StringComparison.OrdinalIgnoreCase);
    }

    public static bool MatchesList(this string value, List<string>? filterList)
    {
        if (filterList == null || filterList.Count == 0 || string.IsNullOrEmpty(filterList[0]))
        {
            return true;
        }
        
        return filterList.Contains(value, StringComparer.OrdinalIgnoreCase);
    }

    public static bool MatchesAny(this IEnumerable<string> values, List<string>? filterList)
    {
        if (filterList == null || filterList.Count == 0 || string.IsNullOrEmpty(filterList[0]))
        {
            return true;
        }
        
        return values.Any(v => filterList.Contains(v, StringComparer.OrdinalIgnoreCase));
    }
    
    public static bool MatchesAll(this IEnumerable<string> source, List<string>? filterList)
    {
        if (filterList == null || filterList.Count == 0 || string.IsNullOrEmpty(filterList[0]))
        {
            return true;
        }
        
        return filterList.All(tag => source.Contains(tag, StringComparer.OrdinalIgnoreCase));
    }

    public static bool MatchesRange<T>(this T value, T? lower, T? upper)
        where T : struct, IComparable<T>, IMinMaxValue<T>
    {
        if (lower == null && upper == null)
        {
            return true;
        }

        T lowerBound = lower ?? T.MinValue;
        T upperBound = upper ?? T.MaxValue;
        
        return value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) <= 0;
    }
    
    public static bool ContainsSubstring(this IEnumerable<string> source, List<string>? filterList)
    {
        if (filterList == null || filterList.Count == 0 || string.IsNullOrEmpty(filterList[0]))
        {
            return true;
        }
        
        return source.Any(str => filterList.Any(sub => str.Contains(sub, StringComparison.OrdinalIgnoreCase)));
    }
}