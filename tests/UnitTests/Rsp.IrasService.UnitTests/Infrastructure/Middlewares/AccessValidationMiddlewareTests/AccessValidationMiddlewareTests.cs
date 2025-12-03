using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Constants;
using Rsp.IrasService.Infrastructure.Middlewares;

namespace Rsp.IrasService.UnitTests.Infrastructure.Middlewares.AccessValidationMiddlewareTests;

public class AccessValidationMiddlewareTests : TestServiceBase<AccessValidationMiddleware>
{
    [Fact]
    public async Task InvokeAsync_NoUser_AllowsPipeline()
    {
        // Arrange
        var context = new DefaultHttpContext();

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
        context.Response.StatusCode.ShouldBe(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task InvokeAsync_HighPrivilegeRole_AllowsPipeline()
    {
        // Arrange
        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
        [
            new Claim("userId", "user-1"),
            new Claim(ClaimTypes.Role, Roles.TeamManager)
        ], "Test");

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
    public async Task InvokeAsync_ApplicantWithProject_AllowsWhenRepositoryGrantsAccess()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();

        repo

            .Setup(x => x.HasAccessAsync("user-1", "PR123", null))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
        [
            new Claim("userId", "user-1"),
            new Claim(ClaimTypes.Role, Roles.Applicant)
        ], "Test");

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
        repo.Verify(r => r.HasAccessAsync("user-1", "PR123", null), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_ApplicantWithProject_BlocksWhenRepositoryDeniesAccess()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasAccessAsync("user-1", "PR123", null))
            .ReturnsAsync(false);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
        [
            new Claim("userId", "user-1"),
            new Claim(ClaimTypes.Role, Roles.Applicant)
        ], "Test");

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
        repo.Verify(r => r.HasAccessAsync("user-1", "PR123", null), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_AuthenticatedButNoUserId_AllowsPipeline()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
        [
            new Claim(ClaimTypes.Role, Roles.Applicant)
        ], "Test");

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
        repo.Verify(r => r.HasAccessAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<Guid?>()), Times.Never);
    }

    [Fact]
    public async Task InvokeAsync_WithModificationIdQuery_CallsRepositoryWithModificationId()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();

        var modificationId = Guid.NewGuid();

        repo
            .Setup(x => x.HasAccessAsync("user-1", null, modificationId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
        [
            new Claim("userId", "user-1"),
            new Claim(ClaimTypes.Role, Roles.Applicant)
        ], "Test");

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
        repo.Verify(r => r.HasAccessAsync("user-1", null, modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithProjectModificationIdQuery_CallsRepositoryWithModificationId()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();

        var modificationId = Guid.NewGuid();

        repo
            .Setup(x => x.HasAccessAsync("user-1", null, modificationId))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
        [
            new Claim("userId", "user-1"),
            new Claim(ClaimTypes.Role, Roles.Applicant)
        ], "Test");

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
        repo.Verify(r => r.HasAccessAsync("user-1", null, modificationId), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WithRouteProjectRecordId_CallsRepositoryWithProjectRecordId()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();
        repo
            .Setup(x => x.HasAccessAsync("user-1", "PR456", null))
            .ReturnsAsync(true);

        var context = new DefaultHttpContext();
        context.Request.RouteValues["projectRecordId"] = "PR456";

        var identity = new ClaimsIdentity
        (
        [
            new Claim("userId", "user-1"),
            new Claim(ClaimTypes.Role, Roles.Sponsor)
        ], "Test");

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
        repo.Verify(r => r.HasAccessAsync("user-1", "PR456", null), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_ApplyingRoleButNoIds_SkipsValidation()
    {
        // Arrange
        var repo = Mocker.GetMock<IAccessValidationRepository>();

        var context = new DefaultHttpContext();

        var identity = new ClaimsIdentity
        (
        [
            new Claim("userId", "user-1"),
            new Claim(ClaimTypes.Role, Roles.Applicant)
        ], "Test");

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
        repo.Verify(r => r.HasAccessAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<Guid?>()), Times.Never);
    }
}