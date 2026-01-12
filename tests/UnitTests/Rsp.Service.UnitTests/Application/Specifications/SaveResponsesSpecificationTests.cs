using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;
using Rsp.Service.UnitTests.Fixtures;

namespace Rsp.Service.UnitTests.Application.Specifications;

public class SaveResponsesSpecificationTests
{
    /// <summary>
    ///     Tests that SaveResponsesSpecification filters by ApplicationId and RespondentId correctly.
    /// </summary>
    [Theory, NoRecursionAutoData]
    public void SaveResponsesSpecification_ByApplicationIdAndRespondentId_ReturnsCorrectSpecification(
        Generator<ProjectRecordAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = respondentAnswers[0].ProjectRecordId;
        var respondentId = respondentAnswers[0].UserId;

        var spec = new SaveResponsesSpecification(applicationId, respondentId);

        // Act
        var result = spec
            .Evaluate(respondentAnswers)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldAllBe(answer => answer.ProjectRecordId == applicationId && answer.UserId == respondentId);
    }

    /// <summary>
    ///     Tests that SaveResponsesSpecification returns an empty list when no matching responses exist.
    /// </summary>
    [Theory, NoRecursionAutoData]
    public void SaveResponsesSpecification_NoMatches_ReturnsEmptyList(Generator<ProjectRecordAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = "NonExistentApplication";
        var respondentId = "NonExistentRespondent";

        var spec = new SaveResponsesSpecification(applicationId, respondentId);

        // Act
        var result = spec
            .Evaluate(respondentAnswers)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }
}