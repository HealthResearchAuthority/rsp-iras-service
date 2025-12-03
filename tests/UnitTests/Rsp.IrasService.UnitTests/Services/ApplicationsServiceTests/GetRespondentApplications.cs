using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Settings;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ApplicationsServiceTests;

public class GetRespondentApplications : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly IrasContext _context;
    private readonly AppSettings _appSettings;

    public GetRespondentApplications()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
        _appSettings = new AppSettings();
    }

    /// <summary>
    ///     Tests that GetRespondentApplications returns applications for the given respondentId.
    /// </summary>
    [Fact]
    public async Task GetRespondentApplications_ShouldReturnApplicationsForRespondent()
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        var fixedRespondentId = "FixedRespondentId-123"; // Explicit RespondentId

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

        // Adding an application with a different RespondentId (should be filtered out)
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
        var result = await applicationsService.GetRespondentApplications(fixedRespondentId);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(2);

        // Validate fields
        foreach (var application in result)
        {
            var expectedApplication = applicationRequests.First(a => a.Id == application.Id);
            application.CreatedBy.ShouldBe(expectedApplication.CreatedBy);
            application.FullProjectTitle.ShouldBe(expectedApplication.FullProjectTitle);
            application.ShortProjectTitle.ShouldBe(expectedApplication.ShortProjectTitle);
            application.UpdatedBy.ShouldBe(expectedApplication.UpdatedBy);
        }
    }

    /// <summary>
    ///     Tests that GetRespondentApplications returns an empty list when no matching applications exist.
    /// </summary>
    [Fact]
    public async Task GetRespondentApplications_ShouldReturnEmptyList_WhenNoApplicationsExist()
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        var fixedRespondentId = "NonExistentRespondent"; // No applications exist for this ID

        // Act
        var result = await applicationsService.GetRespondentApplications(fixedRespondentId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }
}