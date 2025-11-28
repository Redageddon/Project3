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
        return filterList is not { Count: > 0 } || 
               filterList.Contains(value, StringComparer.OrdinalIgnoreCase);
    }

    public static bool MatchesAny(this IEnumerable<string> values, List<string>? filterList)
    {
        return filterList is not { Count: > 0 } || 
               values.Any(v => filterList.Contains(v, StringComparer.OrdinalIgnoreCase));
    }
    
    public static bool MatchesAll(this IEnumerable<string> source, List<string>? filterList)
    {
        return filterList is not { Count: > 0 } || 
               filterList.All(tag => source.Contains(tag, StringComparer.OrdinalIgnoreCase));
    }

    public static bool MatchesRange<T>(this T value, T? lower, T? upper)
        where T : struct, IComparable<T>
    {
        return (!lower.HasValue || value.CompareTo(lower.Value) >= 0)
            && (!upper.HasValue || value.CompareTo(upper.Value) <= 0);
    }
    
    public static bool ContainsSubstring(this IEnumerable<string> source, List<string>? filterList)
    {
        return filterList is not { Count: > 0 }
            || source.Any(i => filterList.Any(f => i.Contains(f, StringComparison.OrdinalIgnoreCase)));
    }
}