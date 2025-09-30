using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ApplicationsServiceTests;

/// <summary>
///     Covers the tests for CreateApplication method
/// </summary>
public class CreateApplication : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly IrasContext _context;

    public CreateApplication()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
    }

    /// <summary>
    ///     Tests that applicaiton is created
    /// </summary>
    /// <param name="createApplicationRequest">Represents the model for new application request</param>
    [Theory, AutoData]
    public async Task Returns_CreateApplicationResponse(ApplicationRequest createApplicationRequest)
    {
        // Arrange
        Mocker.Use<IProjectRecordRepository>(_applicationRepository);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // Act
        var irasApplication = await Sut.CreateApplication(createApplicationRequest);

        // Assert
        irasApplication.ShouldNotBeNull();
        irasApplication.ShouldBeOfType<ApplicationResponse>();
        (await _context.ProjectRecords.CountAsync()).ShouldBe(1);
    }
}