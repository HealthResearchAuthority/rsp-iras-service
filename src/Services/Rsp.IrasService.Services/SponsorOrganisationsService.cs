using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class SponsorOrganisationsService(ISponsorOrganisationsRepository sponsorOrganisationsRepository)
    : ISponsorOrganisationsService
{
    public async Task<AllSponsorOrganisationsResponse> GetSponsorOrganisations(int pageNumber, int pageSize,
        string sortField, string sortDirection,
        SponsorOrganisationSearchRequest searchQuery)
    {
        var specification =
            new GetSponsorOrganisationsSpecification(pageNumber, pageSize, sortField, sortDirection, searchQuery);

        var rbResponses = await sponsorOrganisationsRepository.GetSponsorOrganisations(specification);
        var rbCount = await sponsorOrganisationsRepository.GetSponsorOrganisationCount(searchQuery);

        var response = new AllSponsorOrganisationsResponse
        {
            SponsorOrganisations = rbResponses.Select(x => x.Adapt<SponsorOrganisationDto>()),
            TotalCount = rbCount
        };

        return response;
    }

    public async Task<SponsorOrganisationDto> CreateSponsorOrganisation(SponsorOrganisationDto sponsorOrganisationDto)
    {
        var sponsorOrganisation = sponsorOrganisationDto.Adapt<SponsorOrganisation>();
        var response = await sponsorOrganisationsRepository.CreateSponsorOrganisation(sponsorOrganisation);
        return response.Adapt<SponsorOrganisationDto>();
    }

    public async Task<SponsorOrganisationUserDto?> AddUserToSponsorOrganisation(
        SponsorOrganisationUserDto sponsorOrganisationUserDto)
    {
        var sponsorOrganisationUserEntity = sponsorOrganisationUserDto.Adapt<SponsorOrganisationUser>();

        var response = await sponsorOrganisationsRepository.AddUserToSponsorOrganisation(sponsorOrganisationUserEntity);
        return response.Adapt<SponsorOrganisationUserDto?>();
    }

    public async Task<SponsorOrganisationUserDto?> GetUserInSponsorOrganisation(string rtsId, Guid userId)
    {
        var response = await sponsorOrganisationsRepository.GetUserInSponsorOrganisation(rtsId, userId);
        return response.Adapt<SponsorOrganisationUserDto?>();
    }

    public async Task<SponsorOrganisationUserDto?> EnableUserInSponsorOrganisation(string rtsId, Guid userId)
    {
        var response = await sponsorOrganisationsRepository.EnableUserInSponsorOrganisation(rtsId, userId);
        return response.Adapt<SponsorOrganisationUserDto?>();
    }

    public async Task<SponsorOrganisationUserDto?> DisableUserInSponsorOrganisation(string rtsId, Guid userId)
    {
        var response = await sponsorOrganisationsRepository.DisableUserInSponsorOrganisation(rtsId, userId);
        return response.Adapt<SponsorOrganisationUserDto?>();
    }

    public async Task<SponsorOrganisationDto?> EnableSponsorOrganisation(string rtsId)
    {
        var response = await sponsorOrganisationsRepository.EnableSponsorOrganisation(rtsId);
        return response.Adapt<SponsorOrganisationDto>();
    }

    public async Task<SponsorOrganisationDto?> DisableSponsorOrganisation(string rtsId)
    {
        var response = await sponsorOrganisationsRepository.DisableSponsorOrganisation(rtsId);
        return response.Adapt<SponsorOrganisationDto>();
    }
}