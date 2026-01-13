using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.RespondentServiceTests;

/// <summary>
///     Covers the tests for DeleteModificationDocumentResponses method
/// </summary>
public class DeleteModificationDocumentResponsesTests : TestServiceBase<RespondentService>
{
    private readonly RespondentRepository _personnelRepository;
    private readonly IrasContext _context;

    public DeleteModificationDocumentResponsesTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _personnelRepository = new RespondentRepository(_context);
    }

    /// <summary>
    ///     Returns early when input is null
    /// </summary>
    [Fact]
    public async Task ReturnsEarly_WhenNullList()
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);
        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.DeleteModificationDocumentResponses(null);

        // Assert
        var actualCount = await _context.ModificationDocuments.CountAsync();
        actualCount.ShouldBe(0);
    }

    /// <summary>
    ///     Returns early when list is empty
    /// </summary>
    [Fact]
    public async Task ReturnsEarly_WhenEmptyList()
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);
        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.DeleteModificationDocumentResponses(new List<ModificationDocumentDto>());

        // Assert
        var actualCount = await _context.ModificationDocuments.CountAsync();
        actualCount.ShouldBe(0);
    }

    /// <summary>
    ///     Deletes modification documents when valid DTOs provided
    /// </summary>
    [Theory, AutoData]
    public async Task Deletes_ModificationDocuments(List<ModificationDocumentDto> documents)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);
        Sut = Mocker.CreateInstance<RespondentService>();

        // Seed some documents into the context first (simulate existing rows)
        var entities = documents.Select(d => d.Adapt<ModificationDocument>()).ToList();
        await _context.ModificationDocuments.AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        // Act
        await Sut.DeleteModificationDocumentResponses(documents);

        // Assert
        var remainingCount = await _context.ModificationDocuments.CountAsync();
        remainingCount.ShouldBe(0); // all deleted
    }
}