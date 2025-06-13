using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ApplicationsServiceTests;

/// <summary>
///     Covers the tests for UpdateApplication method
/// </summary>
public class UpdateApplication : TestServiceBase<ApplicationsService>
{
    private readonly ApplicationRepository _applicationRepository;
    private readonly IrasContext _context;

    public UpdateApplication()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _applicationRepository = new ApplicationRepository(_context);
    }

    /// <summary>
    ///     Tests that applicaiton is updated
    /// </summary>
    /// <param name="createApplicationRequest">Represents the model for application request</param>
    [Theory]
    [InlineAutoData(1)]
    public async Task Updates_And_Returns_CreateApplicationResponse(int records,
        Generator<ProjectApplication> generator, ApplicationRequest createApplicationRequest)
    {
        // Arrange
        Mocker.Use<IApplicationRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data with the number of records
        var applications = await TestData.SeedData(_context, generator, records);

        createApplicationRequest.ProjectApplicationId = applications[0].ProjectApplicationId;
        createApplicationRequest.StartDate = applications[0].CreatedDate;

        // Act
        var irasApplication = await Sut.UpdateApplication(createApplicationRequest);

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<ApplicationResponse>();
        irasApplication.ShouldSatisfyAllConditions
        (
            app => app.ProjectApplicationId.ShouldBe(createApplicationRequest.ProjectApplicationId),
            app => app.Title.ShouldBe(createApplicationRequest.Title),
            app => app.CreatedDate.ShouldBe(createApplicationRequest.StartDate!.Value),
            app => app.Status.ShouldBe(createApplicationRequest.Status)
        );
    }

    /// <summary>
    ///     Tests that applicaiton is created
    /// </summary>
    /// <param name="createApplicationRequest">Represents the model for new application request</param>
    [Theory]
    [InlineAutoData(1)]
    public async Task ReturnsNull_If_Id_DoesNotExist(int records, Generator<ProjectApplication> generator,
        ApplicationRequest createApplicationRequest)
    {
        // Arrange
        Mocker.Use<IApplicationRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data with the number of records
        await TestData.SeedData(_context, generator, records);

        // get the id that won't exists
        createApplicationRequest.ProjectApplicationId = DateTime.Now.ToString("yyyyddMMHHmmss");

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
        var applicationsService = new ApplicationsService(_applicationRepository);
        var fixedRespondentId = "FixedRespondentId-123"; // Explicitly setting RespondentId

        var applicationRequest = new ApplicationRequest
        {
            ProjectApplicationId = Guid.NewGuid().ToString(),
            Respondent = new RespondentDto { ProjectApplicationRespondentId = fixedRespondentId },
            CreatedBy = "CreatedBy",
            Description = "Description",
            Title = "Title",
            UpdatedBy = "UpdatedBy"
        };

        var existingApplication = new ProjectApplication
        {
            ProjectApplicationId = applicationRequest.ProjectApplicationId,
            ProjectApplicationRespondentId = fixedRespondentId,
            CreatedBy = applicationRequest.CreatedBy,
            Description = applicationRequest.Description,
            Title = applicationRequest.Title,
            UpdatedBy = applicationRequest.UpdatedBy
        };

        await _context.ProjectApplications.AddAsync(existingApplication);
        await _context.SaveChangesAsync();

        // Ensure EF Core is tracking the entity
        _context.Entry(existingApplication).State = EntityState.Detached;

        // Act
        var updatedApplication = await applicationsService.UpdateApplication(applicationRequest);

        // Assert
        updatedApplication.ShouldNotBeNull();
        updatedApplication.ShouldBeOfType<ApplicationResponse>();

        // Reload entity from database to ensure the update was persisted
        var dbApplication = await _context.ProjectApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ProjectApplicationId == existingApplication.ProjectApplicationId);

        dbApplication.ShouldNotBeNull();
        dbApplication.ProjectApplicationRespondentId.ShouldBe(fixedRespondentId);

        (await _context.ProjectApplications.CountAsync()).ShouldBe(1);
    }
}