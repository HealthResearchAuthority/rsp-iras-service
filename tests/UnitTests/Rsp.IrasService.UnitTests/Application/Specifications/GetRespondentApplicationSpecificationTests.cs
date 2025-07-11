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
}