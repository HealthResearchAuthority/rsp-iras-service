using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Infrastructure.Middlewares;

namespace Rsp.IrasService.UnitTests.Infrastructure.Middlewares.DocumentAccessValidationMiddlewareTests;

public class DocumentAccessValidationMiddlewareTests : TestServiceBase<DocumentAccessValidationMiddleware>
{
    [Fact]
    public async Task InvokeAsync_NoUserId_ReturnsForbidden()
    {
        // Arrange
        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeFalse();
        context.Response.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
    }

    [Fact]
    public async Task InvokeAsync_EmptyUserId_ReturnsForbidden()
    {
        // Arrange
        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeFalse();
        context.Response.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
    }

    [Fact]
    public async Task InvokeAsync_NoDocumentIdOrModificationId_AllowsPipeline()
    {
        // Arrange
        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
    }

    [Fact]
    public async Task InvokeAsync_WithDocumentIdQueryAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasDocumentAccess("user-1", documentId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?documentId={documentId}");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasDocumentAccess("user-1", documentId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithDocumentIdQueryAndAccessDenied_ReturnsForbidden()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasDocumentAccess("user-1", documentId))
            .ReturnsAsync(false);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?documentId={documentId}");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeFalse();
        context.Response.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
        repo.Verify(r => r.HasDocumentAccess("user-1", documentId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithModificationIdQueryAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", null, modificationId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?modificationId={modificationId}");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasModificationAccess("user-1", null, modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithModificationIdQueryAndAccessDenied_ReturnsForbidden()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", null, modificationId))
            .ReturnsAsync(false);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?modificationId={modificationId}");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeFalse();
        context.Response.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
        repo.Verify(r => r.HasModificationAccess("user-1", null, modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithDocumentIdInRoute_UsesRouteValue()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasDocumentAccess("user-1", documentId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.RouteValues["documentId"] = documentId.ToString();

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasDocumentAccess("user-1", documentId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithModificationIdInRoute_UsesRouteValue()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", null, modificationId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.RouteValues["modificationId"] = modificationId.ToString();

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasModificationAccess("user-1", null, modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_QueryStringTakesPrecedenceOverRoute()
    {
        // Arrange
        var queryDocumentId = Guid.NewGuid();
        var routeDocumentId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasDocumentAccess("user-1", queryDocumentId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?documentId={queryDocumentId}");
        context.Request.RouteValues["documentId"] = routeDocumentId.ToString();

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasDocumentAccess("user-1", queryDocumentId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithBothIdsAndDocumentAccessDenied_ReturnsForbidden()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var modificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasDocumentAccess("user-1", documentId))
            .ReturnsAsync(false);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?documentId={documentId}&modificationId={modificationId}");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeFalse();
        context.Response.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
        repo.Verify(r => r.HasDocumentAccess("user-1", documentId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_InvalidDocumentId_SkipsConversionAndUsesEmpty()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?documentId=not-a-guid");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasDocumentAccess(It.IsAny<string>(), It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task InvokeAsync_InvalidModificationId_SkipsConversionAndUsesEmpty()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?modificationId=not-a-guid");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasModificationAccess(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task InvokeAsync_OnlyModificationIdWithAccessGranted_AllowsPipeline()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", null, modificationId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?modificationId={modificationId}");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasModificationAccess("user-1", null, modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_OnlyModificationIdWithAccessDenied_ReturnsForbidden()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", null, modificationId))
            .ReturnsAsync(false);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?modificationId={modificationId}");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeFalse();
        context.Response.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
        repo.Verify(r => r.HasModificationAccess("user-1", null, modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_MultipleQueryParameters_UsesCorrectId()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasDocumentAccess("user-1", documentId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?someParam=value&documentId={documentId}&anotherParam=something");

        var nextCalled = false;

        Task next(HttpContext _)
        {
            nextCalled = true;
            return Task.CompletedTask;
        }

        // Act
        await Sut.InvokeAsync(context, next);

        // Assert
        nextCalled.ShouldBeTrue();
        repo.Verify(r => r.HasDocumentAccess("user-1", documentId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_RepositoryThrowsExceptionForDocumentAccess_PropagatesException()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasDocumentAccess("user-1", documentId))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?documentId={documentId}");

        Task next(HttpContext _)
        {
            return Task.CompletedTask;
        }

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(async () =>
            await Sut.InvokeAsync(context, next));
    }

    [Fact]
    public async Task InvokeAsync_RepositoryThrowsExceptionForModificationAccess_PropagatesException()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", null, modificationId))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?modificationId={modificationId}");

        Task next(HttpContext _)
        {
            return Task.CompletedTask;
        }

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(async () =>
            await Sut.InvokeAsync(context, next));
    }
}