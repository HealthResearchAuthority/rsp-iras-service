using System.Globalization;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.UnitTests.Fixtures;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetRespondentApplicationSpecificationTests
{
    /// <summary>
    ///     Tests that GetRespondentApplicationSpecification filters by RespondentId and ApplicationId correctly.
    /// </summary>
    [Theory, NoRecursionAutoData]
    public void GetRespondentApplicationSpecification_ById_ReturnsCorrectSpecification(
        Generator<ProjectRecord> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();
        var respondentId = applications[0].UserId;
        var applicationId = applications[0].Id;

        var spec = new GetRespondentApplicationSpecification(respondentId, id: applicationId);

        // Act
        var result = spec
            .Evaluate(applications)
            .SingleOrDefault();

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(applicationId);
        result.UserId.ShouldBe(respondentId);
    }

    /// <summary>
    ///     Tests that GetRespondentApplicationSpecification filters by RespondentId correctly.
    /// </summary>
    [Theory, NoRecursionAutoData]
    public void GetRespondentApplicationSpecification_ByRespondentId_ReturnsCorrectSpecification(
        Generator<ProjectRecord> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();
        var respondentId = applications[0].UserId;

        var spec = new GetRespondentApplicationSpecification(respondentId);

        // Act
        var result = spec
            .Evaluate(applications)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldAllBe(application => application.UserId == respondentId);
    }

    /// <summary>
    ///     Tests that GetRespondentApplicationSpecification filters by records count correctly.
    /// </summary>
    [Theory, NoRecursionInlineAutoData(2), NoRecursionInlineAutoData(0)]
    public void GetRespondentApplicationSpecification_ByRecords_ReturnsCorrectCount(int records,
        Generator<ProjectRecord> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();
        var respondentId = applications[0].UserId;

        // Ensure all test data has the same RespondentId
        foreach (var application in applications) application.UserId = respondentId;

        var spec = new GetRespondentApplicationSpecification(respondentId, records: records);

        // Act
        var result = spec
            .Evaluate(applications)
            .Take(records > 0 ? records : applications.Count) // Apply Take() only when records > 0
            .ToList();

        // Assert
        var expectedCount = records > 0 ? records : applications.Count;
        result.ShouldNotBeNull();
        result.Count.ShouldBe(expectedCount);
    }

    [Theory]
    [InlineData("Initiative", null, new[] { "2" })]
    [InlineData("123", null, new[] { "1", "2", "3", "5" })]
    [InlineData(null, "Approved", new[] { "1", "3" })]
    [InlineData("ABC", "Rejected", new string[] { })]
    [InlineData("123 Test", null, new[] { "5" })]
    public void GetRespondentApplicationSpecification_ShouldFilterCorrectly(
    string? searchTitleTerm,
    string? status,
    string[] expectedIds)
    {
        // Arrange
        var respondentId = "R-123";

        var applications = new List<ProjectRecord>
    {
        new() { Id = "1", UserId = respondentId, ShortProjectTitle = "ABC Project", IrasId = 12334, Status = "Approved" },
        new() { Id = "2", UserId = respondentId, ShortProjectTitle = "XYZ Initiative", IrasId = 12335, Status = "Rejected" },
        new() { Id = "3", UserId = respondentId, ShortProjectTitle = "123 Study", IrasId = 12336, Status = "Approved,Pending" },
        new() { Id = "4", UserId = "Other", ShortProjectTitle = "ABC Project", IrasId = 12337, Status = "Approved" },
        new() { Id = "5", UserId = respondentId, ShortProjectTitle = "123 ABC Test", IrasId = 12339, Status = "Pending" }
    };

        var searchQuery = new ApplicationSearchRequest
        {
            SearchTitleTerm = searchTitleTerm,
            Status = status?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>()
        };

        var spec = new GetRespondentApplicationSpecification(respondentId, searchQuery);

        // Act
        var result = spec.Evaluate(applications).ToList();

        // Assert
        result.ShouldNotBeNull();
        result.Select(r => r.Id).ShouldBe(expectedIds);
    }

    [Theory]
    [InlineData("2023-01-01", "2023-03-03", new[] { "2" })]
    [InlineData("2022-01-01", "2022-12-31", new string[] { })]
    [InlineData("2023-05-01", null, new[] { "1", "3" })]
    [InlineData(null, "2023-05-31", new[] { "1", "2" })]
    [InlineData(null, null, new[] { "1", "2", "3" })]
    public void GetRespondentApplicationSpecification_ShouldFilterByDateRange(
    string? fromDateStr,
    string? toDateStr,
    string[] expectedIds)
    {
        // Arrange
        var respondentId = "R-123";
        CultureInfo ukCulture = new CultureInfo("en-GB");

        var applications = new List<ProjectRecord>
        {
            new() { Id = "1", UserId = respondentId, CreatedDate = new DateTime(2023, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
            new() { Id = "2", UserId = respondentId, CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new() { Id = "3", UserId = respondentId, CreatedDate = new DateTime(2023, 12, 31, 0, 0, 0, DateTimeKind.Utc) },
            new() { Id = "4", UserId = "Other", CreatedDate = new DateTime(2023, 5, 10, 0, 0, 0, DateTimeKind.Utc) }
        };

        var searchQuery = new ApplicationSearchRequest
        {
            FromDate = fromDateStr != null ? DateTime.Parse(fromDateStr, ukCulture) : (DateTime?)null,
            ToDate = toDateStr != null ? DateTime.Parse(toDateStr, ukCulture) : (DateTime?)null
        };

        var spec = new GetRespondentApplicationSpecification(respondentId, searchQuery);

        // Act
        var result = spec.Evaluate(applications).ToList();

        // Assert
        result.ShouldNotBeNull();
        result.Select(r => r.Id).ShouldBe(expectedIds);
    }

    [Fact]
    public void GetRespondentApplicationSpecification_WithSearchQuery_NoMatch_ReturnsEmpty()
    {
        // Arrange
        var respondentId = "R-123";
        var applications = new List<ProjectRecord>
        {
            new() { Id = "1", UserId = respondentId, ShortProjectTitle = "ABC Project", FullProjectTitle = "Phase Alpha" }
        };

        ApplicationSearchRequest searchQuery = new ApplicationSearchRequest();
        searchQuery.SearchTitleTerm = "Initiative";

        var spec = new GetRespondentApplicationSpecification(respondentId, searchQuery);

        // Act
        var result = spec.Evaluate(applications).ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }
}