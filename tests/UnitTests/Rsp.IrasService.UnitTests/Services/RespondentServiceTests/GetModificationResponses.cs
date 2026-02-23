using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.RespondentServiceTests;

/// <summary>
///     Covers the tests for GetModificationResponses(modificationId, projectRecordId)
/// </summary>
public class GetModificationResponses : TestServiceBase<RespondentService>
{
    private readonly RespondentRepository _personnelRepository;
    private readonly IrasContext _context;

    public GetModificationResponses()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        var featureManager = new Mock<IFeatureManager>();
        _personnelRepository = new RespondentRepository(_context, featureManager.Object);
    }

    [Theory, AutoData]
    public async Task Returns_Answers_For_Modification_And_Project(Guid modificationId, string projectRecordId)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_personnelRepository);
        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        var result = await Sut.GetModificationResponses(modificationId, projectRecordId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IEnumerable<RespondentAnswerDto>>();
    }
}