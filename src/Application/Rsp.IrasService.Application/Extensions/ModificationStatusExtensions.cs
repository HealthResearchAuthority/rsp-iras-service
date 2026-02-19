using Rsp.Service.Application.Constants;

namespace Rsp.Service.Application.Extensions;

public static class ModificationStatusExtensions
{
    public static string ToBackstageDisplayStatus(this string? status, string? reviewerName)
    {
        // If status is null/empty → fallback
        if (string.IsNullOrWhiteSpace(status))
        {
            return string.Empty;
        }

        // Only care about WithReviewBody — everything else returns unchanged
        if (!status.Equals(ModificationStatus.WithReviewBody, StringComparison.OrdinalIgnoreCase))
        {
            return status;
        }

        // With reviewer name → "Review in progress"
        if (!string.IsNullOrWhiteSpace(reviewerName))
        {
            return ModificationStatus.ReviewInProgress;
        }

        // Without reviewer name → "Received"
        return ModificationStatus.Received;
    }

    public static string ToUiRevisionStatus(this string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return string.Empty;
        }

        if (status.Equals(ModificationStatus.RequestRevisions, StringComparison.OrdinalIgnoreCase))
        {
            return ModificationStatus.InDraft;
        }

        if (status.Equals(ModificationStatus.ReviseAndAuthorise, StringComparison.OrdinalIgnoreCase))
        {
            return ModificationStatus.WithSponsor;
        }

        return status;
    }
}