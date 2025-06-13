using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetApplicationSpecificationTests
{
    [Theory, AutoData]
    public void GetApplicationSpecification_ById_ReturnsCorrectSpecification(Generator<ProjectApplication> generator)
    {
        // Arrange
        var applications = generator.Take(3).ToList();

        var spec = new GetApplicationSpecification(id: applications[0].ProjectApplicationId);

        // Act
        var result = spec
            .Evaluate(applications)
            .SingleOrDefault();

        // Assert
        result.ShouldNotBeNull();
        result.ProjectApplicationId.ShouldBe(applications[0].ProjectApplicationId);
    }

    [Theory, InlineAutoData(5, 5), InlineAutoData(0, 10)]
    public void GetApplicationSpecification_ByRecords_ReturnsCorrectSpecification(int records, int expected,
        Generator<ProjectApplication> generator)
    {
        // Arrange
        var applications = generator.Take(10).ToList();

        // out of 10 records, it should return expected records
        var spec = new GetApplicationSpecification(records: records);

        // Act
        var result = spec
            .Evaluate(applications)
            .Count();

        // Assert
        result.ShouldBe(expected);
    }

    [Theory, AutoData]
    public void GetApplicationSpecification_ByStatusAndId_ReturnsCorrectSpecification(
        Generator<ProjectApplication> generator)
    {
        // Arrange
        var applications = generator.Take(10).ToList();

        applications[1].Status = "pending";
        applications[2].Status = "pending";

        // out of 10 records, it should return a single record
        var spec = new GetApplicationSpecification("pending", applications[1].ProjectApplicationId);

        // Act
        var result = spec
            .Evaluate(applications)
            .ToList();

        // Assert
        result.Count.ShouldBe(1);
        result[0].ProjectApplicationId.ShouldBe(applications[1].ProjectApplicationId);
    }

    [Theory, InlineAutoData(5, 2), InlineAutoData(0, 2)]
    public void GetApplicationSpecification_ByStatusAndRecords_ReturnsCorrectSpecification(int records, int expected,
        Generator<ProjectApplication> generator)
    {
        // Arrange
        var applications = generator.Take(10).ToList();

        applications[1].Status = "pending";
        applications[2].Status = "pending";

        // out of 10 records, it should return a expected records
        var spec = new GetApplicationSpecification(status: "pending", records: records);

        // Act
        var result = spec
            .Evaluate(applications)
            .ToList();

        // Assert
        result.Count.ShouldBe(expected);
    }
}