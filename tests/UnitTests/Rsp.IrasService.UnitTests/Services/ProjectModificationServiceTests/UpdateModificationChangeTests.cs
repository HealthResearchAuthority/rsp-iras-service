using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

public class UpdateModificationChangeTests : TestServiceBase<ProjectModificationService>
{
    private readonly IrasContext _context;
    private readonly ProjectModificationRepository _repo;

    public UpdateModificationChangeTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
        _context = new IrasContext(options);
        _repo = new ProjectModificationRepository(_context);
    }

    [Fact]
    public async Task Updates_ModificationChange_In_Database()
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
        var change = new ProjectModificationChange
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = mod.Id,
            AreaOfChange = "Area",
            SpecificAreaOfChange = "Specific",
            Status = "Draft",
            CreatedBy = "u",
            UpdatedBy = "u",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
        await _context.ProjectModifications.AddAsync(mod);
        await _context.ProjectModificationChanges.AddAsync(change);
        await _context.SaveChangesAsync();

        Mocker.Use<IProjectModificationRepository>(_repo);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        var req = new UpdateModificationChangeRequest
        {
            Id = change.Id,
            ProjectModificationId = mod.Id,
            AreaOfChange = "Area",
            SpecificAreaOfChange = "Specific",
            Status = "Submitted",
            CreatedBy = change.CreatedBy,
            UpdatedBy = change.UpdatedBy,
            CreatedDate = change.CreatedDate,
            UpdatedDate = DateTime.UtcNow
        };

        // Act
        await Sut.UpdateModificationChange(req);

        // Assert
        var updated = await _context.ProjectModificationChanges.SingleAsync(c => c.Id == change.Id);
        updated.Status.ShouldBe("Submitted");
    }
}
