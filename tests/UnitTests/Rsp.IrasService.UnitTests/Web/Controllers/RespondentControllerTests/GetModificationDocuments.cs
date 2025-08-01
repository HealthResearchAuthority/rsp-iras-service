﻿using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class GetModificationDocuments : TestServiceBase<RespondentController>
{
    [Theory, AutoData]
    public async Task GetModificationDocuments_ByModificationChangeIdAndProjectRecordId_ReturnsExpected
    (
        Guid modificationChangeId,
        string projectRecordId,
        string projectPersonnelId,
        List<ModificationDocumentDto> expected
    )
    {
        // Arrange
        var mediator = Mocker.GetMock<IMediator>();

        mediator
            .Setup(m => m.Send(It.IsAny<GetModificationDocumentsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await Sut.GetModificationDocuments(modificationChangeId, projectRecordId, projectPersonnelId);

        // Assert
        result.ShouldBeEquivalentTo(expected);

        mediator
            .Verify
            (m => m
                .Send
                (
                    It.Is<GetModificationDocumentsQuery>
                    (q =>
                        q.ProjectModificationChangeId == modificationChangeId &&
                        q.ProjectRecordId == projectRecordId
                    ), It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
    }
}