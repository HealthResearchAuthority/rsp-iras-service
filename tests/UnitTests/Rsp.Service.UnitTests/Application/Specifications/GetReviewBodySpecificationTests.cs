using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.UnitTests.Application.Specifications;

public class GetReviewBodySpecificationTests
{
    [Theory, AutoData]
    public void GetRespondentApplicationSpecification_ById_ReturnsCorrectSpecification(
        Generator<RegulatoryBody> generator)
    {
        // Arrange
        var applications = generator.Take(5).ToList();

        var spec = new GetReviewBodiesSpecification(1, 10, nameof(ReviewBodyDto.RegulatoryBodyName), "asc", null);

        // Act
        var result = spec
            .Evaluate(applications)
            .FirstOrDefault();

        // Assert
        result.ShouldNotBeNull();
    }
}