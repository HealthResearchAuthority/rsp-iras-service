using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.CQRS.Handlers.QueryHandlers;
using Rsp.Service.Application.CQRS.Queries;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;

namespace Rsp.Service.UnitTests.Application.Extensions;

public class AllowedStatusesExtensionsHandlerTests
{
    [Fact]
    public async Task GetApplicationsHandler_FiltersByAllowedStatuses()
    {
        var service = new Mock<IApplicationsService>();

        var apps = new List<ApplicationResponse>
        {
            new() { Id = "1", Status = "allowed" },
            new() { Id = "2", Status = "blocked" }
        };

        service
            .Setup(s => s.GetApplications())
            .ReturnsAsync(apps);

        var handler = new GetApplicationsHandler(service.Object);

        var query = new GetApplicationsQuery
        {
            AllowedStatuses = ["allowed"]
        };

        var response = await handler.Handle(query, CancellationToken.None);

        response.ShouldHaveSingleItem();
        response.First().Status.ToLowerInvariant().ShouldBe("allowed");
    }

    [Fact]
    public async Task GetApplicationHandler_ReturnsNullWhenStatusNotAllowed()
    {
        var service = new Mock<IApplicationsService>();

        var app = new ApplicationResponse { Id = "1", Status = "blocked" };

        service
            .Setup(s => s.GetApplication("1"))
            .ReturnsAsync(app);

        var handler = new GetApplicationHandler(service.Object);
        var query = new GetApplicationQuery("1")
        {
            AllowedStatuses = ["allowed"]
        };

        var response = await handler.Handle(query, CancellationToken.None);

        response.ShouldBeNull();
    }

    [Fact]
    public async Task GetApplicationsWithStatusHandler_FiltersByAllowedStatuses()
    {
        var service = new Mock<IApplicationsService>();

        var apps = new List<ApplicationResponse>
        {
            new() { Id = "1", Status = "allowed" },
            new() { Id = "2", Status = "blocked" }
        };

        service
            .Setup(s => s.GetApplications("any"))
            .ReturnsAsync(apps);

        var handler = new GetApplicationsWithStatusHandler(service.Object);
        var query = new GetApplicationsWithStatusQuery
        {
            ApplicationStatus = "any",
            AllowedStatuses = ["allowed"]
        };

        var response = await handler.Handle(query, CancellationToken.None);

        response.ShouldHaveSingleItem();
        response.First().Status.ToLowerInvariant().ShouldBe("allowed");
    }

    [Fact]
    public async Task GetApplicationsWithRespondentHandler_FiltersByAllowedStatuses()
    {
        var service = new Mock<IApplicationsService>();

        var apps = new List<ApplicationResponse>
        {
            new() { Id = "1", Status = "allowed" },
            new() { Id = "2", Status = "blocked" }
        };

        service
            .Setup(s => s.GetRespondentApplications("r1"))
            .ReturnsAsync(apps);

        var handler = new GetApplicationsWithRespondentHandler(service.Object);
        var query = new GetApplicationsWithRespondentQuery
        {
            RespondentId = "r1",
            AllowedStatuses = ["allowed"]
        };

        var response = await handler.Handle(query, CancellationToken.None);

        response.ShouldHaveSingleItem();
        response.First().Status.ToLowerInvariant().ShouldBe("allowed");
    }

    [Fact]
    public async Task GetModificationDocumentsHandler_FiltersByAllowedStatuses()
    {
        var service = new Mock<IRespondentService>();

        var docs = new List<ModificationDocumentDto>
        {
            new() { Id = Guid.NewGuid(), FileName = "f1", Status = "allowed" },
            new() { Id = Guid.NewGuid(), FileName = "f2", Status = "blocked" }
        };

        service
            .Setup(s => s.GetModificationDocumentResponses(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(docs);

        var handler = new GetModificationDocumentsHandler(service.Object);
        var query = new GetModificationDocumentsQuery
        {
            ProjectModificationId = Guid.NewGuid(),
            ProjectRecordId = "p1",
            AllowedStatuses = ["allowed"]
        };

        var response = await handler.Handle(query, CancellationToken.None);

        response.Count().ShouldBe(1);
        response.First().Status?.ToLowerInvariant().ShouldBe("allowed");
    }

    [Fact]
    public async Task GetModificationDocumentDetailsHandler_FiltersByAllowedStatuses_ReturnsNullWhenNotAllowed()
    {
        var service = new Mock<IRespondentService>();

        var doc = new ModificationDocumentDto { Id = Guid.NewGuid(), FileName = "f1", Status = "blocked" };
        service
            .Setup(s => s.GetModificationDocumentDetailsResponses(It.IsAny<Guid>())).ReturnsAsync(doc);

        var handler = new GetModificationDocumentDetailsHandler(service.Object);
        var query = new GetModificationDocumentDetailsQuery
        {
            Id = doc.Id,
            AllowedStatuses = ["allowed"]
        };

        var response = await handler.Handle(query, CancellationToken.None);

        response.ShouldBeNull();
    }

    [Fact]
    public async Task GetModificationHandler_ReturnsNullWhenNotAllowed()
    {
        var service = new Mock<IProjectModificationService>();

        var mod = new ModificationResponse { Id = Guid.NewGuid(), Status = "blocked" };
        service
            .Setup(s => s.GetModification(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(mod);

        var handler = new GetModificationHandler(service.Object);
        var query = new GetModificationQuery
        {
            ProjectRecordId = "p1",
            ProjectModificationId = mod.Id,
            AllowedStatuses = ["allowed"]
        };

        var response = await handler.Handle(query, CancellationToken.None);

        response.ShouldBeNull();
    }
}