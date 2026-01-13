using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;
using Rsp.Service.UnitTests.Fixtures;

namespace Rsp.Service.UnitTests.Services.ProjectClosureServiceTests;

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
        var result = await Sut.GetProjectClosuresByProjectRecordId(projectRecordId);

        // Assert
        result.ShouldNotBeNull();
        result.ProjectClosures.ShouldNotBeNull();
    }

    [Theory]
    [NoRecursionInlineAutoData()]
    public async Task GetProjectClosuresBySponsorOrganisationUserId_ShouldReturnMappedResponse(
        Guid sponsorOrganisationUserId,
        ProjectClosuresSearchRequest searchQuery,
        List<ProjectClosure> projectClosures,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection)
    {
        // Arrange
        var mockRepo = new Mock<IProjectClosureRepository>();
        mockRepo.Setup(r => r.GetProjectClosuresBySponsorOrganisationUser(searchQuery, pageNumber, pageSize, sortField, sortDirection, sponsorOrganisationUserId))
                .Returns(projectClosures);

        mockRepo.Setup(r => r.GetProjectClosuresBySponsorOrganisationUserCount(searchQuery, sponsorOrganisationUserId))
                .Returns(projectClosures.Count);

        var service = new ProjectClosureService(mockRepo.Object);

        // Act
        var result = await service.GetProjectClosuresBySponsorOrganisationUserId(sponsorOrganisationUserId, searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.ProjectClosures.ShouldNotBeNull();
        result.ProjectClosures.Count().ShouldBe(projectClosures.Count);
    }

    [Theory, AutoData]
    public async Task Calls_Repository_With_Spec_And_Arguments(
            string projectRecordId,
            string status,
            string userId)
    {
        // Arrange
        var repoMock = new Mock<IProjectClosureRepository>(MockBehavior.Strict);

        repoMock
            .Setup(r => r.UpdateProjectClosureStatus(
                It.Is<GetProjectClosureSpecification>(spec =>
                    spec != null
                ),
                status,
                userId))
            .ReturnsAsync((ProjectClosure?)null) // return value is ignored by the service
            .Verifiable();

        Mocker.Use<IProjectClosureRepository>(repoMock.Object);
        Sut = Mocker.CreateInstance<ProjectClosureService>();

        // Act
        await Sut.UpdateProjectClosureStatus(projectRecordId, status, userId);

        // Assert (no result to assert, we just verify the interaction)
        repoMock.Verify(r => r.UpdateProjectClosureStatus(
            It.Is<GetProjectClosureSpecification>(spec => spec != null),
            status,
            userId),
            Times.Once);
    }
}