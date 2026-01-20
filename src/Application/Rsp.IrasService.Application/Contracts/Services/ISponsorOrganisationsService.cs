using Rsp.Logging.Interceptors;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.Application.Contracts.Services;

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

    Task<SponsorOrganisationUserDto?> AddUserToSponsorOrganisation(SponsorOrganisationUserDto sponsorOrganisationUserDto);

    Task<SponsorOrganisationUserDto?> GetUserInSponsorOrganisation(
        string rtsId, Guid userId);

    Task<SponsorOrganisationUserDto?> EnableUserInSponsorOrganisation(
        string rtsId, Guid userId);

    Task<SponsorOrganisationUserDto?> DisableUserInSponsorOrganisation(
        string rtsId, Guid userId);

    Task<SponsorOrganisationDto?> EnableSponsorOrganisation(
        string rtsId);

    Task<SponsorOrganisationDto?> DisableSponsorOrganisation(
        string rtsId);

    Task<SponsorOrganisationAuditTrailResponse> GetAuditTrailForSponsorOrganisation(string rtsId);

    Task<IEnumerable<SponsorOrganisationDto>> GetSponsorOrganisationsForUser(Guid userId);

    Task<SponsorOrganisationUserDto?> UpdateUserInSponsorOrganisation(SponsorOrganisationUserDto sponsorOrganisationUserDto);

    Task<SponsorOrganisationUserDto> GetSponsorOrganisationUserById(Guid sponsorOrganisationUserId);
}