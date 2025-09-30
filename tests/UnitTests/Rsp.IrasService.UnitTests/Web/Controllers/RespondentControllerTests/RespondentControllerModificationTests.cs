using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.WebApi.Controllers;

namespace Rsp.IrasService.UnitTests.Web.Controllers.RespondentControllerTests;

public class RespondentControllerModificationTests : TestServiceBase<RespondentController>
{
    [Fact]
    public async Task GetModificationAnswers_NoCategory_Sends_Query()
    {
        var mediator = Mocker.GetMock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<GetModificationAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<RespondentAnswerDto>());

        var modId = Guid.NewGuid();
        var prId = "PR-1";

        var res = await Sut.GetModificationAnswers(modId, prId);

        res.ShouldNotBeNull();
        mediator.Verify(m => m.Send(It.Is<GetModificationAnswersQuery>(q => q.ProjectModificationId == modId && q.ProjectRecordId == prId && q.CategoryId == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetModificationAnswers_WithCategory_Sends_Query()
    {
        var mediator = Mocker.GetMock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<GetModificationAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<RespondentAnswerDto>());

        var modId = Guid.NewGuid();
        var prId = "PR-1";
        var cat = "C-1";

        var res = await Sut.GetModificationAnswers(modId, prId, cat);

        res.ShouldNotBeNull();
        mediator.Verify(m => m.Send(It.Is<GetModificationAnswersQuery>(q => q.ProjectModificationId == modId && q.ProjectRecordId == prId && q.CategoryId == cat), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetModificationChangeAnswers_NoCategory_Sends_Query()
    {
        var mediator = Mocker.GetMock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<GetModificationChangeAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<RespondentAnswerDto>());

        var changeId = Guid.NewGuid();
        var prId = "PR-1";

        var res = await Sut.GetModificationChangeAnswers(changeId, prId);

        res.ShouldNotBeNull();
        mediator.Verify(m => m.Send(It.Is<GetModificationChangeAnswersQuery>(q => q.ProjectModificationChangeId == changeId && q.ProjectRecordId == prId && q.CategoryId == null), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetModificationChangeAnswers_WithCategory_Sends_Query()
    {
        var mediator = Mocker.GetMock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<GetModificationChangeAnswersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<RespondentAnswerDto>());

        var changeId = Guid.NewGuid();
        var prId = "PR-1";
        var cat = "C-1";

        var res = await Sut.GetModificationChangeAnswers(changeId, prId, cat);

        res.ShouldNotBeNull();
        mediator.Verify(m => m.Send(It.Is<GetModificationChangeAnswersQuery>(q => q.ProjectModificationChangeId == changeId && q.ProjectRecordId == prId && q.CategoryId == cat), It.IsAny<CancellationToken>()), Times.Once);
    }
}