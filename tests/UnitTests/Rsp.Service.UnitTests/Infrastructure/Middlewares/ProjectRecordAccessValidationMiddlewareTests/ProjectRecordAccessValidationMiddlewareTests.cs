using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Infrastructure.Middlewares;

namespace Rsp.Service.UnitTests.Infrastructure.Middlewares.ProjectRecordAccessValidationMiddlewareTests;

public class ProjectRecordAccessValidationMiddlewareTests : TestServiceBase<ProjectRecordAccessValidationMiddleware>
{
    [Fact]
    public async Task InvokeAsync_NoUserId_ReturnsForbidden()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity([], "Test");
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
    public async Task InvokeAsync_NoProjectRecordIdOrApplicationId_AllowsPipeline()
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
    public async Task InvokeAsync_WithProjectRecordIdQueryAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "PR123"))
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
        repo.Verify(r => r.HasProjectAccess("user-1", "PR123"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithProjectRecordIdQueryAndAccessDenied_ReturnsForbidden()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "PR123"))
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
        repo.Verify(r => r.HasProjectAccess("user-1", "PR123"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithApplicationIdQueryAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "APP456"))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?applicationId=APP456");

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
        repo.Verify(r => r.HasProjectAccess("user-1", "APP456"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithApplicationIdQueryAndAccessDenied_ReturnsForbidden()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "APP456"))
            .ReturnsAsync(false);

        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?applicationId=APP456");

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
        repo.Verify(r => r.HasProjectAccess("user-1", "APP456"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithProjectRecordIdInRouteAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "PR789"))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.RouteValues["projectRecordId"] = "PR789";

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
        repo.Verify(r => r.HasProjectAccess("user-1", "PR789"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithApplicationIdInRouteAndAccessGranted_AllowsPipeline()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "APP999"))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.RouteValues["applicationId"] = "APP999";

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
        repo.Verify(r => r.HasProjectAccess("user-1", "APP999"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_QueryStringTakesPrecedenceOverRoute_UsesQueryStringValue()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "QueryValue"))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?projectRecordId=QueryValue");
        context.Request.RouteValues["projectRecordId"] = "RouteValue";

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
        repo.Verify(r => r.HasProjectAccess("user-1", "QueryValue"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_MultipleQueryParameters_UsesCorrectId()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "APP222"))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?someParam=value&applicationId=APP222&anotherParam=something");

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
        repo.Verify(r => r.HasProjectAccess("user-1", "APP222"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_RouteValuesUsedWhenNoQueryString_UsesRouteValue()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "RouteProjectId"))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.RouteValues["projectRecordId"] = "RouteProjectId";

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
        repo.Verify(r => r.HasProjectAccess("user-1", "RouteProjectId"), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_RepositoryThrowsException_PropagatesException()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasProjectAccess("user-1", "PR123"))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        var context = new DefaultHttpContext();
        var identity = new ClaimsIdentity
        (
            [new Claim("userId", "user-1")],
            "Test"
        );

        context.User = new ClaimsPrincipal(identity);
        context.Request.QueryString = new QueryString("?projectRecordId=PR123");

        Task next(HttpContext _)
        {
            return Task.CompletedTask;
        }

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(async () =>
            await Sut.InvokeAsync(context, next));
    }
}