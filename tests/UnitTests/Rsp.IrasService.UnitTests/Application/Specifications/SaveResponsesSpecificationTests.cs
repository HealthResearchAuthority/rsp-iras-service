using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class SaveResponsesSpecificationTests
{
    /// <summary>
    ///     Tests that SaveResponsesSpecification filters by ApplicationId and RespondentId correctly.
    /// </summary>
    [Theory, AutoData]
    public void SaveResponsesSpecification_ByApplicationIdAndRespondentId_ReturnsCorrectSpecification(
        Generator<RespondentAnswer> generator)
    {
        // Arrange
        var respondentAnswers = generator.Take(5).ToList();
        var applicationId = respondentAnswers[0].ApplicationId;
        var respondentId = respondentAnswers[0].RespondentId;

        var spec = new SaveResponsesSpecification(applicationId, respondentId);

        // Act
        var result = spec
            .Evaluate(respondentAnswers)
            .ToList();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldAllBe(answer => answer.ApplicationId == applicationId && answer.RespondentId == respondentId);
    }

    /// <summary>
    ///     Tests that SaveResponsesSpecification returns an empty list when no matching responses exist.
    /// </summary>
    [Theory, AutoData]
    public void SaveResponsesSpecification_NoMatches_ReturnsEmptyList(Generator<RespondentAnswer> generator)
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