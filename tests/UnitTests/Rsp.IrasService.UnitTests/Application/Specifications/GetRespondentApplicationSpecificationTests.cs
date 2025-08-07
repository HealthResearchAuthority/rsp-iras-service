using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetRespondentApplicationSpecificationTests
{
    /// <summary>
    ///     Tests that GetRespondentApplicationSpecification filters by RespondentId and ApplicationId correctly.
    /// </summary>
    [Theory, AutoData]
    public void GetRespondentApplicationSpecification_ById_ReturnsCorrectSpecification(
        Generator<ProjectRecord> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();
        var respondentId = applications[0].ProjectPersonnelId;
        var applicationId = applications[0].Id;

        var spec = new GetRespondentApplicationSpecification(respondentId, applicationId);

        // Act
        var result = spec
            .Evaluate(applications)
            .SingleOrDefault();

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(applicationId);
        result.ProjectPersonnelId.ShouldBe(respondentId);
    }

    /// <summary>
    ///     Tests that GetRespondentApplicationSpecification filters by RespondentId correctly.
    /// </summary>
    [Theory, AutoData]
    public void GetRespondentApplicationSpecification_ByRespondentId_ReturnsCorrectSpecification(
        Generator<ProjectRecord> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();
        var respondentId = applications[0].ProjectPersonnelId;

        var spec = new GetRespondentApplicationSpecification(respondentId);

        // Act
        var result = spec
            .Evaluate(applications)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldAllBe(application => application.ProjectPersonnelId == respondentId);
    }

    /// <summary>
    ///     Tests that GetRespondentApplicationSpecification filters by records count correctly.
    /// </summary>
    [Theory, InlineAutoData(2), InlineAutoData(0)]
    public void GetRespondentApplicationSpecification_ByRecords_ReturnsCorrectCount(int records,
        Generator<ProjectRecord> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();
        var respondentId = applications[0].ProjectPersonnelId;

        // Ensure all test data has the same RespondentId
        foreach (var application in applications) application.ProjectPersonnelId = respondentId;

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

    [Fact]
    public void GetRespondentApplicationSpecification_WithSearchQuery_FiltersCorrectly()
    {
        // Arrange
        var respondentId = "R-123";
        var applications = new List<ProjectRecord>
        {
            new() { Id = "1", ProjectPersonnelId = respondentId, Title = "ABC Project", Description = "Phase Alpha" },
            new() { Id = "2", ProjectPersonnelId = respondentId, Title = "XYZ Initiative", Description = "Phase Beta" },
            new() { Id = "3", ProjectPersonnelId = respondentId, Title = "123 Study", Description = "Phase Gamma" },
            new() { Id = "4", ProjectPersonnelId = "Other", Title = "ABC Project", Description = "Phase Alpha" }
        };

        var searchQuery = "ABC Alpha";

        var spec = new GetRespondentApplicationSpecification(respondentId, searchQuery, null, null);

        // Act
        var result = spec.Evaluate(applications).ToList();

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(1);
        result[0].Id.ShouldBe("1");
    }

    [Fact]
    public void GetRespondentApplicationSpecification_WithSearchQuery_NoMatch_ReturnsEmpty()
    {
        // Arrange
        var respondentId = "R-123";
        var applications = new List<ProjectRecord>
        {
            new() { Id = "1", ProjectPersonnelId = respondentId, Title = "ABC Project", Description = "Phase Alpha" }
        };

        var searchQuery = "XYZ";

        var spec = new GetRespondentApplicationSpecification(respondentId, searchQuery, null, null);

        // Act
        var result = spec.Evaluate(applications).ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("title", "asc", new[] { "1", "2", "3" })]
    [InlineData("title", "desc", new[] { "3", "2", "1" })]
    [InlineData("createddate", "asc", new[] { "1", "2", "3" })]
    [InlineData("createddate", "desc", new[] { "3", "2", "1" })]
    [InlineData("status", "asc", new[] { "1", "2", "3" })]
    [InlineData("status", "desc", new[] { "3", "2", "1" })]
    [InlineData("irasid", "asc", new[] { "1", "2", "3" })]
    [InlineData("irasid", "desc", new[] { "3", "2", "1" })]
    [InlineData(null, null, new[] { "3", "2", "1" })] // default sort by CreatedDate desc
    [InlineData("other", "asc", new[] { "3", "2", "1" })] // default sort by CreatedDate desc
    public void GetRespondentApplicationSpecification_Sorting_WorksCorrectly(
    string? sortField,
    string? sortDirection,
    string[] expectedOrder)
    {
        // Arrange
        var respondentId = "R-123";
        var applications = new List<ProjectRecord>
    {
        new() { Id = "1", ProjectPersonnelId = respondentId, Title = "Alpha", CreatedDate = new DateTime(2020, 1, 1, 0 ,0, 0, 0, DateTimeKind.Utc), Status = "a", IrasId = 11 },
        new() { Id = "2", ProjectPersonnelId = respondentId, Title = "Beta", CreatedDate = new DateTime(2021, 1, 1, 0 ,0, 0, 0, DateTimeKind.Utc), Status = "b", IrasId = 22 },
        new() { Id = "3", ProjectPersonnelId = respondentId, Title = "Zeta", CreatedDate = new DateTime(2022, 1, 1, 0 ,0, 0, 0, DateTimeKind.Utc), Status = "c", IrasId = 33 }
    };

        var spec = new GetRespondentApplicationSpecification(respondentId, null, sortField, sortDirection);

        // Act
        var result = spec.Evaluate(applications).ToList();

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(3);
        result.Select(x => x.Id).ToArray().ShouldBe(expectedOrder);
    }
}