using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ReviewBodyServiceTests;

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