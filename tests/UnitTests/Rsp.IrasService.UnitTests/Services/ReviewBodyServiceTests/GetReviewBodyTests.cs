using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyServiceTests;

public class GetReviewBodyTests : TestServiceBase<ReviewBodyService>
{
    private readonly IrasContext _context;
    private readonly RegulatoryBodyRepository _reviewBodyRepository;

    public GetReviewBodyTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        _reviewBodyRepository = new RegulatoryBodyRepository(_context);
    }

    [Theory]
    [AutoData]
    public async Task Gets_ReviewBody_Correctly(ReviewBodyDto reviewBodyDto)
    {
        // Arrange
        Mocker.Use<IRegulatoryBodyRepository>(_reviewBodyRepository);
        Sut = Mocker.CreateInstance<ReviewBodyService>();

        // Seed via the service to ensure an entity exists
        var created = await Sut.CreateReviewBody(reviewBodyDto);

        // Act
        var result = await Sut.GetReviewBody(created.Id);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ReviewBodyDto>();
        result.Id.ShouldBe(created.Id);
    }
}