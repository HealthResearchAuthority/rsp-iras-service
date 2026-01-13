using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Settings;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ApplicationsServiceTests;

public class UpdateProjectRecordStatusTests : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly IrasContext _context;
    private readonly AppSettings _appSettings;

    public UpdateProjectRecordStatusTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
        _appSettings = new AppSettings();
    }

    [Fact]
    public async Task UpdateApplication_ShouldUpdateAndReturnUpdatedApplication()
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        var fixedRespondentId = "FixedRespondentId-123";
        var applicationRequest = new ApplicationRequest
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "CreatedBy",
            FullProjectTitle = "Description",
            ShortProjectTitle = "Title",
            UpdatedBy = "UpdatedBy"
        };

        var existingProjectRecord = new ProjectRecord
        {
            Id = applicationRequest.Id,
            Status = applicationRequest.Status,
            UserId = fixedRespondentId,
            CreatedBy = applicationRequest.CreatedBy,
            FullProjectTitle = applicationRequest.FullProjectTitle,
            ShortProjectTitle = applicationRequest.ShortProjectTitle,
            UpdatedBy = applicationRequest.UpdatedBy
        };
        await _context.ProjectRecords.AddAsync(existingProjectRecord);
        await _context.SaveChangesAsync();

        // Ensure EF Core is tracking the entity
        _context.Entry(existingProjectRecord).State = EntityState.Detached;

        // Act
        var updatedApplication = applicationsService.UpdateProjectRecordStatus(existingProjectRecord.Id, existingProjectRecord.Status);

        // Assert
        updatedApplication.ShouldNotBeNull();

        // Reload entity from database to ensure the update was persisted
        var dbApplication = await _context.ProjectRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == existingProjectRecord.Id);

        dbApplication.ShouldNotBeNull();
        dbApplication.UserId.ShouldBe(fixedRespondentId);
        (await _context.ProjectRecords.CountAsync()).ShouldBe(1);
    }
}