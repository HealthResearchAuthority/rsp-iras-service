using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.CQRS.Handlers.QueryHandlers;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationHandlerTests : TestServiceBase<GetModificationHandler>
{
    [Theory, AutoData]
    public async Task Handle_Delegates_To_Service_And_Returns_Response(ModificationResponse response)
    {
        // Arrange
        var service = Mocker.GetMock<IProjectModificationService>();

        var id = Guid.NewGuid().ToString();
        var query = new GetModificationQuery(id);

        response.Id = Guid.Parse(id);

        service
            .Setup(s => s.GetModification(id))
            .ReturnsAsync(response);

        // Act
        var result = await Sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(response);
        service.Verify(s => s.GetModification(id), Times.Once);
    }

    [Fact]
    public async Task Handle_Returns_Null_When_Service_Returns_Null()
    {
        // Arrange
        var svc = new Mock<IProjectModificationService>();
        var handler = new GetModificationHandler(svc.Object);
        var id = Guid.NewGuid().ToString();
        var query = new GetModificationQuery(id);

        svc.Setup(s => s.GetModification(id)).ReturnsAsync((ModificationResponse?)null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeNull();
        svc.Verify(s => s.GetModification(id), Times.Once);
    }
}