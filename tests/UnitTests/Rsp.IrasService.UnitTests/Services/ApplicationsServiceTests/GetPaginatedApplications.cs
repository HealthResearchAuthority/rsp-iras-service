using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Settings;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;
using Rsp.Service.UnitTests.Fixtures;

namespace Rsp.Service.UnitTests.Services.ApplicationsServiceTests;

public class GetPaginatedApplications : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly IrasContext _context;
    private readonly AppSettings _appSettings;

    public GetPaginatedApplications()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
        _appSettings = new AppSettings() { QuestionIds = new Dictionary<string, string> { { "ShortProjectTitle", "IQA0002" } } };
    }

    /// <summary>
    /// Tests that GetPaginatedRespondentApplications returns paginated applications for the given respondentId.
    /// </summary>
    [Theory, NoRecursionAutoData]
    public async Task GetPaginatedApplications_ShouldReturnApplications(List<ProjectRecord> projectRecords)
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        ProjectRecordSearchRequest searchQuery = new ProjectRecordSearchRequest();

        await _context.ProjectRecords.AddRangeAsync(projectRecords);
        await _context.SaveChangesAsync();

        // Act
        int pageIndex = 1;
        int pageSize = 10;
        var result = await applicationsService.GetPaginatedApplications(searchQuery, pageIndex, pageSize, null, null);

        // Assert
        result.ShouldNotBeNull();
    }

    /// <summary>
    /// Tests that GetPaginatedRespondentApplications returns an empty list when no matching applications exist.
    /// </summary>
    [Fact]
    public async Task GetPaginatedApplications_ShouldReturnEmptyList_WhenNoApplicationsExist()
    {
        // Arrange
        ProjectRecordSearchRequest searchQuery = new ProjectRecordSearchRequest();
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);

        // Act
        int pageIndex = 1;
        int pageSize = 10;
        var result = await applicationsService.GetPaginatedApplications(searchQuery, pageIndex, pageSize, null, null);

        // Assert
        result.ShouldNotBeNull();
        result.Items.ShouldBeEmpty();
        result.TotalCount.ShouldBe(0);
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetPaginatedApplications_ShouldPaginateCorrectly_WhenPageSizeIsLimited(List<ProjectRecord> generatedRecords)
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        ProjectRecordSearchRequest searchQuery = new ProjectRecordSearchRequest();

        await _context.ProjectRecords.AddRangeAsync(generatedRecords);
        await _context.SaveChangesAsync();

        // Act
        int pageIndex = 1;
        int pageSize = 2;
        var result = await applicationsService.GetPaginatedApplications(searchQuery, pageIndex, pageSize, null, null);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(Math.Min(pageSize, generatedRecords.Count));
        result.TotalCount.ShouldBe(generatedRecords.Count);
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetPaginatedApplications_ShouldReturnAll_WhenPageSizeIsNull(List<ProjectRecord> generatedRecords)
    {
        // Arrange
        ProjectRecordSearchRequest searchQuery = new ProjectRecordSearchRequest();
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);

        await _context.ProjectRecords.AddRangeAsync(generatedRecords);
        await _context.SaveChangesAsync();

        // Act
        int pageIndex = 1;
        int? pageSize = null;
        var result = await applicationsService.GetPaginatedApplications(searchQuery, pageIndex, pageSize, null, null);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(generatedRecords.Count);
        result.TotalCount.ShouldBe(generatedRecords.Count);
    }
}