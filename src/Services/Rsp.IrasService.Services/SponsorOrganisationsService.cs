using System.Security.Claims;
using Ardalis.Specification;
using Mapster;
using Microsoft.AspNetCore.Http;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

public class SponsorOrganisationsService(ISponsorOrganisationsRepository sponsorOrganisationsRepository,
    IHttpContextAccessor httpContextAccessor)
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

    public async Task<SponsorOrganisationAuditTrailResponse> GetAuditTrailForSponsorOrganisation(string rtsId)
    {
        var result = new SponsorOrganisationAuditTrailResponse();

        var response = await sponsorOrganisationsRepository.GetAuditsForSponsorOrganisation(rtsId);
        var sponsorOrganisationAuditTrailDtos = response.Adapt<List<SponsorOrganisationAuditTrailDto>>();

        result.Items = sponsorOrganisationAuditTrailDtos;
        result.TotalCount = sponsorOrganisationAuditTrailDtos.Count;

        return result;
    }

    public async Task<IEnumerable<SponsorOrganisationDto>> GetSponsorOrganisationsForUser(Guid userId)
    {
        var spec = new GetActiveSponsorOrganisationsForEnabledUserSpecification(userId);
        var entities = await sponsorOrganisationsRepository.GetSponsorOrganisations(spec);
        return entities.Select(e => e.Adapt<SponsorOrganisationDto>());
    }

    public async Task<SponsorOrganisationUserDto?> UpdateUserInSponsorOrganisation(SponsorOrganisationUserDto sponsorOrganisationUserDto)
    {
        var userProfileEntity = sponsorOrganisationUserDto.Adapt<SponsorOrganisationUser>();
        var response = await sponsorOrganisationsRepository.UpdateUserInSponsorOrganisation(userProfileEntity);

        return response.Adapt<SponsorOrganisationUserDto?>();
    }

    public async Task<SponsorOrganisationUserDto> GetSponsorOrganisationUserById(Guid sponsorOrganisationUserId)
    {
        var email = httpContextAccessor.HttpContext!.User.Claims!.First(c => c.Type == ClaimTypes.Email).Value;

        var response = await sponsorOrganisationsRepository.GetSponsorOrganisationUserById(sponsorOrganisationUserId, email);
        return response.Adapt<SponsorOrganisationUserDto>();
    }
}