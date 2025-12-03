using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

/// <summary>
///     Covers the tests for GetModificationChange method
/// </summary>
public class GetModificationChangeTests() : TestServiceBase<ProjectModificationService>
{
    [Theory, AutoData]
    public async Task Returns_ModificationChangeResponse(ProjectModificationChange expected)
    {
        // Arrange
        var repo = new Mock<IProjectModificationRepository>();
        repo
            .Setup(r => r.GetModificationChange(It.IsAny<GetModificationChangeSpecification>()))
            .ReturnsAsync(expected);

        Mocker.Use(repo.Object);

        Sut = Mocker.CreateInstance<ProjectModificationService>();

        // Act
        var result = await Sut.GetModificationChange(expected.Id);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ModificationChangeResponse>();
        result.ProjectModificationId.ShouldBe(expected.ProjectModificationId);
    }
}