using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ApplicationsServiceTests;

/// <summary>
///     Covers the tests for CreateApplication method
/// </summary>
public class CreateApplication : TestServiceBase<ApplicationsService>
{
    private readonly ProjectRecordRepository _applicationRepository;
    private readonly RspContext _context;

    public CreateApplication()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
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