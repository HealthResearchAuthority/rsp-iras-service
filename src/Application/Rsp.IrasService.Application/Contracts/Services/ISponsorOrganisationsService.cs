using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.Logging.Interceptors;

namespace Rsp.IrasService.Application.Contracts.Services;

/// <summary>
///     Interface to create/read/update the application records in the database. Marked as IInterceptable to enable
///     the start/end logging for all methods.
/// </summary>
public interface ISponsorOrganisationsService : IInterceptable
{
    Task<AllSponsorOrganisationsResponse> GetSponsorOrganisations(int pageNumber, int pageSize, string sortField,
        string sortDirection,
        SponsorOrganisationSearchRequest searchQuery);

    Task<SponsorOrganisationDto> CreateSponsorOrganisation(SponsorOrganisationDto sponsorOrganisationDto);
}