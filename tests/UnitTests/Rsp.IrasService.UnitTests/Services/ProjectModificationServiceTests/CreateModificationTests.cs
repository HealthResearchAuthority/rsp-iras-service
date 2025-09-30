using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for CreateModification method
/// </summary>
public class CreateModificationTests : TestServiceBase<ProjectModificationService>
{
    private readonly ProjectModificationRepository _modificationRepository;
    private readonly IrasContext _context;

    public CreateModificationTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _modificationRepository = new ProjectModificationRepository(_context);
    }

    /// <summary>
    ///     Tests that modification is created
    /// </summary>
    [Theory, AutoData]
    public async Task Returns_ModificationResponse(ModificationRequest modificationRequest)
    {
        // Arrange
        Mocker.Use<IProjectModificationRepository>(_modificationRepository);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var modificationResponse = await Sut.CreateModification(modificationRequest);

        // Assert
        modificationResponse.ShouldNotBeNull();
        modificationResponse.ShouldBeOfType<ModificationResponse>();
        (await _context.ProjectModifications.CountAsync()).ShouldBe(1);
    }
}