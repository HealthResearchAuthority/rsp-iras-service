using System.Diagnostics.CodeAnalysis;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Helpers;

[ExcludeFromCodeCoverage]
public static class ChiefInvestigatorSearchExtensions
{
    public static bool MatchesChiefInvestigator(this ProjectModificationResult x, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return true;
        }

        var s = search.ToLower();

        return
            x.ChiefInvestigatorFirstName.ToLower().Contains(s) ||
            x.ChiefInvestigatorLastName.ToLower().Contains(s) ||
            (x.ChiefInvestigatorFirstName + " " + x.ChiefInvestigatorLastName).ToLower().Contains(s);
    }
}