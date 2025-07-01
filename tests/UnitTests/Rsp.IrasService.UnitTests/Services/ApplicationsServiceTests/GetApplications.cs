using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ApplicationsServiceTests;

/// <summary>
///     Covers the tests for GetApplications method
/// </summary>
public class GetApplications : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly IrasContext _context;

    public GetApplications()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
    }

    /// <summary>
    ///     Tests that all applications are returned
    /// </summary>
    /// <param name="generator">Test data generator</param>
    [Theory]
    [InlineAutoData(5)]
    public async Task Returns_AllApplications(int records, Generator<ProjectRecord> generator)
    {
        // Arrange
        Mocker.Use<IProjectRecordRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed data using number of records to seed
        await TestData.SeedData(_context, generator, records);

        // Act
        var applications = await Sut.GetApplications();

        // Assert
        applications.ShouldNotBeNull();
        applications.Count().ShouldBe(records);
    }

    /// <summary>
    ///     Tests the all applications are returned by the status
    /// </summary>
    /// <param name="records">Number of records to seed</param>
    /// <param name="generator">Test data generator</param>
    [Theory]
    [InlineAutoData(5, true, 2)]
    [InlineAutoData(5, false, 0)]
    public async Task Returns_ApplicationByStatusOrEmpty(int records, bool updateStatus, int expected,
        Generator<ProjectRecord> generator)
    {
        // Arrange
        Mocker.Use<IProjectRecordRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // seed the data using number of records to seed, some with pending status
        await TestData.SeedData(_context, generator, records, updateStatus);

        // Act
        var applications = await Sut.GetApplications("pending");

        // Assert
        applications.ShouldNotBeNull();
        applications.Count().ShouldBe(expected);
    }
}