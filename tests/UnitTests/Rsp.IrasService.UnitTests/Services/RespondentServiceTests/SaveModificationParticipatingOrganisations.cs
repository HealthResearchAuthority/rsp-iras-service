using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.RespondentServiceTests;

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
        var featureManager = new Mock<IFeatureManager>();
        _personnelRepository = new RespondentRepository(_context, featureManager.Object);
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