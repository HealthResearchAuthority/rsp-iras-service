using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for CreateModification method
/// </summary>
public class CreateModificationTests : TestServiceBase<ProjectModificationService>
{
    private readonly ProjectModificationRepository _modificationRepository;
    private readonly RspContext _context;

    public CreateModificationTests()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
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