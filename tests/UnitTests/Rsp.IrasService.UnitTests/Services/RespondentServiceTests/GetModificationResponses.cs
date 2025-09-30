using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.RespondentServiceTests;

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
        _personnelRepository = new RespondentRepository(_context);
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