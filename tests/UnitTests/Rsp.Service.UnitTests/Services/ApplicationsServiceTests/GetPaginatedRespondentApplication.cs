using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Settings;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;
using Rsp.Service.UnitTests.Fixtures;

namespace Rsp.Service.UnitTests.Services.ApplicationsServiceTests;

public class GetPaginatedRespondentApplications : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly RspContext _context;
    private readonly AppSettings _appSettings;

    public GetPaginatedRespondentApplications()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new RspContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
        _appSettings = new AppSettings() { QuestionIds = new Dictionary<string, string> { { "ShortProjectTitle", "IQA0002" } } };
    }

    /// <summary>
    /// Tests that GetPaginatedRespondentApplications returns paginated applications for the given respondentId.
    /// </summary>
    [Fact]
    public async Task GetPaginatedRespondentApplications_ShouldReturnApplicationsForRespondent()
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        var fixedRespondentId = "FixedRespondentId-123";
        ApplicationSearchRequest searchQuery = new ApplicationSearchRequest();

        var applicationRequests = new List<ApplicationRequest>
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = fixedRespondentId,
                CreatedBy = "User1",
                FullProjectTitle = "Description1",
                ShortProjectTitle = "Title1",
                UpdatedBy = "Updater1"
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = fixedRespondentId,
                CreatedBy = "User2",
                FullProjectTitle = "Description2",
                ShortProjectTitle = "Title2",
                UpdatedBy = "Updater2"
            }
        };

        var researchApplications = applicationRequests.Select(request => new ProjectRecord
        {
            Id = request.Id,
            UserId = request.UserId,
            CreatedBy = request.CreatedBy,
            FullProjectTitle = request.FullProjectTitle,
            ShortProjectTitle = request.ShortProjectTitle,
            UpdatedBy = request.UpdatedBy
        }).ToList();

        // Add an unrelated application
        researchApplications.Add(new ProjectRecord
        {
            Id = Guid.NewGuid().ToString(),
            UserId = "OtherRespondent",
            CreatedBy = "User3",
            FullProjectTitle = "Description3",
            ShortProjectTitle = "Title3",
            UpdatedBy = "Updater3"
        });

        await _context.ProjectRecords.AddRangeAsync(researchApplications);
        await _context.SaveChangesAsync();

        // Act
        int pageIndex = 1;
        int pageSize = 10;
        var result = await applicationsService.GetPaginatedRespondentApplications(fixedRespondentId, searchQuery, pageIndex, pageSize, null, null);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(2);
        result.TotalCount.ShouldBe(2);

        foreach (var application in result.Items)
        {
            var expectedApplication = applicationRequests.First(a => a.Id == application.Id);
            application.CreatedBy.ShouldBe(expectedApplication.CreatedBy);
            application.FullProjectTitle.ShouldBe(expectedApplication.FullProjectTitle);
            application.ShortProjectTitle.ShouldBe(expectedApplication.ShortProjectTitle);
            application.UpdatedBy.ShouldBe(expectedApplication.UpdatedBy);
        }
    }

    /// <summary>
    /// Tests that GetPaginatedRespondentApplications returns an empty list when no matching applications exist.
    /// </summary>
    [Fact]
    public async Task GetPaginatedRespondentApplications_ShouldReturnEmptyList_WhenNoApplicationsExist()
    {
        // Arrange
        ApplicationSearchRequest searchQuery = new ApplicationSearchRequest();
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        var fixedRespondentId = "NonExistentRespondent"; // No applications exist for this ID

        // Act
        int pageIndex = 1;
        int pageSize = 10;
        var result = await applicationsService.GetPaginatedRespondentApplications(fixedRespondentId, searchQuery, pageIndex, pageSize, null, null);

        // Assert
        result.ShouldNotBeNull();
        result.Items.ShouldBeEmpty();
        result.TotalCount.ShouldBe(0);
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetPaginatedRespondentApplications_ShouldPaginateCorrectly_WhenPageSizeIsLimited(List<ProjectRecord> generatedRecords)
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        ApplicationSearchRequest searchQuery = new ApplicationSearchRequest();
        var respondentId = "LimitedPageSizeRespondent";

        foreach (var record in generatedRecords)
        {
            record.UserId = respondentId;
        }

        await _context.ProjectRecords.AddRangeAsync(generatedRecords);
        await _context.SaveChangesAsync();

        // Act
        int pageIndex = 1;
        int pageSize = 2;
        var result = await applicationsService.GetPaginatedRespondentApplications(respondentId, searchQuery, pageIndex, pageSize, null, null);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(Math.Min(pageSize, generatedRecords.Count));
        result.TotalCount.ShouldBe(generatedRecords.Count);
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetPaginatedRespondentApplications_ShouldReturnAll_WhenPageSizeIsNull(List<ProjectRecord> generatedRecords)
    {
        // Arrange
        ApplicationSearchRequest searchQuery = new ApplicationSearchRequest();
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        var respondentId = "NoPageSizeRespondent";

        foreach (var record in generatedRecords)
        {
            record.UserId = respondentId;
        }

        await _context.ProjectRecords.AddRangeAsync(generatedRecords);
        await _context.SaveChangesAsync();

        // Act
        int pageIndex = 1;
        int? pageSize = null;
        var result = await applicationsService.GetPaginatedRespondentApplications(respondentId, searchQuery, pageIndex, pageSize, null, null);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(generatedRecords.Count);
        result.TotalCount.ShouldBe(generatedRecords.Count);
    }
}