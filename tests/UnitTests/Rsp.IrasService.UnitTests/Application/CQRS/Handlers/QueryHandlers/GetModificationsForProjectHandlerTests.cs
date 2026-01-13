using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.UnitTests.Application.CQRS.Handlers.QueryHandlers;

public class GetModificationsForProjectHandlerTests : TestServiceBase<GetModificationsForProjectHandler>
{
    [Fact]
    public async Task Handle_ShouldReturnExpectedModificationSearchResponse()
    {
        // Arrange
        var projectRecordId = "PR-001";
        var searchQuery = new ModificationSearchRequest
        {
            IrasId = "IRAS-123",
            ChiefInvestigatorName = "Dr. Smith"
        };

        var expectedModifications = new List<ModificationDto>
        {
            new() { ModificationId = "MOD-001", ChiefInvestigator = "Dr. Smith" }
        };

        var expectedResponse = new ModificationSearchResponse
        {
            Modifications = expectedModifications,
            TotalCount = expectedModifications.Count,
            ProjectRecordId = projectRecordId
        };

        var query = new GetModificationsForProjectQuery(
            projectRecordId,
            searchQuery,
            pageNumber: 1,
            pageSize: 10,
            sortField: "CreatedAt",
            sortDirection: "asc"
        );

        var modificationService = Mocker.GetMock<IProjectModificationService>();

        modificationService
            .Setup
            (
                service => service.GetModificationsForProject
                (
                    query.ProjectRecordId,
                    query.SearchQuery,
                    query.PageNumber,
                    query.PageSize,
                    query.SortField,
                    query.SortDirection
                )
            )
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await Sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.ProjectRecordId.ShouldBe(projectRecordId);
        result.Modifications.ShouldHaveSingleItem();
        result.Modifications.First().ModificationId.ShouldBe("MOD-001");

        modificationService.Verify
        (
            service =>
                service.GetModificationsForProject
                (
                    query.ProjectRecordId,
                    query.SearchQuery,
                    query.PageNumber,
                    query.PageSize,
                    query.SortField,
                    query.SortDirection
                ),
            Times.Once
        );
    }
}