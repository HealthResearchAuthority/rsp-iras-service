using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

public class ProjectModificationServiceTests : TestServiceBase<ProjectModificationService>
{
    private readonly ProjectModificationRepository _modificationRepository;
    private readonly IrasContext _context;

    public ProjectModificationServiceTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _modificationRepository = new ProjectModificationRepository(_context);
    }

    [Fact]
    public async Task UpdateModificationStatus_Updates_Status_For_Modification_And_Changes()
    {
        // Arrange - seed a modification with two changes
        var modId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        var modification = new ProjectModification
        {
            Id = modId,
            ProjectRecordId = "PR-1",
            ModificationNumber = 1,
            ModificationIdentifier = "IRAS/",
            Status = "Draft",
            CreatedDate = now,
            UpdatedDate = now,
            CreatedBy = "tester",
            UpdatedBy = "tester"
        };
        var change1 = new ProjectModificationChange
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = modId,
            AreaOfChange = "Area1",
            SpecificAreaOfChange = "Detail1",
            Status = "Draft",
            CreatedDate = now,
            UpdatedDate = now,
            CreatedBy = "tester",
            UpdatedBy = "tester"
        };
        var change2 = new ProjectModificationChange
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = modId,
            AreaOfChange = "Area2",
            SpecificAreaOfChange = "Detail2",
            Status = "Draft",
            CreatedDate = now,
            UpdatedDate = now,
            CreatedBy = "tester",
            UpdatedBy = "tester"
        };

        await _context.ProjectModifications.AddAsync(modification);
        await _context.ProjectModificationChanges.AddRangeAsync(change1, change2);
        await _context.SaveChangesAsync();

        Mocker.Use<IProjectModificationRepository>(_modificationRepository);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        var newStatus = "Submitted";

        // Act
        await Sut.UpdateModificationStatus(modId, newStatus);

        // Assert - reload and verify status changes persisted
        var updated = await _context.ProjectModifications
            .Include(m => m.ProjectModificationChanges)
            .SingleAsync(m => m.Id == modId);

        updated.Status.ShouldBe(newStatus);
        updated.ProjectModificationChanges.ShouldAllBe(c => c.Status == newStatus);
    }

    [Fact]
    public async Task RemoveModificationChange_Removes_Change_From_Database()
    {
        // Arrange - seed a modification change
        var modId = Guid.NewGuid();
        var now = DateTime.UtcNow;

        var modification = new ProjectModification
        {
            Id = modId,
            ProjectRecordId = "PR-1",
            ModificationNumber = 1,
            ModificationIdentifier = "IRAS/",
            Status = "Draft",
            CreatedDate = now,
            UpdatedDate = now,
            CreatedBy = "tester",
            UpdatedBy = "tester"
        };

        var change = new ProjectModificationChange
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = modId,
            AreaOfChange = "Area",
            SpecificAreaOfChange = "Detail",
            Status = "Draft",
            CreatedDate = now,
            UpdatedDate = now,
            CreatedBy = "tester",
            UpdatedBy = "tester"
        };

        await _context.ProjectModifications.AddAsync(modification);
        await _context.ProjectModificationChanges.AddAsync(change);
        await _context.SaveChangesAsync();

        Mocker.Use<IProjectModificationRepository>(_modificationRepository);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        await Sut.RemoveModificationChange(change.Id);

        // Assert
        (await _context.ProjectModificationChanges.FindAsync(change.Id)).ShouldBeNull();
    }

    [Fact]
    public async Task DeleteModification_Removes_From_Database()
    {
        // Arrange - seed a modification change + doc + doc answer
        var modId = Guid.NewGuid();
        var now = DateTime.UtcNow;

        var modification = new ProjectModification
        {
            Id = modId,
            ProjectRecordId = "PR-1",
            ModificationNumber = 1,
            ModificationIdentifier = "IRAS/",
            Status = "Draft",
            CreatedDate = now,
            UpdatedDate = now,
            CreatedBy = "tester",
            UpdatedBy = "tester"
        };

        var change = new ProjectModificationChange
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = modId,
            AreaOfChange = "Area",
            SpecificAreaOfChange = "Detail",
            Status = "Draft",
            CreatedDate = now,
            UpdatedDate = now,
            CreatedBy = "tester",
            UpdatedBy = "tester"
        };

        var doc = new ModificationDocument
        {
            Id = Guid.NewGuid(),
            ProjectModificationId = modId,
            ProjectPersonnelId = "PP-1",
            ProjectRecordId = "PR-1",
            DocumentStoragePath = "/blob/doc1.pdf",
            FileName = "doc1.pdf",
            FileSize = 123,
            Status = "Pending"
        };

        var docAns = new ModificationDocumentAnswer
        {
            Id = Guid.NewGuid(),
            ModificationDocumentId = doc.Id,
            QuestionId = "Q1",
            VersionId = "V1",
            Category = "Cat",
            Section = "Sec",
            Response = "Yes"
        };

        await _context.ProjectModifications.AddAsync(modification);
        await _context.ProjectModificationChanges.AddAsync(change);
        await _context.ModificationDocuments.AddAsync(doc);
        await _context.ModificationDocumentAnswers.AddAsync(docAns);
        await _context.SaveChangesAsync();

        Mocker.Use<IProjectModificationRepository>(_modificationRepository);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        await Sut.DeleteModification(modification.Id);

        // Assert
        (await _context.ProjectModificationChanges.FindAsync(change.Id)).ShouldBeNull();
        (await _context.ModificationDocuments.FindAsync(doc.Id)).ShouldBeNull();
        (await _context.ModificationDocumentAnswers.FindAsync(docAns.Id)).ShouldBeNull();
        (await _context.ProjectModifications.FindAsync(modId)).ShouldBeNull();
    }
}