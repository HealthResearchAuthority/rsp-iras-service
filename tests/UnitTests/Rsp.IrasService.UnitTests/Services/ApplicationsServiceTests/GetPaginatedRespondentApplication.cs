using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsp.IrasService.UnitTests.Services.ApplicationsServiceTests;

public class GetPaginatedRespondentApplications : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly IrasContext _context;

    public GetPaginatedRespondentApplications()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
    }

    /// <summary>
    /// Tests that GetPaginatedRespondentApplications returns paginated applications for the given respondentId.
    /// </summary>
    [Fact]
    public async Task GetPaginatedRespondentApplications_ShouldReturnApplicationsForRespondent()
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository);
        var fixedRespondentId = "FixedRespondentId-123";

        var applicationRequests = new List<ApplicationRequest>
    {
        new()
        {
            Id = Guid.NewGuid().ToString(),
            Respondent = new RespondentDto { Id = fixedRespondentId },
            CreatedBy = "User1",
            Description = "Description1",
            Title = "Title1",
            UpdatedBy = "Updater1"
        },
        new()
        {
            Id = Guid.NewGuid().ToString(),
            Respondent = new RespondentDto { Id = fixedRespondentId },
            CreatedBy = "User2",
            Description = "Description2",
            Title = "Title2",
            UpdatedBy = "Updater2"
        }
    };

        var researchApplications = applicationRequests.Select(request => new ProjectRecord
        {
            Id = request.Id,
            ProjectPersonnelId = request.Respondent.Id,
            CreatedBy = request.CreatedBy,
            Description = request.Description,
            Title = request.Title,
            UpdatedBy = request.UpdatedBy
        }).ToList();

        // Add an unrelated application
        researchApplications.Add(new ProjectRecord
        {
            Id = Guid.NewGuid().ToString(),
            ProjectPersonnelId = "OtherRespondent",
            CreatedBy = "User3",
            Description = "Description3",
            Title = "Title3",
            UpdatedBy = "Updater3"
        });

        await _context.ProjectRecords.AddRangeAsync(researchApplications);
        await _context.SaveChangesAsync();

        // Act
        int pageIndex = 0;
        int pageSize = 10;
        var result = await applicationsService.GetPaginatedRespondentApplications(fixedRespondentId, null, pageIndex, pageSize);

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(2);
        result.TotalCount.ShouldBe(2);

        foreach (var application in result.Items)
        {
            var expectedApplication = applicationRequests.First(a => a.Id == application.Id);
            application.CreatedBy.ShouldBe(expectedApplication.CreatedBy);
            application.Description.ShouldBe(expectedApplication.Description);
            application.Title.ShouldBe(expectedApplication.Title);
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
        var applicationsService = new ApplicationsService(_applicationRepository);
        var fixedRespondentId = "NonExistentRespondent"; // No applications exist for this ID

        // Act
        int pageIndex = 0;
        int pageSize = 10;
        var result = await applicationsService.GetPaginatedRespondentApplications(fixedRespondentId, null, pageIndex, pageSize);

        // Assert
        result.ShouldNotBeNull();
        result.Items.ShouldBeEmpty();
        result.TotalCount.ShouldBe(0);
    }
}