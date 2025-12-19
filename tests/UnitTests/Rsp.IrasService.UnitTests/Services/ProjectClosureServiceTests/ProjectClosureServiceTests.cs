using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;
using Rsp.IrasService.UnitTests.Fixtures;

namespace Rsp.IrasService.UnitTests.Services.ProjectClosureServiceTests;

public class ProjectClosureServiceTests : TestServiceBase<ProjectClosureService>
{
    private readonly ProjectClosureRepository _projectClosureRepository;
    private readonly IrasContext _context;

    public ProjectClosureServiceTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _projectClosureRepository = new ProjectClosureRepository(_context);
    }

    [Theory, AutoData]
    public async Task Returns_CreateProjectClosure_Response(ProjectClosureRequest createProjectClosureRequest)
    {
        // Arrange
        Mocker.Use<IProjectClosureRepository>(_projectClosureRepository);

        Sut = Mocker.CreateInstance<ProjectClosureService>();

        // Act
        var irasApplication = await Sut.CreateProjectClosure(createProjectClosureRequest);

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<ProjectClosureResponse>();
        (await _context.ProjectClosures.CountAsync()).ShouldBe(1);
    }

    [Theory]
    [NoRecursionInlineAutoData(5)]
    public async Task Returns_ProjectClosure_By_ProjectRecordId(int records, Generator<ProjectClosure> generator)
    {
        // Arrange
        Mocker.Use<IProjectClosureRepository>(_projectClosureRepository);

        Sut = Mocker.CreateInstance<ProjectClosureService>();

        // seed data using number of records to seed
        var projectClosures = await TestData.SeedData(_context, generator, records);

        // get the random project recordId id between 0 and 4
        var projectRecordId = projectClosures[Random.Shared.Next(0, 4)].ProjectRecordId;

        // Act
        var projectClosure = await Sut.GetProjectClosure(projectRecordId);

        // Assert
        projectClosure.ShouldNotBeNull();
        projectClosure.ShouldBeOfType<ProjectClosureResponse>();
        projectClosure.Id.ShouldBe(projectClosure.Id);
        projectClosure.ProjectRecordId.ShouldBe(projectClosure.ProjectRecordId);
        projectClosure.Status.ShouldBe(projectClosure.Status);
    }
}