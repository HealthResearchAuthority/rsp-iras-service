using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for GetModificationChanges method
/// </summary>
public class GetModificationChangesTests : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Returns_List_Of_ModificationChangeResponse(Guid projectModificationId, List<ProjectModificationChange> changes)
    {
        // Arrange
        var repo = new Mock<IProjectModificationRepository>();
        repo.Setup(r => r.GetModificationChanges(It.IsAny<GetModificationChangesSpecification>()))
            .ReturnsAsync(changes.AsEnumerable());

        Mocker.Use(repo.Object);
        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var result = await Sut.GetModificationChanges(projectModificationId);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(changes.Count);
    }
}