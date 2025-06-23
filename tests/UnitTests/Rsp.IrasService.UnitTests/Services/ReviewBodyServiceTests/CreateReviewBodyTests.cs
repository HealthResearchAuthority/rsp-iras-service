using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyServiceTests;

public class CreateReviewBodyTests : TestServiceBase<ReviewBodyService>

{
    private readonly IrasContext _context;
    private readonly ReviewBodyRepository _reviewBodyRepository;

    public CreateReviewBodyTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _reviewBodyRepository = new ReviewBodyRepository(_context);
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