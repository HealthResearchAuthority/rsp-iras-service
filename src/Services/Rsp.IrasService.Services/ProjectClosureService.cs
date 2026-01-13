using Mapster;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

public class ProjectClosureService(IProjectClosureRepository projectClosureRepository) : IProjectClosureService
{
    public async Task<ProjectClosureResponse> CreateProjectClosure(ProjectClosureRequest projectClosureRequest)
    {
        var projectClosure = projectClosureRequest.Adapt<ProjectClosure>();

        var createdProjectClosure = await projectClosureRepository.CreateProjectClosure(projectClosure);

        return createdProjectClosure.Adapt<ProjectClosureResponse>();
    }

    public async Task UpdateProjectClosureStatus(string projectRecordId, string status, string userId)
    {
        var specification = new GetProjectClosureSpecification(projectRecordId);

        await projectClosureRepository.UpdateProjectClosureStatus(specification, status, userId);
    }

    public async Task<ProjectClosuresSearchResponse> GetProjectClosuresByProjectRecordId(string projectRecordId)
    {
        var projectClosures = await projectClosureRepository.GetProjectClosures(projectRecordId);
        return new ProjectClosuresSearchResponse
        {
            ProjectClosures = projectClosures.Select(pc => pc.Adapt<ProjectClosureResponse>()),
            TotalCount = projectClosures.Count()
        };
    }

    public Task<ProjectClosuresSearchResponse> GetProjectClosuresBySponsorOrganisationUserId
    (
        Guid sponsorOrganisationUserId,
        ProjectClosuresSearchRequest searchQuery,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        var projectClosures = projectClosureRepository.GetProjectClosuresBySponsorOrganisationUser(searchQuery, pageNumber, pageSize, sortField, sortDirection, sponsorOrganisationUserId);
        var totalCount = projectClosureRepository.GetProjectClosuresBySponsorOrganisationUserCount(searchQuery, sponsorOrganisationUserId);

        return Task.FromResult(new ProjectClosuresSearchResponse
        {
            ProjectClosures = projectClosures.Adapt<IEnumerable<ProjectClosureResponse>>(),
            TotalCount = totalCount
        });
    }
}