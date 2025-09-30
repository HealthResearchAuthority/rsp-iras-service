using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.RespondentServiceTests;

/// <summary>
///     Covers the tests for SaveModificationChangeAnswers method
/// </summary>
public class SaveModificationChangeAnswers : TestServiceBase<RespondentService>
{
    private readonly RespondentRepository _personnelRepository;
    private readonly IrasContext _context;

    public SaveModificationChangeAnswers()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _personnelRepository = new RespondentRepository(_context);
    }

    /// <summary>
    ///     Tests that modification answers are saved
    /// </summary>
    [Theory, AutoData]
    public async Task Persists_ModificationAnswers(ModificationChangeAnswersRequest request)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.SaveModificationChangeAnswers(request);

        // Assert
        (await _context.ProjectModificationChangeAnswers.CountAsync()).ShouldBe(request.ModificationChangeAnswers.Count);
    }
}