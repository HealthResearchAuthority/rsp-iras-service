using Rsp.Service.Application.CQRS.Queries;

namespace Rsp.Service.Application.Extensions;

public static class AllowedStatusesExtensions
{
    /// <summary>
    /// Checks for the presence of allowed statuses on a <see cref="BaseQuery"/>.
    /// This is a null-safe helper that returns true when the query has one or more allowed statuses.
    /// </summary>
    /// <param name="query">The query to inspect.</param>
    /// <returns>True if <see cref="BaseQuery.AllowedStatuses"/> contains at least one entry; otherwise false.</returns>
    private static bool HasAllowed(this BaseQuery query) => query.AllowedStatuses?.Count > 0;

    /// <summary>
    /// Filters an enumerable source by allowed statuses taken from <paramref name="query"/>.
    /// If the query contains no allowed statuses the original source is returned unchanged.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
    /// <param name="source">Sequence of items to filter.</param>
    /// <param name="query">Query containing <see cref="BaseQuery.AllowedStatuses"/>.</param>
    /// <param name="statusSelector">Function that selects the status string from an item.</param>
    /// <returns>An enumerable of items whose selected status exists in the allowed statuses list (case-insensitive).</returns>
    public static IEnumerable<T> FilterByAllowedStatuses<T>(this IEnumerable<T> source, BaseQuery query, Func<T, string?> statusSelector)
    {
        // If there are no allowed statuses provided, return the original sequence.
        if (!query.HasAllowed())
        {
            return source;
        }

        // Create a case-insensitive set for fast lookups of allowed statuses.
        var set = new HashSet<string>(query.AllowedStatuses, StringComparer.OrdinalIgnoreCase);

        // Filter items whose status (selected by the provided selector) exists in the set.
        return source.Where(x =>
        {
            var status = statusSelector(x);
            return status != null && set.Contains(status);
        });
    }

    /// <summary>
    /// Returns the supplied item when its status is included in the query's allowed statuses; otherwise returns null.
    /// If the query has no allowed statuses the original item is returned unchanged.
    /// </summary>
    /// <typeparam name="T">Type of the item to evaluate.</typeparam>
    /// <param name="item">The item to check.</param>
    /// <param name="query">Query containing allowed statuses.</param>
    /// <param name="statusSelector">Function that selects the status string from the item.</param>
    /// <returns>The original item if its status is allowed; otherwise <c>default</c> (null for reference types).</returns>
    public static T? FilterSingleOrNull<T>(this T item, BaseQuery query, Func<T, string?> statusSelector)
    {
        if (!query.HasAllowed())
        {
            // No allowed statuses configured - preserve the item as-is.
            return item;
        }

        // Determine the status for the single item.
        var status = statusSelector(item);

        // Return the item only when the status is non-null and present in the allowed statuses (case-insensitive).
        return status != null &&
               query.AllowedStatuses.Contains(status, StringComparer.OrdinalIgnoreCase) ? item : default;
    }
}