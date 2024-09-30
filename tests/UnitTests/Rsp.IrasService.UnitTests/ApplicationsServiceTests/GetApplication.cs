using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Repositories;
using Rsp.IrasService.Application.Responses;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;
using Shouldly;
using static Rsp.IrasService.UnitTests.TestData;

namespace Rsp.IrasService.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Covers the tests for GetApplication method
/// </summary>
public class GetApplication : TestServiceBase<ApplicationsService>
{
    private readonly IrasContext _context;
    private readonly ApplicationRepository _applicationRepository;

    public GetApplication()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _applicationRepository = new ApplicationRepository(_context);
    }

    /// <summary>
    /// Tests that correct application is returned by Id
    /// </summary>
    /// <param name="records">Number of records to seed</param>
    /// <param name="generator">Test Data Generator</param>
    [Theory, InlineAutoData(5)]
    public async Task Returns_Application_ById(int records, Generator<ResearchApplication> generator)
    {
        // Arrange
        Mocker.Use<IApplicationRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data using number of records to seed
        var applications = await SeedData(_context, generator, records);

        // get the random application id between 0 and 4
        var applicationId = applications[Random.Shared.Next(0, 4)].ApplicationId;

        // Act
        var irasApplication = await Sut.GetApplication(applicationId);

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<GetApplicationResponse>();
        irasApplication.Id.ShouldBe(applicationId);
    }

    /// <summary>
    /// Tests that correct application is returned by Id
    /// </summary>
    /// <param name="records">Number of records to seed</param>
    /// <param name="generator">Test Data Generator</param>
    [Theory, InlineAutoData(5)]
    public async Task Returns_Application_ByIdAndStatus(int records, Generator<ResearchApplication> generator)
    {
        // Arrange
        Mocker.Use<IApplicationRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed the data using number of records to seed, some with pending status
        var applications = await SeedData(_context, generator, records, true);

        // get the random application id between 0 and 4
        var applicationId = applications[2].ApplicationId;

        // Act
        var irasApplication = await Sut.GetApplication(applicationId, "pending");

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<GetApplicationResponse>();
        irasApplication.Id.ShouldBe(applicationId);
    }

    /// <summary>
    /// Tests that exception is thrown if Id doesn't exist
    /// </summary>
    /// <param name="generator">Test data generator</param>
    [Theory, InlineAutoData(5)]
    public async Task ThrowsException_If_Id_DoesNotExist(int records, Generator<ResearchApplication> generator)
    {
        // Arrange
        Mocker.Use<IApplicationRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data using number of records to seed
        var applications = await SeedData(_context, generator, records);

        // get the id that won't exist
        var applicationId = DateTime.Now.ToString("HHmmssddMMyyyy");

        // Act/Assert
        await Should.ThrowAsync<NotImplementedException>(Sut.GetApplication(applicationId));
    }
}