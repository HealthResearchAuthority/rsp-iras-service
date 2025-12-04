using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Infrastructure.Middlewares;

namespace Rsp.IrasService.UnitTests.Infrastructure.Middlewares.ModificationAccessValidationMiddlewareTests;

public class ModificationAccessValidationMiddlewareTests : TestServiceBase<ModificationAccessValidationMiddleware>
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
    public async Task InvokeAsync_NoRelevantIds_AllowsPipeline()
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
    public async Task InvokeAsync_WithProjectRecordIdAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", "PR123", Guid.Empty))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?projectRecordId=PR123");

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
        repo.Verify(r => r.HasModificationAccess("user-1", "PR123", Guid.Empty), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithProjectRecordIdAndAccessDenied_ReturnsForbidden()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", "PR123", Guid.Empty))
            .ReturnsAsync(false);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?projectRecordId=PR123");

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
        repo.Verify(r => r.HasModificationAccess("user-1", "PR123", Guid.Empty), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithModificationIdAndAccessGranted_AllowsPipeline()
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
    public async Task InvokeAsync_WithModificationIdAndAccessDenied_ReturnsForbidden()
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
    public async Task InvokeAsync_WithProjectModificationIdAndAccessGranted_AllowsPipeline()
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
        context.Request.QueryString = new QueryString($"?projectModificationId={modificationId}");

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
    public async Task InvokeAsync_WithModificationChangeIdOnlyAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var modificationChangeId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", modificationChangeId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?modificationChangeId={modificationChangeId}");

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
        repo.Verify(r => r.HasModificationAccess("user-1", modificationChangeId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithModificationChangeIdOnlyAndAccessDenied_ReturnsForbidden()
    {
        // Arrange
        var modificationChangeId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", modificationChangeId))
            .ReturnsAsync(false);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?modificationChangeId={modificationChangeId}");

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
        repo.Verify(r => r.HasModificationAccess("user-1", modificationChangeId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithBothProjectRecordAndModificationIds_CallsWithBothIds()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", "PR123", modificationId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?projectRecordId=PR123&modificationId={modificationId}");

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
        repo.Verify(r => r.HasModificationAccess("user-1", "PR123", modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_InvalidModificationId_SkipsConversionAndUsesEmpty()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", "PR123", Guid.Empty))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?projectRecordId=PR123&modificationId=not-a-guid");

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
        repo.Verify(r => r.HasModificationAccess("user-1", "PR123", Guid.Empty), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_InvalidModificationChangeId_SkipsConversionAndUsesEmpty()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", "PR123", Guid.Empty))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?projectRecordId=PR123&modificationChangeId=invalid-guid");

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
        repo.Verify(r => r.HasModificationAccess("user-1", "PR123", Guid.Empty), Times.Once);
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
        var queryModificationId = Guid.NewGuid();
        var routeModificationId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", null, queryModificationId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString($"?modificationId={queryModificationId}");
        context.Request.RouteValues["modificationId"] = routeModificationId.ToString();

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
        repo.Verify(r => r.HasModificationAccess("user-1", null, queryModificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_AllIdsPresentAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var modificationChangeId = Guid.NewGuid();
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasModificationAccess("user-1", "PR123", modificationId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString(
            $"?projectRecordId=PR123&modificationId={modificationId}&modificationChangeId={modificationChangeId}");

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
        repo.Verify(r => r.HasModificationAccess("user-1", "PR123", modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_RepositoryThrowsException_PropagatesException()
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