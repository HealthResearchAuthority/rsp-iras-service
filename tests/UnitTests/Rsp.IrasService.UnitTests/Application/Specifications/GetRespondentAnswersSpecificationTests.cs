using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.UnitTests.Fixtures;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetRespondentAnswersSpecificationTests
{
    /// <summary>
    ///     Tests that GetRespondentAnswersSpecification filters by ApplicationId and CategoryId correctly.
    /// </summary>
    [Theory, NoRecursionAutoData]
    public void GetRespondentAnswersSpecification_ByApplicationIdAndCategoryId_ReturnsCorrectSpecification(
        Generator<ProjectRecordAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = respondentAnswers[0].ProjectRecordId;
        var categoryId = respondentAnswers[0].Category;

        var spec = new GetRespondentAnswersSpecification(applicationId, categoryId);

        // Act
        var result = spec
            .Evaluate(respondentAnswers)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldAllBe(answer => answer.ProjectRecordId == applicationId && answer.Category == categoryId);
    }

    /// <summary>
    ///     Tests that GetRespondentAnswersSpecification filters by ApplicationId correctly.
    /// </summary>
    [Theory, NoRecursionAutoData]
    public void GetRespondentAnswersSpecification_ByApplicationId_ReturnsCorrectSpecification(
        Generator<ProjectRecordAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = respondentAnswers[0].ProjectRecordId;

        var spec = new GetRespondentAnswersSpecification(applicationId);

        // Act
        var result = spec
            .Evaluate(respondentAnswers)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldAllBe(answer => answer.ProjectRecordId == applicationId);
    }

    /// <summary>
    ///     Tests that GetRespondentAnswersSpecification filters correctly based on different record counts.
    /// </summary>
    [Theory, NoRecursionInlineAutoData(3), NoRecursionInlineAutoData(0)]
    public void GetRespondentAnswersSpecification_ByRecords_ReturnsCorrectCount(int records,
        Generator<ProjectRecordAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = respondentAnswers[0].ProjectRecordId;

        // Ensure all test data has the same ApplicationId
        foreach (var answer in respondentAnswers) answer.ProjectRecordId = applicationId;

        var spec = new GetRespondentAnswersSpecification(applicationId);

        // Act
        var result = spec
            .Evaluate(respondentAnswers)
            .Take(records > 0 ? records : respondentAnswers.Count) // Take only if records > 0
            .ToList();

        // Assert
        var expectedCount = records > 0 ? records : respondentAnswers.Count;
        result.ShouldNotBeNull();
        result.Count.ShouldBe(expectedCount);
    }
}