using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ReviewBodyServiceTests;

public class GetRegulatoryBodiesUsersByUserIdTests : TestServiceBase<ReviewBodyService>
{
    private readonly IrasContext _context;
    private readonly RegulatoryBodyRepository _reviewBodyRepository;

    public GetRegulatoryBodiesUsersByUserIdTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        _reviewBodyRepository = new RegulatoryBodyRepository(_context);
    }

    [Theory]
    [AutoData]
    public async Task Gets_RegulatoryBodyUsers_For_Given_UserId(
        Guid userId,
        Guid reviewBodyId1,
        Guid reviewBodyId2,
        Guid otherUserId,
        Guid otherReviewBodyId)
    {
        // Arrange
        // Seed some data (two records for userId, one for a different user)
        _context.RegulatoryBodiesUsers.AddRange(
            new RegulatoryBodyUser
            {
                Id = reviewBodyId1,
                UserId = userId,
                DateAdded = DateTime.UtcNow
            },
            new RegulatoryBodyUser
            {
                Id = reviewBodyId2,
                UserId = userId,
                DateAdded = DateTime.UtcNow
            },
            new RegulatoryBodyUser
            {
                Id = otherReviewBodyId,
                UserId = otherUserId,
                DateAdded = DateTime.UtcNow
            }
        );
        await _context.SaveChangesAsync();

        Mocker.Use<IRegulatoryBodyRepository>(_reviewBodyRepository);
        Sut = Mocker.CreateInstance<ReviewBodyService>();

        // Act
        var result = await Sut.GetRegulatoryBodiesUsersByUserId(userId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<ReviewBodyUserDto>>();
        result.Count.ShouldBe(2);
        result.ShouldAllBe(x => x.UserId == userId);
        result.Select(x => x.Id).ShouldContain(reviewBodyId1);
        result.Select(x => x.Id).ShouldContain(reviewBodyId2);
    }
}