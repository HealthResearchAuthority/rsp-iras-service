using Microsoft.AspNetCore.Mvc;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.Service.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class GetModificationControllerTests : TestServiceBase<ProjectModificationsController>
{
    [Fact]
    public async Task GetModification_Returns_BadRequest_When_Id_Missing()
    {
        // Act
        var result = await Sut.GetModification("", Guid.Empty);

        // Assert
        var badRequest = result.Result.ShouldBeOfType<BadRequestObjectResult>();
        badRequest.Value.ShouldBeOfType<string>();
    }

    [Fact]
    public async Task GetModification_Sends_Query_And_Returns_Response()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mediator = Mocker.GetMock<IMediator>();
        var expected = new ModificationResponse
        {
            Id = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            ModificationIdentifier = "12345/1",
            Status = "Draft",
            CreatedBy = "u",
            UpdatedBy = "u",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        mediator
            .Setup(x => x.Send(It.IsAny<GetModificationQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var res = await Sut.GetModification("PR-1", id);

        // Assert
        res.Value.ShouldBe(expected);
        mediator.Verify(m => m.Send(It.Is<GetModificationQuery>(q => q.ProjectModificationId == id), It.IsAny<CancellationToken>()), Times.Once);
    }
}