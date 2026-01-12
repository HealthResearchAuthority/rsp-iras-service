using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.RespondentServiceTests;

/// <summary>
///     Covers the tests for SaveModificationAnswers method
/// </summary>
public class SaveModificationAnswers : TestServiceBase<RespondentService>
{
    private readonly RespondentRepository _personnelRepository;
    private readonly RspContext _context;

    public SaveModificationAnswers()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
        _personnelRepository = new RespondentRepository(_context);
    }

    /// <summary>
    ///     Tests that modification answers are saved
    /// </summary>
    [Theory, AutoData]
    public async Task Persists_ModificationAnswers(ModificationAnswersRequest request)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.SaveModificationAnswers(request);

        // Assert
        (await _context.ProjectModificationAnswers.CountAsync()).ShouldBe(request.ModificationAnswers.Count);
    }
}
