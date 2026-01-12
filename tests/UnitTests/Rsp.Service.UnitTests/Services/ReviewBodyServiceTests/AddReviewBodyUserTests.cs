using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ReviewBodyServiceTests;

public class AddReviewBodyUserTests : TestServiceBase<ReviewBodyService>

{
    private readonly RspContext _context;
    private readonly RegulatoryBodyRepository _reviewBodyRepository;

    public AddReviewBodyUserTests()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
        _reviewBodyRepository = new RegulatoryBodyRepository(_context);
    }

    [Theory]
    [AutoData]
    public async Task Creates_ReviewBody_Correctly(ReviewBodyUserDto reviewBodyUserDto)
    {
        // Arrange
        Mocker.Use<IRegulatoryBodyRepository>(_reviewBodyRepository);
        Sut = Mocker.CreateInstance<ReviewBodyService>();

        // Act
        var result = await Sut.AddUserToReviewBody(reviewBodyUserDto);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ReviewBodyUserDto>();
    }
}