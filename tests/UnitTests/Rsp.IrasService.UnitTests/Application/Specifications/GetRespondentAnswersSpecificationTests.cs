using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetRespondentAnswersSpecificationTests
{
    /// <summary>
    ///     Tests that GetRespondentAnswersSpecification filters by ApplicationId and CategoryId correctly.
    /// </summary>
    [Theory, AutoData]
    public void GetRespondentAnswersSpecification_ByApplicationIdAndCategoryId_ReturnsCorrectSpecification(
        Generator<ProjectApplicationRespondentAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = respondentAnswers[0].ProjectApplicationId;
        var categoryId = respondentAnswers[0].Category;

        var spec = new GetRespondentAnswersSpecification(applicationId, categoryId);

        // Act
        var result = spec
            .Evaluate(respondentAnswers)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldAllBe(answer => answer.ProjectApplicationId == applicationId && answer.Category == categoryId);
    }

    /// <summary>
    ///     Tests that GetRespondentAnswersSpecification filters by ApplicationId correctly.
    /// </summary>
    [Theory, AutoData]
    public void GetRespondentAnswersSpecification_ByApplicationId_ReturnsCorrectSpecification(
        Generator<ProjectApplicationRespondentAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = respondentAnswers[0].ProjectApplicationId;

        var spec = new GetRespondentAnswersSpecification(applicationId);

        // Act
        var result = spec
            .Evaluate(respondentAnswers)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldAllBe(answer => answer.ProjectApplicationId == applicationId);
    }

    /// <summary>
    ///     Tests that GetRespondentAnswersSpecification filters correctly based on different record counts.
    /// </summary>
    [Theory, InlineAutoData(3), InlineAutoData(0)]
    public void GetRespondentAnswersSpecification_ByRecords_ReturnsCorrectCount(int records,
        Generator<ProjectApplicationRespondentAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = respondentAnswers[0].ProjectApplicationId;

        // Ensure all test data has the same ApplicationId
        foreach (var answer in respondentAnswers) answer.ProjectApplicationId = applicationId;

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