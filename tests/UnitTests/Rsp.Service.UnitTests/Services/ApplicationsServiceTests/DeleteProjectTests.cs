using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Settings;
using Rsp.Service.Application.Specifications;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ApplicationsServiceTests;

public class DeleteProjectTests : TestServiceBase<ApplicationsService>
{
    [Theory, AutoData]
    public async Task Calls_Repository_DeleteProjectRecord_With_Correct_Specification(string projectRecordId)
    {
        // Arrange
        var repo = new Mock<IProjectRecordRepository>();
        var appSettings = new AppSettings();
        repo.Setup
        (
            r => r
                .DeleteProjectRecord(It.Is<GetApplicationSpecification>(spec => spec != null))
        )
        .Returns(Task.CompletedTask)
        .Verifiable();

        Mocker.Use(repo.Object);
        Mocker.Use(appSettings);

        Sut = Mocker.CreateInstance<ApplicationsService>();

        // Act
        await Sut.DeleteProject(projectRecordId);

        // Assert
        repo.Verify(r => r.DeleteProjectRecord(It.Is<GetApplicationSpecification>(spec => spec != null)), Times.Once);
    }
}