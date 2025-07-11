using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyServiceTests;

public class RemoveReviewBodyUserTests : TestServiceBase<ReviewBodyService>
{
    [Theory, AutoData]
    public async Task RemoveUserFromReviewBody_ShouldReturnDto_WhenUserIsRemoved(Guid reviewBodyId, Guid userId)
    {
        // Arrange
        var reviewBodyUser = new RegulatoryBodyUser
        {
            Id = reviewBodyId,
            UserId = userId,
            DateAdded = DateTime.UtcNow,
        };

        Mocker.GetMock<IRegulatoryBodyRepository>()
            .Setup(x => x.RemoveUserFromRegulatoryBody(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(reviewBodyUser);

        // Act
        var result = await Sut.RemoveUserFromReviewBody(reviewBodyId, userId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ReviewBodyUserDto>();
        result.Id.ShouldBe(reviewBodyId);
        result.UserId.ShouldBe(userId);
    }
}