using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

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