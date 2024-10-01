using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;
using Shouldly;

namespace Rsp.IrasService.UnitTests.ApplicationsServiceTests;

/// <summary>
/// Covers the tests for CreateApplication method
/// </summary>
public class CreateApplication : TestServiceBase<ApplicationsService>
{
    private readonly IrasContext _context;

    private readonly ApplicationRepository _applicationRepository;

    public CreateApplication()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _applicationRepository = new ApplicationRepository(_context);
    }

    /// <summary>
    /// Tests that applicaiton is created
    /// </summary>
    /// <param name="createApplicationRequest">Represents the model for new application request</param>
    [Theory, AutoData]
    public async Task Returns_CreateApplicationResponse(ApplicationRequest createApplicationRequest)
    {
        // Arrange
        Mocker.Use<IApplicationRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // Act
        var irasApplication = await Sut.CreateApplication(createApplicationRequest);

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<ApplicationResponse>();
        (await _context.ResearchApplications.CountAsync()).ShouldBe(1);
    }
}