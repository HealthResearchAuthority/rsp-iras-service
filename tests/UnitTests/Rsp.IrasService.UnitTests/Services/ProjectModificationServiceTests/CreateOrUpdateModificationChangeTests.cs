using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

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