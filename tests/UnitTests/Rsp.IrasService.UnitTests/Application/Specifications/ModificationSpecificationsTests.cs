using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.UnitTests.Application.Specifications;

public class GetModificationAnswersSpecificationTests
{
    [Fact]
    public void Filters_By_Modification_And_Record()
    {
        var modId = Guid.NewGuid();
        var prId = "PR-1";
        var data = new List<ProjectModificationAnswer>
        {
            new() { ProjectModificationId = modId, ProjectRecordId = prId, Category = "C1" },
            new() { ProjectModificationId = modId, ProjectRecordId = prId, Category = "C2" },
            new() { ProjectModificationId = Guid.NewGuid(), ProjectRecordId = prId }
        };

        var spec = new GetModificationAnswersSpecification(modId, prId);
        var result = spec.Evaluate(data).ToList();

        result.Count.ShouldBe(2);
        result.All(r => r.ProjectModificationId == modId && r.ProjectRecordId == prId).ShouldBeTrue();
    }

    [Fact]
    public void Filters_By_Modification_Record_And_Category()
    {
        var modId = Guid.NewGuid();
        var prId = "PR-1";
        var cat = "C1";
        var data = new List<ProjectModificationAnswer>
        {
            new() { ProjectModificationId = modId, ProjectRecordId = prId, Category = cat },
            new() { ProjectModificationId = modId, ProjectRecordId = prId, Category = "C2" },
        };

        var spec = new GetModificationAnswersSpecification(modId, prId, cat);
        var result = spec.Evaluate(data).ToList();

        result.Count.ShouldBe(1);
        result.All(r => r.ProjectModificationId == modId && r.ProjectRecordId == prId && r.Category == cat).ShouldBeTrue();
    }
}

public class GetModificationChangeAnswersSpecificationTests
{
    [Fact]
    public void Filters_By_ModificationChange_And_Record()
    {
        var changeId = Guid.NewGuid();
        var prId = "PR-1";
        var data = new List<ProjectModificationChangeAnswer>
        {
            new() { ProjectModificationChangeId = changeId, ProjectRecordId = prId, Category = "C1" },
            new() { ProjectModificationChangeId = changeId, ProjectRecordId = prId, Category = "C2" },
            new() { ProjectModificationChangeId = Guid.NewGuid(), ProjectRecordId = prId }
        };

        var spec = new GetModificationChangeAnswersSpecification(changeId, prId);
        var result = spec.Evaluate(data).ToList();

        result.Count.ShouldBe(2);
        result.All(r => r.ProjectModificationChangeId == changeId && r.ProjectRecordId == prId).ShouldBeTrue();
    }

    [Fact]
    public void Filters_By_ModificationChange_Record_And_Category()
    {
        var changeId = Guid.NewGuid();
        var prId = "PR-1";
        var cat = "C1";
        var data = new List<ProjectModificationChangeAnswer>
        {
            new() { ProjectModificationChangeId = changeId, ProjectRecordId = prId, Category = cat },
            new() { ProjectModificationChangeId = changeId, ProjectRecordId = prId, Category = "C2" },
        };

        var spec = new GetModificationChangeAnswersSpecification(changeId, prId, cat);
        var result = spec.Evaluate(data).ToList();

        result.Count.ShouldBe(1);
        result.All(r => r.ProjectModificationChangeId == changeId && r.ProjectRecordId == prId && r.Category == cat).ShouldBeTrue();
    }
}

public class GetModificationSpecificationTests
{
    [Fact]
    public void Filters_By_Id()
    {
        var id = Guid.NewGuid();
        var data = new List<ProjectModification>
        {
            new() { Id = id },
            new() { Id = Guid.NewGuid() },
        };

        var spec = new GetModificationSpecification(id);
        var result = spec.Evaluate(data).ToList();

        result.Count.ShouldBe(1);
        result.Single().Id.ShouldBe(id);
    }
}

public class GetModificationChangeSpecificationTests
{
    [Fact]
    public void Filters_By_Id()
    {
        var id = Guid.NewGuid();
        var data = new List<ProjectModificationChange>
        {
            new() { Id = id },
            new() { Id = Guid.NewGuid() },
        };

        var spec = new GetModificationChangeSpecification(id);
        var result = spec.Evaluate(data).ToList();

        result.Count.ShouldBe(1);
        result.Single().Id.ShouldBe(id);
    }
}

public class SaveModificationChangeResponsesSpecificationTests
{
    [Fact]
    public void Filters_By_Change_Record_And_Personnel()
    {
        var changeId = Guid.NewGuid();
        var prId = "PR-1";
        var personId = "P-1";
        var data = new List<ProjectModificationChangeAnswer>
        {
            new() { ProjectModificationChangeId = changeId, ProjectRecordId = prId, ProjectPersonnelId = personId },
            new() { ProjectModificationChangeId = changeId, ProjectRecordId = prId, ProjectPersonnelId = "P-2" },
        };

        var spec = new SaveModificationChangeResponsesSpecification(changeId, prId, personId);
        var result = spec.Evaluate(data).ToList();

        result.Count.ShouldBe(1);
        result.Single().ProjectPersonnelId.ShouldBe(personId);
    }
}

public class SaveModificationResponsesSpecificationTests_V2
{
    [Fact]
    public void Filters_By_Modification_Record_And_Personnel()
    {
        var modId = Guid.NewGuid();
        var prId = "PR-1";
        var personId = "P-1";
        var data = new List<ProjectModificationAnswer>
        {
            new() { ProjectModificationId = modId, ProjectRecordId = prId, ProjectPersonnelId = personId },
            new() { ProjectModificationId = modId, ProjectRecordId = prId, ProjectPersonnelId = "P-2" },
        };

        var spec = new SaveModificationResponsesSpecification(modId, prId, personId);
        var result = spec.Evaluate(data).ToList();

        result.Count.ShouldBe(1);
        result.Single().ProjectPersonnelId.ShouldBe(personId);
    }
}
