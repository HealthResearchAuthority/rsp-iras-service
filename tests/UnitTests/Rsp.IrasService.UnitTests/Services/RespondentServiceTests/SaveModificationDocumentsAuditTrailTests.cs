using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.RespondentServiceTests;

public class SaveModificationDocumentsAuditTrailTests : TestServiceBase<RespondentService>
{
    private readonly RespondentRepository _personnelRepository;
    private readonly IrasContext _context;

    public SaveModificationDocumentsAuditTrailTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _personnelRepository = new RespondentRepository(_context);
    }

    [Theory, AutoData]
    public async Task SaveModificationDocumentsAuditTrail_PersistsAuditEntries(
    List<ModificationDocumentsAuditTrailDto> auditDtos)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);
        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.SaveModificationDocumentsAuditTrail(auditDtos);

        // Assert
        var savedEntries = await _context.ModificationDocumentsAuditTrail.ToListAsync();

        savedEntries.Count.ShouldBe(auditDtos.Count);

        // Optional: spot-check mapping correctness
        for (int i = 0; i < auditDtos.Count; i++)
        {
            savedEntries[i].Description.ShouldBe(auditDtos[i].Description);
            savedEntries[i].FileName.ShouldBe(auditDtos[i].FileName);
            savedEntries[i].User.ShouldBe(auditDtos[i].User);
            savedEntries[i].ProjectModificationId.ShouldBe(auditDtos[i].ProjectModificationId);
        }
    }

    [Fact]
    public async Task SaveModificationDocumentsAuditTrail_WithEmptyList_DoesNothing()
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);
        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.SaveModificationDocumentsAuditTrail(new List<ModificationDocumentsAuditTrailDto>());

        // Assert
        var count = await _context.ModificationDocumentsAuditTrail.CountAsync();
        count.ShouldBe(0);
    }

    [Fact]
    public async Task SaveModificationDocumentsAuditTrail_WithNullList_DoesNothing()
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);
        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.SaveModificationDocumentsAuditTrail(null!);

        // Assert
        var count = await _context.ModificationDocumentsAuditTrail.CountAsync();
        count.ShouldBe(0);
    }
}