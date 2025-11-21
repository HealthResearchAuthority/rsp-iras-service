using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsp.IrasService.Application.Constants;

namespace Rsp.IrasService.Application.Extensions;
public static class ModificationStatusExtensions
{
    public static string ToBackstageDisplayStatus(this string? status, string? reviewerName)
    {
        // If status is null/empty → fallback
        if (string.IsNullOrWhiteSpace(status))
            return string.Empty;

        // Only care about WithReviewBody — everything else returns unchanged
        if (!status.Equals(ModificationStatus.WithReviewBody, StringComparison.OrdinalIgnoreCase))
            return status;

        // With reviewer name → "Review in progress"
        if (!string.IsNullOrWhiteSpace(reviewerName))
            return ModificationStatus.ReviewInProgress;

        // Without reviewer name → "Received"
        return ModificationStatus.Received;
    }
}
