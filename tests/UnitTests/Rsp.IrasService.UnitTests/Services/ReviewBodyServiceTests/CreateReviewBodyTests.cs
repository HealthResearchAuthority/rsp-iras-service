using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ReviewBodyServiceTests;

public class CreateReviewBodyTests : TestServiceBase<ReviewBodyService>

{
    private readonly IrasContext _context;
    private readonly RegulatoryBodyRepository _reviewBodyRepository;

    public CreateReviewBodyTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _reviewBodyRepository = new RegulatoryBodyRepository(_context);
    }

    [Theory]
    [AutoData]
    public async Task Creates_ReviewBody_Correctly(ReviewBodyDto reviewBodyDto)
    {
        // Arrange
        Mocker.Use<IRegulatoryBodyRepository>(_reviewBodyRepository);
        Sut = Mocker.CreateInstance<ReviewBodyService>();

        // Act
        var result = await Sut.CreateReviewBody(reviewBodyDto);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ReviewBodyDto>();
    }
}