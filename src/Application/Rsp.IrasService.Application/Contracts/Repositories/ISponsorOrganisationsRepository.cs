using Ardalis.Specification;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Contracts.Repositories;

public interface ISponsorOrganisationsRepository
{
    Task<IEnumerable<SponsorOrganisation>> GetSponsorOrganisations(ISpecification<SponsorOrganisation> specification);

    Task<int> GetSponsorOrganisationCount(SponsorOrganisationSearchRequest searchQuery);

    Task<SponsorOrganisation> CreateSponsorOrganisation(SponsorOrganisation sponsorOrganisation);

    Task<SponsorOrganisationUser> AddUserToSponsorOrganisation(SponsorOrganisationUser user);

    Task<SponsorOrganisationUser?> GetUserInSponsorOrganisation(string rtsId, Guid userId);

    Task<SponsorOrganisationUser> DisableUserInSponsorOrganisation(string rtsId, Guid userId);

    Task<SponsorOrganisationUser> EnableUserInSponsorOrganisation(string rtsId, Guid userId);

    Task<SponsorOrganisation> DisableSponsorOrganisation(string rtsId);

    Task<SponsorOrganisation> EnableSponsorOrganisation(string rtsId);

    Task<IEnumerable<SponsorOrganisationAuditTrail>> GetAuditsForSponsorOrganisation(string rtsId);

    Task<SponsorOrganisationUser> UpdateUserInSponsorOrganisation(SponsorOrganisationUser user);

    Task<SponsorOrganisationUser> GetSponsorOrganisationUserById(Guid sponsorOrganisationUserId, string email);
}