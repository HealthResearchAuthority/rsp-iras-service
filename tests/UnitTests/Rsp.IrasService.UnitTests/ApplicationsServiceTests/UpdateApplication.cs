using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;
using Shouldly;
using static Rsp.IrasService.UnitTests.TestData;

namespace Rsp.IrasService.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Covers the tests for UpdateApplication method
/// </summary>
public class UpdateApplication : TestServiceBase<ApplicationsService>
{
    private readonly IrasContext _context;

    private readonly ApplicationRepository _applicationRepository;

    public UpdateApplication()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _applicationRepository = new ApplicationRepository(_context);
    }

    /// <summary>
    /// Tests that applicaiton is updated
    /// </summary>
    /// <param name="createApplicationRequest">Represents the model for application request</param>
    [Theory, InlineAutoData(1)]
    public async Task Updates_And_Returns_CreateApplicationResponse(int records, Generator<ResearchApplication> generator, ApplicationRequest createApplicationRequest)
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
    /// Tests that applicaiton is created
    /// </summary>
    /// <param name="createApplicationRequest">Represents the model for new application request</param>
    [Theory, InlineAutoData(1)]
    public async Task ReturnsNull_If_Id_DoesNotExist(int records, Generator<ResearchApplication> generator, ApplicationRequest createApplicationRequest)
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
}