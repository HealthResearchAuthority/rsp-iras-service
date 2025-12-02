using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;

namespace Rsp.IrasService.UnitTests.Infrastructure.ProjectRecordRepositoryTests;

public class ProjectRecordRepositoryTests
{
    private readonly IrasContext _context;
    private readonly ProjectRecordRepository _applicationRepository;
    private const string projectTitleQuestionId = "IQA0002";
    private const string applicationsTitleCategory = "project record v1";
    private readonly string respondentId = "1";

    public ProjectRecordRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectRecordRepository(_context);
        SeedData();
    }

    private void SeedData()
    {
        var projectRecords = new List<ProjectRecord>
        {
            new ProjectRecord { Id = "1", ShortProjectTitle = "", CreatedDate = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc), Status = "Open", IrasId = 1, UserId = respondentId, CreatedBy = "A", FullProjectTitle = "", UpdatedBy = "A" },
            new ProjectRecord { Id = "2", ShortProjectTitle = "", CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc), Status = "Closed", IrasId = 2, UserId = respondentId, CreatedBy = "A", FullProjectTitle = "", UpdatedBy = "A" },
            new ProjectRecord { Id = "3", ShortProjectTitle = "", CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), Status = "Pending", IrasId = 3, UserId = respondentId, CreatedBy = "A", FullProjectTitle = "", UpdatedBy = "A" }
        };

        var projectRecordAnswers = new List<ProjectRecordAnswer>
        {
            new ProjectRecordAnswer { ProjectRecordId = "1", Response = "A", UserId = respondentId, QuestionId = projectTitleQuestionId, Category = applicationsTitleCategory, Section = "", VersionId = "1" },
            new ProjectRecordAnswer { ProjectRecordId = "2", Response = "B", UserId = respondentId, QuestionId = projectTitleQuestionId, Category = applicationsTitleCategory, Section = "", VersionId = "1" },
            new ProjectRecordAnswer { ProjectRecordId = "3", Response = "C", UserId = respondentId, QuestionId = projectTitleQuestionId, Category = applicationsTitleCategory, Section = "", VersionId = "1"}
        };

        _context.ProjectRecords.AddRange(projectRecords);
        _context.ProjectRecordAnswers.AddRange(projectRecordAnswers);
        _context.SaveChanges();
    }

    [Theory]
    [InlineData("title", "asc", new[] { "1", "2", "3" })]
    [InlineData("title", "desc", new[] { "3", "2", "1" })]
    [InlineData("createddate", "asc", new[] { "1", "2", "3" })]
    [InlineData("createddate", "desc", new[] { "3", "2", "1" })]
    [InlineData("status", "asc", new[] { "2", "1", "3" })]
    [InlineData("status", "desc", new[] { "3", "1", "2" })]
    [InlineData("irasid", "asc", new[] { "1", "2", "3" })]
    [InlineData("irasid", "desc", new[] { "3", "2", "1" })]
    [InlineData("other", "asc", new[] { "3", "2", "1" })]
    public async Task GetPaginatedProjectRecords_ReturnsExpectedResults_ForVariousSorts(
        string sortField,
        string sortDirection,
        string[] expectedIds)
    {
        var projectSpec = new GetRespondentApplicationSpecification(respondentId);
        var projectTitlesSpec = new GetRespondentProjectsTitlesSpecification(respondentId, projectTitleQuestionId);

        var (results, totalCount) = await _applicationRepository.GetPaginatedProjectRecords(
            projectSpec,
            projectTitlesSpec,
            pageIndex: 1,
            pageSize: null,
            sortField: sortField,
            sortDirection: sortDirection
        );

        Assert.Equal(3, totalCount);
        Assert.Equal(expectedIds, results.Select(r => r.Id).ToArray());
    }

    [Theory]
    [InlineData(null, 1, new[] { "A", "B", "C" })]
    [InlineData(1, 1, new[] { "A" })]
    [InlineData(2, 1, new[] { "A", "B" })]
    [InlineData(2, 2, new[] { "C" })]
    public async Task GetPaginatedProjectRecords_PaginatesCorrectly(
        int? pageSize,
        int pageIndex,
        string[] expectedTitles)
    {
        var projectSpec = new GetRespondentApplicationSpecification(respondentId);
        var projectTitlesSpec = new GetRespondentProjectsTitlesSpecification(respondentId, projectTitleQuestionId);

        var (results, totalCount) = await _applicationRepository.GetPaginatedProjectRecords(
            projectSpec,
            projectTitlesSpec,
            pageIndex: pageIndex,
            pageSize: pageSize,
            sortField: "title",
            sortDirection: "asc"
        );

        Assert.Equal(3, totalCount);
        Assert.Equal(expectedTitles, results.Select(r => r.ShortProjectTitle).ToArray());
    }
}