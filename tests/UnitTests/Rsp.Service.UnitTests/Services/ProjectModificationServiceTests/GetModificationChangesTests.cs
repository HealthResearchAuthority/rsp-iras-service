using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for GetModificationChanges method
/// </summary>
public class GetModificationChangesTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Returns_List_Of_ModificationChangeResponse(string projectRecordId, Guid projectModificationId, List<ProjectModificationChange> changes)
    {
        // Arrange
        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.GetModificationChanges(It.IsAny<GetModificationChangesSpecification>()))
            .ReturnsAsync(changes.AsEnumerable());

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var result = await Sut.GetModificationChanges(projectRecordId, projectModificationId);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(changes.Count);
    }
}