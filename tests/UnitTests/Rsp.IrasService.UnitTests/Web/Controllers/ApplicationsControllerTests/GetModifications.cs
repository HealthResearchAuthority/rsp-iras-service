using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ApplicationsControllerTests;

public class GetModifications : TestServiceBase
{
    private readonly ApplicationsController _controller;

    public GetModifications()
    {
        _controller = Mocker.CreateInstance<ApplicationsController>();
    }

    [Theory]
    [AutoData]
    public async Task GetModifications_ShouldReturnOk_WhenModificationsExist
    (
        ModificationSearchRequest searchQuery,
        ModificationResponse mockResponse,
        int pageNumber,
        int pageSize,
        string sortField,
        string sortDirection
    )
    {
        // Arrange
        var query = new GetModificationsQuery(searchQuery, pageNumber, pageSize, sortField, sortDirection);

        var mockMediator = Mocker.GetMock<IMediator>();
        mockMediator
            .Setup(m => m.Send(It.Is<GetModificationsQuery>(q =>
                q.SearchQuery.Equals(searchQuery) &&
                q.PageNumber == pageNumber &&
                q.PageSize == pageSize), default))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _controller.GetModifications(searchQuery, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.Value.ShouldBe(mockResponse);
    }
}