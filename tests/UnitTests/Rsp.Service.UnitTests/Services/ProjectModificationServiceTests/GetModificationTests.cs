using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

public class GetModificationTests : TestServiceBase<ProjectModificationService>
{
    private readonly RspContext _context;
    private readonly ProjectModificationRepository _repo;

    public GetModificationTests()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
        _context = new RspContext(options);
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
        var response = await Sut.GetModification("PR-1", mod.Id);

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
        var response = await Sut.GetModification("PR1", Guid.NewGuid());

        // Assert
        response.ShouldBeNull();
    }
}