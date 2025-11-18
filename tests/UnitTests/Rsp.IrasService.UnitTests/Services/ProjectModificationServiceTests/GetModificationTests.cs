using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

public class GetModificationTests : TestServiceBase<ProjectModificationService>
{
    private readonly IrasContext _context;
    private readonly ProjectModificationRepository _repo;

    public GetModificationTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
        _context = new IrasContext(options);
        _repo = new ProjectModificationRepository(_context);
    }

    [Fact]
    public async Task Returns_Response_When_Found()
    {
        // Arrange
        var mod = new ProjectModification
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            ModificationIdentifier = "M-1",
            Status = "Draft",
            CreatedBy = "u",
            UpdatedBy = "u",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
        await _context.ProjectModifications.AddAsync(mod);
        await _context.SaveChangesAsync();

        Mocker.Use<IProjectModificationRepository>(_repo);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var response = await Sut.GetModification(mod.Id.ToString());

        // Assert
        response.ShouldNotBeNull();
        response.Id.ShouldBe(mod.Id);
        response.ProjectRecordId.ShouldBe(mod.ProjectRecordId);
    }

    [Fact]
    public async Task Returns_Null_When_Not_Found()
    {
        // Arrange
        Mocker.Use<IProjectModificationRepository>(_repo);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var response = await Sut.GetModification(Guid.NewGuid().ToString());

        // Assert
        response.ShouldBeNull();
    }
}
