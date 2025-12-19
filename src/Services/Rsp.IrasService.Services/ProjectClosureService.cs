using Mapster;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ProjectClosureService(IProjectClosureRepository projectClosureRepository) : IProjectClosureService
{
    public async Task<ProjectClosureResponse> CreateProjectClosure(ProjectClosureRequest projectClosureRequest)
    {
        var projectClosure = projectClosureRequest.Adapt<ProjectClosure>();

        var createdProjectClosure = await projectClosureRepository.CreateProjectClosure(projectClosure);

        return createdProjectClosure.Adapt<ProjectClosureResponse>();
    }

    public async Task<ProjectClosureResponse> GetProjectClosure(string projectRecordId)
    {
        var specification = new GetProjectClosureSpecification(projectRecordId);

        var projectClosureFromDb = await projectClosureRepository.GetProjectClosure(specification);

        return projectClosureFromDb.Adapt<ProjectClosureResponse>();
    }

    public async Task<ProjectClosureResponse> UpdateProjectClosureStatus(ProjectClosureRequest projectClosureRequest)
    {
        var projectClosure = projectClosureRequest.Adapt<ProjectClosure>();

        var updateProjectClosure = await projectClosureRepository.UpdateProjectClosureStatus(projectClosure);

        return updateProjectClosure.Adapt<ProjectClosureResponse>();
    }
}