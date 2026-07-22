using System.Globalization;

namespace Umbraco.Community.Automate.ClickUp;

/// <summary>
/// Parsing helpers shared across action settings (comma-separated lists, dates) and
/// ClickUp API response mapping (Unix millisecond timestamps).
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// Splits a comma-separated string into a trimmed, non-empty string array, or
    /// <see langword="null"/> if <paramref name="value"/> is null/blank — so callers can
    /// pass the result straight through to a <see cref="System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull"/>
    /// request property and have an unset field omitted rather than sent as an empty array.
    /// </summary>
    public static string[]? SplitCsv(this string? value)
        => string.IsNullOrWhiteSpace(value)
            ? null
            : value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    /// <summary>
    /// Splits a comma-separated string into an integer array (e.g. ClickUp user IDs), dropping
    /// any entries that aren't valid integers. Returns <see langword="null"/> if
    /// <paramref name="value"/> is null/blank or no entries parse successfully.
    /// </summary>
    public static int[]? SplitCsvInts(this string? value)
    {
        var parts = value.SplitCsv();
        if (parts is null)
        {
            return null;
        }

        var result = new List<int>(parts.Length);
        foreach (var part in parts)
        {
            if (int.TryParse(part, out var id))
            {
                result.Add(id);
            }
        }

        return result.Count > 0 ? result.ToArray() : null;
    }

    /// <summary>
    /// Parses an ISO-8601 date/datetime string into a <see cref="DateTimeOffset"/>. Intended for
    /// settings fields that are typed <see langword="string"/> specifically so they can support
    /// <c>${ }</c> bindings (see <c>CreateTaskSettings.DueDate</c>) — bindings are already resolved
    /// to a literal value by the time this runs. Returns <see langword="true"/> with a
    /// <see langword="null"/> result for a null/blank value (i.e. "not set" is not a parse failure).
    /// </summary>
    public static bool TryParseDate(this string? value, out DateTimeOffset? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return true;
        }

        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
        {
            result = parsed;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Parses a Unix millisecond timestamp string, as returned by ClickUp for fields like
    /// <c>date_created</c>/<c>date_updated</c>/<c>due_date</c>. Returns <see langword="null"/>
    /// if <paramref name="value"/> is null/empty or not a valid integer.
    /// </summary>
    public static DateTimeOffset? ParseUnixMilliseconds(this string? value)
        => !string.IsNullOrEmpty(value) && long.TryParse(value, out var ms)
            ? DateTimeOffset.FromUnixTimeMilliseconds(ms)
            : null;
}
