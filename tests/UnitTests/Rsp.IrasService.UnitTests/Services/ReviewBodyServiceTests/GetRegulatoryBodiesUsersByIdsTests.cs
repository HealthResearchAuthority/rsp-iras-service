using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyServiceTests;

public class GetRegulatoryBodiesUsersByIdsTests : TestServiceBase<ReviewBodyService>
{
    private readonly IrasContext _context;
    private readonly RegulatoryBodyRepository _reviewBodyRepository;

    public GetRegulatoryBodiesUsersByIdsTests()
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

        var ids = new List<Guid> { reviewBodyId1, reviewBodyId2 };

        // Act
        var result = await Sut.GetRegulatoryBodiesUsersByIds(ids);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<ReviewBodyUserDto>>();
        result.Count.ShouldBe(2);
        result.Select(x => x.Id).ShouldContain(reviewBodyId1);
        result.Select(x => x.Id).ShouldContain(reviewBodyId2);
        result.ShouldAllBe(x => ids.Contains(x.Id));
    }
}