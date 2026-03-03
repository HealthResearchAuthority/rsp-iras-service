using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsp.Service.Application.CQRS.Commands;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.UnitTests;
using Rsp.Service.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.ProjectModificationsControllerTests;

public class DuplicateModification : TestServiceBase
{
    private readonly ProjectModificationsController _controller;

    public DuplicateModification()
    {
        _controller = Mocker.CreateInstance<ProjectModificationsController>();
    }

    [Theory, AutoData]
    public async Task DeleteModificationDocumentAnswers_SendsExpectedCommand(
        DuplicateModificationRequest duplicateModificationRequest)
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<DuplicateModificationRequest>(), It.IsAny<CancellationToken>()));

        // Act
        await _controller.DuplicateModification(duplicateModificationRequest);

        // Assert
        mediator
            .Verify
            (
                m => m.Send(
                    It.Is<DuplicateModificationCommand>(cmd =>
                        cmd.ModificationRequest == duplicateModificationRequest
                    ),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}