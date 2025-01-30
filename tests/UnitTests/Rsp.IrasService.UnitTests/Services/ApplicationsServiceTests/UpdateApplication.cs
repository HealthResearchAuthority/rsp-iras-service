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
        Generator<ResearchApplication> generator, ApplicationRequest createApplicationRequest)
    {
        // Arrange
        Mocker.Use<IApplicationRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data with the number of records
        var applications = await SeedData(_context, generator, records);

        createApplicationRequest.ApplicationId = applications[0].ApplicationId;
        createApplicationRequest.StartDate = applications[0].CreatedDate;

        // Act
        var irasApplication = await Sut.UpdateApplication(createApplicationRequest);

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<ApplicationResponse>();
        irasApplication.ShouldSatisfyAllConditions
        (
            app => app.ApplicationId.ShouldBe(createApplicationRequest.ApplicationId),
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
    public async Task ReturnsNull_If_Id_DoesNotExist(int records, Generator<ResearchApplication> generator,
        ApplicationRequest createApplicationRequest)
    {
        // Arrange
        Mocker.Use<IApplicationRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data with the number of records
        await SeedData(_context, generator, records);

        // get the id that won't exists
        createApplicationRequest.ApplicationId = DateTime.Now.ToString("yyyyddMMHHmmss");

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
            ApplicationId = Guid.NewGuid().ToString(),
            Respondent = new RespondentDto { RespondentId = fixedRespondentId },
            CreatedBy = "CreatedBy",
            Description = "Description",
            Title = "Title",
            UpdatedBy = "UpdatedBy"
        };

        var existingApplication = new ResearchApplication
        {
            ApplicationId = applicationRequest.ApplicationId,
            RespondentId = fixedRespondentId,
            CreatedBy = applicationRequest.CreatedBy,
            Description = applicationRequest.Description,
            Title = applicationRequest.Title,
            UpdatedBy = applicationRequest.UpdatedBy
        };

        await _context.ResearchApplications.AddAsync(existingApplication);
        await _context.SaveChangesAsync();

        // Ensure EF Core is tracking the entity
        _context.Entry(existingApplication).State = EntityState.Detached;

        // Act
        var updatedApplication = await applicationsService.UpdateApplication(applicationRequest);

        // Assert
        updatedApplication.ShouldNotBeNull();
        updatedApplication.ShouldBeOfType<ApplicationResponse>();

        // Reload entity from database to ensure the update was persisted
        var dbApplication = await _context.ResearchApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ApplicationId == existingApplication.ApplicationId);

        dbApplication.ShouldNotBeNull();
        dbApplication.RespondentId.ShouldBe(fixedRespondentId);

        (await _context.ResearchApplications.CountAsync()).ShouldBe(1);
    }
}