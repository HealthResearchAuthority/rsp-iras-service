using System.Diagnostics.CodeAnalysis;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public static class ChiefInvestigatorSearchExtensions
{
    public static bool MatchesChiefInvestigator(this ProjectModificationResult x, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return true;
        }

        // Trim search just once
        var term = search.Trim();

        // Use OrdinalIgnoreCase for fastest consistent case-insensitive matching
        return
            x.ChiefInvestigatorFirstName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            x.ChiefInvestigatorLastName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            ContainsFullName(x, term);
    }

    private static bool ContainsFullName(ProjectModificationResult x, string term)
    {
        // Manual check without allocating a "First Last" string
        // Check "First Last"
        var fullName = $"{x.ChiefInvestigatorFirstName} {x.ChiefInvestigatorLastName}";
        return fullName.Contains(term, StringComparison.OrdinalIgnoreCase);
    }
}