using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.RespondentServiceTests;

/// <summary>
///     Covers the tests for SaveModificationParticipatingOrganisations method
/// </summary>
public class SaveModificationParticipatingOrganisations : TestServiceBase<RespondentService>
{
    private readonly RespondentRepository _personnelRepository;
    private readonly IrasContext _context;

    public SaveModificationParticipatingOrganisations()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _personnelRepository = new RespondentRepository(_context);
    }

    /// <summary>
    ///     Tests that modification documents are saved
    /// </summary>
    [Theory, AutoData]
    public async Task Persists_ModificationParticipatingOrganisations(List<ModificationParticipatingOrganisationDto> request)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.SaveModificationParticipatingOrganisationResponses(request);

        // Assert
        (await _context.ModificationParticipatingOrganisations.CountAsync()).ShouldBe(request.Count);
    }
}