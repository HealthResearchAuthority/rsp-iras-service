using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;
using Rsp.Service.UnitTests.Fixtures;

namespace Rsp.Service.UnitTests.Application.Specifications;

public class GetApplicationSpecificationTests
{
    [Theory, NoRecursionAutoData]
    public void GetApplicationSpecification_ById_ReturnsCorrectSpecification(Generator<ProjectRecord> generator)
    {
        // Arrange
        var applications = generator.Take(3).ToList();

        var spec = new GetApplicationSpecification(id: applications[0].Id);

        // Act
        var result = spec
            .Evaluate(applications)
            .SingleOrDefault();

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(applications[0].Id);
    }

    [Theory, NoRecursionInlineAutoData(5, 5), NoRecursionInlineAutoData(0, 10)]
    public void GetApplicationSpecification_ByRecords_ReturnsCorrectSpecification(int records, int expected,
        Generator<ProjectRecord> generator)
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

    [Theory, NoRecursionAutoData]
    public void GetApplicationSpecification_ByStatusAndId_ReturnsCorrectSpecification(
        Generator<ProjectRecord> generator)
    {
        // Arrange
        var applications = generator.Take(10).ToList();

        applications[1].Status = "pending";
        applications[2].Status = "pending";

        // out of 10 records, it should return a single record
        var spec = new GetApplicationSpecification("pending", applications[1].Id);

        // Act
        var result = spec
            .Evaluate(applications)
            .ToList();

        // Assert
        result.Count.ShouldBe(1);
        result[0].Id.ShouldBe(applications[1].Id);
    }

    [Theory, NoRecursionInlineAutoData(5, 2), NoRecursionInlineAutoData(0, 2)]
    public void GetApplicationSpecification_ByStatusAndRecords_ReturnsCorrectSpecification(int records, int expected,
        Generator<ProjectRecord> generator)
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