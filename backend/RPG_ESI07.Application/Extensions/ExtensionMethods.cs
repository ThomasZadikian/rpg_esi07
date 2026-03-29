using RPG_ESI07.Application.Responses;
using System.Collections.Generic;

namespace RPG_ESI07.Application.Extensions;

/// <summary>
/// Utility extension methods
/// </summary>
public static class PaginationExtensions
{
    /// <summary>
    /// Paginate an IEnumerable collection
    /// </summary>
    public static PaginatedResponse<T> ToPaginatedResponse<T>(
        this List<T> source,
        int pageNumber,
        int pageSize)
    {
        var totalCount = source.Count;
        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return PaginatedResponse<T>.Create(items, pageNumber, pageSize, totalCount);
    }

    /// <summary>
    /// Check if a collection has next page
    /// </summary>
    public static bool HasNextPage(this PaginatedResponse<object> source)
    {
        return source.PageNumber < source.TotalPages;
    }

    /// <summary>
    /// Check if a collection has previous page
    /// </summary>
    public static bool HasPreviousPage(this PaginatedResponse<object> source)
    {
        return source.PageNumber > 1;
    }
}

public static class StringExtensions
{
    /// <summary>
    /// Check if string is null or empty
    /// </summary>
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Capitalize first letter
    /// </summary>
    public static string Capitalize(this string value)
    {
        if (value.IsNullOrEmpty()) return value;
        return char.ToUpper(value[0]) + value.Substring(1);
    }

    /// <summary>
    /// Convert to Title Case
    /// </summary>
    public static string ToTitleCase(this string value)
    {
        if (value.IsNullOrEmpty()) return value;
        var words = value.Split(' ');
        return string.Join(" ", words.Select(w => w.Capitalize()));
    }
}

public static class CollectionExtensions
{
    /// <summary>
    /// Check if collection is null or empty
    /// </summary>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || !source.Any();
    }

    /// <summary>
    /// Get or default from collection
    /// </summary>
    public static T GetOrDefault<T>(this IEnumerable<T> source, int index, T defaultValue = default)
    {
        var item = source.ElementAtOrDefault(index);
        return item != null ? item : defaultValue;
    }

    /// <summary>
    /// Batch items in groups
    /// </summary>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        if (batchSize <= 0)
            throw new ArgumentException("Batch size must be greater than 0");

        var batch = new List<T>(batchSize);
        foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count >= batchSize)
            {
                yield return batch;
                batch = new List<T>(batchSize);
            }
        }

        if (batch.Count > 0)
            yield return batch;
    }
}

public static class DateTimeExtensions
{
    /// <summary>
    /// Check if date is today
    /// </summary>
    public static bool IsToday(this DateTime dateTime)
    {
        return dateTime.Date == DateTime.Today;
    }

    /// <summary>
    /// Check if date is in the past
    /// </summary>
    public static bool IsInPast(this DateTime dateTime)
    {
        return dateTime < DateTime.UtcNow;
    }

    /// <summary>
    /// Check if date is in the future
    /// </summary>
    public static bool IsInFuture(this DateTime dateTime)
    {
        return dateTime > DateTime.UtcNow;
    }

    /// <summary>
    /// Get days until date
    /// </summary>
    public static int DaysUntil(this DateTime dateTime)
    {
        return (int)(dateTime.Date - DateTime.Today).TotalDays;
    }

    /// <summary>
    /// Format as ISO 8601
    /// </summary>
    public static string ToIso8601String(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}

public static class NumericExtensions
{
    /// <summary>
    /// Check if number is within range
    /// </summary>
    public static bool IsBetween(this int value, int min, int max)
    {
        return value >= min && value <= max;
    }

    /// <summary>
    /// Check if number is within range (decimal)
    /// </summary>
    public static bool IsBetween(this decimal value, decimal min, decimal max)
    {
        return value >= min && value <= max;
    }

    /// <summary>
    /// Clamp value between min and max
    /// </summary>
    public static int Clamp(this int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    /// <summary>
    /// Convert to percentage
    /// </summary>
    public static decimal AsPercentageOf(this int value, int total)
    {
        return total == 0 ? 0 : Math.Round((decimal)value / total * 100, 2);
    }
}

public static class ApiResponseExtensions
{
    /// <summary>
    /// Convert to ApiResponse with data
    /// </summary>
    public static ApiResponse<T> AsSuccess<T>(this T data, string message = "Success")
    {
        return ApiResponse<T>.SuccessResponse(data, message);
    }

    /// <summary>
    /// Create error response
    /// </summary>
    public static ApiResponse<T> AsError<T>(this string error, string message = "Error occurred")
    {
        return ApiResponse<T>.ErrorResponse(message, error);
    }
}
