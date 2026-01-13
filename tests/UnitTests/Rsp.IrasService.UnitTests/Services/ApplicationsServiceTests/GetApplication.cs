using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;
using Rsp.Service.UnitTests.Fixtures;

namespace Rsp.Service.UnitTests.Services.ApplicationsServiceTests;

/// <summary>
///     Covers the tests for GetApplication method
/// </summary>
public class GetApplication : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly IrasContext _context;

    public GetApplication()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
    }

    /// <summary>
    ///     Tests that correct application is returned by Id
    /// </summary>
    /// <param name="records">Number of records to seed</param>
    /// <param name="generator">Test Data Generator</param>
    [Theory]
    [NoRecursionInlineAutoData(5)]
    public async Task Returns_Application_ById(int records, Generator<ProjectRecord> generator)
    {
        // Arrange
        Mocker.Use<IProjectRecordRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data using number of records to seed
        var applications = await TestData.SeedData(_context, generator, records);

        // get the random application id between 0 and 4
        var applicationId = applications[Random.Shared.Next(0, 4)].Id;

        // Act
        var irasApplication = await Sut.GetApplication(applicationId);

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<ApplicationResponse>();
        irasApplication.Id.ShouldBe(applicationId);
    }

    /// <summary>
    ///     Tests that correct application is returned by Id
    /// </summary>
    /// <param name="records">Number of records to seed</param>
    /// <param name="generator">Test Data Generator</param>
    [Theory]
    [NoRecursionInlineAutoData(5)]
    public async Task Returns_Application_ByIdAndStatus(int records, Generator<ProjectRecord> generator)
    {
        // Arrange
        Mocker.Use<IProjectRecordRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed the data using number of records to seed, some with pending status
        var applications = await TestData.SeedData(_context, generator, records, true);

        // get the random application id between 0 and 4
        var applicationId = applications[2].Id;

        // Act
        var irasApplication = await Sut.GetApplication(applicationId, "pending");

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<ApplicationResponse>();
        irasApplication.Id.ShouldBe(applicationId);
    }

    /// <summary>
    ///     Tests that exception is thrown if Id doesn't exist
    /// </summary>
    /// <param name="generator">Test data generator</param>
    [Theory]
    [NoRecursionInlineAutoData(5)]
    public async Task ReturnsNull_If_Id_DoesNotExist(int records, Generator<ProjectRecord> generator)
    {
        // Arrange
        Mocker.Use<IProjectRecordRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data using number of records to seed
        await TestData.SeedData(_context, generator, records);

        // get the id that won't exist
        var applicationId = DateTime.Now.ToString("HHmmssddMMyyyy");

        // Act/Assert
        var application = await Sut.GetApplication(applicationId);

        application.ShouldBeNull();
    }
}