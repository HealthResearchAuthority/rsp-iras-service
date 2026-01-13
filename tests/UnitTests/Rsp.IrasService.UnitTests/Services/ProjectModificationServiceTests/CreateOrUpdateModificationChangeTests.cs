using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for CreateOrUpdateModificationChange method
/// </summary>
public class CreateOrUpdateModificationChangeTests : TestServiceBase<ProjectModificationService>
{
    private readonly ProjectModificationRepository _modificationRepository;
    private readonly IrasContext _context;

    public CreateOrUpdateModificationChangeTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _modificationRepository = new ProjectModificationRepository(_context);
    }

    /// <summary>
    ///     Tests that modification change is created or updated
    /// </summary>
    [Theory]
    [AutoData]
    public async Task Returns_ModificationChangeResponse(ModificationChangeRequest modificationChangeRequest)
    {
        // Arrange
        Mocker.Use<IProjectModificationRepository>(_modificationRepository);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var modificationChangeResponse = await Sut.CreateOrUpdateModificationChange(modificationChangeRequest);

        // Assert
        modificationChangeResponse.ShouldNotBeNull();
        modificationChangeResponse.ShouldBeOfType<ModificationChangeResponse>();
        (await _context.ProjectModificationChanges.CountAsync()).ShouldBe(1);
    }
}