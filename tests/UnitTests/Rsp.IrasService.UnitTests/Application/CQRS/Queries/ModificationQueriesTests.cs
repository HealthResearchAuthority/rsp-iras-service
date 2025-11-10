using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.UnitTests.Application.CQRS.Queries;

public class GetModificationAnswersQueryTests
{
    [Fact]
    public void Properties_Are_Settable()
    {
        var q = new GetModificationAnswersQuery
        {
            ProjectModificationId = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            CategoryId = "C-1"
        };

        q.ProjectRecordId.ShouldBe("PR-1");
        q.CategoryId.ShouldBe("C-1");
        q.ProjectModificationId.ShouldNotBe(Guid.Empty);
    }
}

public class GetModificationChangeAnswersQueryTests
{
    [Fact]
    public void Properties_Are_Settable()
    {
        var q = new GetModificationChangeAnswersQuery
        {
            ProjectModificationChangeId = Guid.NewGuid(),
            ProjectRecordId = "PR-1",
            CategoryId = "C-1"
        };

        q.ProjectRecordId.ShouldBe("PR-1");
        q.CategoryId.ShouldBe("C-1");
        q.ProjectModificationChangeId.ShouldNotBe(Guid.Empty);
    }
}

public class GetModificationChangeQueryTests
{
    [Fact]
    public void Ctor_Sets_Id()
    {
        var id = Guid.NewGuid();
        var q = new GetModificationChangeQuery(id);
        q.ModificationChangeId.ShouldBe(id);
    }
}

public class GetModificationChangesQueryTests
{
    [Fact]
    public void Ctor_Sets_Id()
    {
        var id = Guid.NewGuid();
        var q = new GetModificationChangesQuery(id);
        q.ProjectModificationId.ShouldBe(id);
    }
}

public class GetModificationsBySponsorOrganisationUserIdQueryTests
{
    [Fact]
    public void Ctor_Sets_All_Properties()
    {
        // Arrange
        var sponsorOrganisationUserId = Guid.NewGuid();
        var searchQuery = new SponsorAuthorisationsSearchRequest { SearchTerm = "Test" };
        var pageNumber = 2;
        var pageSize = 50;
        var sortField = "SentToSponsorDate";
        var sortDirection = "desc";

        // Act
        var query = new GetModificationsBySponsorOrganisationUserIdQuery(
            sponsorOrganisationUserId,
            searchQuery,
            pageNumber,
            pageSize,
            sortField,
            sortDirection);

        // Assert
        query.SponsorOrganisationUserId.ShouldBe(sponsorOrganisationUserId);
        query.SearchQuery.ShouldBe(searchQuery);
        query.PageNumber.ShouldBe(pageNumber);
        query.PageSize.ShouldBe(pageSize);
        query.SortField.ShouldBe(sortField);
        query.SortDirection.ShouldBe(sortDirection);
    }
}