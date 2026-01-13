using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Settings;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;
using Rsp.Service.UnitTests.Fixtures;

namespace Rsp.Service.UnitTests.Services.ApplicationsServiceTests;

/// <summary>
///     Covers the tests for UpdateApplication method
/// </summary>
public class UpdateApplication : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly IrasContext _context;
    private readonly AppSettings _appSettings;

    public UpdateApplication()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
        _appSettings = new AppSettings();
    }

    /// <summary>
    ///     Tests that applicaiton is updated
    /// </summary>
    /// <param name="createApplicationRequest">Represents the model for application request</param>
    [Theory]
    [NoRecursionInlineAutoData(1)]
    public async Task Updates_And_Returns_CreateApplicationResponse(int records,
        Generator<ProjectRecord> generator, ApplicationRequest createApplicationRequest)
    {
        // Arrange
        Mocker.Use<IProjectRecordRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data with the number of records
        var applications = await TestData.SeedData(_context, generator, records);

        createApplicationRequest.Id = applications[0].Id;
        createApplicationRequest.StartDate = applications[0].CreatedDate;

        // Act
        var irasApplication = await Sut.UpdateApplication(createApplicationRequest);

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<ApplicationResponse>();
        irasApplication.ShouldSatisfyAllConditions
        (
            app => app.Id.ShouldBe(createApplicationRequest.Id),
            app => app.ShortProjectTitle.ShouldBe(createApplicationRequest.ShortProjectTitle),
            app => app.CreatedDate.ShouldBe(createApplicationRequest.StartDate!.Value),
            app => app.Status.ShouldBe(createApplicationRequest.Status)
        );
    }

    /// <summary>
    ///     Tests that applicaiton is created
    /// </summary>
    /// <param name="createApplicationRequest">Represents the model for new application request</param>
    [Theory]
    [NoRecursionInlineAutoData(1)]
    public async Task ReturnsNull_If_Id_DoesNotExist(int records, Generator<ProjectRecord> generator,
        ApplicationRequest createApplicationRequest)
    {
        // Arrange
        Mocker.Use<IProjectRecordRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data with the number of records
        await TestData.SeedData(_context, generator, records);

        // get the id that won't exists
        createApplicationRequest.Id = DateTime.Now.ToString("yyyyddMMHHmmss");

        // Act/Assert
        var application = await Sut.UpdateApplication(createApplicationRequest);

        application.ShouldBeNull();
    }

    /// <summary>
    ///     Tests that UpdateApplication correctly updates an application.
    /// </summary>
    [Fact]
    public async Task UpdateApplication_ShouldUpdateAndReturnUpdatedApplication()
    {
        // Arrange
        var applicationsService = new ApplicationsService(_applicationRepository, _appSettings);
        var fixedRespondentId = "FixedRespondentId-123"; // Explicitly setting RespondentId

        var applicationRequest = new ApplicationRequest
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "CreatedBy",
            FullProjectTitle = "Description",
            ShortProjectTitle = "Title",
            UpdatedBy = "UpdatedBy"
        };

        var existingApplication = new ProjectRecord
        {
            Id = applicationRequest.Id,
            UserId = fixedRespondentId,
            CreatedBy = applicationRequest.CreatedBy,
            FullProjectTitle = applicationRequest.FullProjectTitle,
            ShortProjectTitle = applicationRequest.ShortProjectTitle,
            UpdatedBy = applicationRequest.UpdatedBy
        };

        await _context.ProjectRecords.AddAsync(existingApplication);
        await _context.SaveChangesAsync();

        // Ensure EF Core is tracking the entity
        _context.Entry(existingApplication).State = EntityState.Detached;

        // Act
        var updatedApplication = await applicationsService.UpdateApplication(applicationRequest);

        // Assert
        updatedApplication.ShouldNotBeNull();
        updatedApplication.ShouldBeOfType<ApplicationResponse>();

        // Reload entity from database to ensure the update was persisted
        var dbApplication = await _context.ProjectRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == existingApplication.Id);

        dbApplication.ShouldNotBeNull();
        dbApplication.UserId.ShouldBe(fixedRespondentId);

        (await _context.ProjectRecords.CountAsync()).ShouldBe(1);
    }
}