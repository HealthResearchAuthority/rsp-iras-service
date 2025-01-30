using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Rsp.IrasService.UnitTests.Application.Authorization.AuthHandlers.ReviewAccessRequirementHandlerTests;

public class HandleRequirementAsync : TestServiceBase
{
    [Fact]
    public async Task WithRequirementAlreadyMet_Returns_CompletedTask()
    {
        // Arrange
        var requirement = new ReviewerAccessRequirement();

        var authorizationHandlerContext = new AuthorizationHandlerContext
        (
            [requirement],
            new ClaimsPrincipal(),
            null
        );

        authorizationHandlerContext.Succeed(requirement);

        var assignedRoleHandler = Mocker.CreateInstance<ReviewerAccessRequirementHandler>();

        // Act
        await assignedRoleHandler.HandleAsync(authorizationHandlerContext);

        // Assert
        authorizationHandlerContext
            .HasFailed
            .ShouldBeFalse();

        authorizationHandlerContext
            .HasSucceeded
            .ShouldBeTrue();

        // Verify
        Mocker
            .GetMock<ILogger<ReviewerAccessRequirementHandler>>()
            .Verify(logger => logger.IsEnabled(LogLevel.Information), Times.Never);
    }

    [Fact]
    public async Task WithRequirementFails_Returns_CompletedTask()
    {
        // Arrange
        var authorizationHandlerContext = new AuthorizationHandlerContext
        (
            [new ReviewerAccessRequirement()],
            new ClaimsPrincipal(),
            null
        );

        // if the requirement was failed previously
        authorizationHandlerContext.Fail();

        var assignedRoleHandler = Mocker.CreateInstance<ReviewerAccessRequirementHandler>();

        // Act
        await assignedRoleHandler.HandleAsync(authorizationHandlerContext);

        // Assert
        authorizationHandlerContext
            .HasFailed
            .ShouldBeTrue();

        authorizationHandlerContext
            .HasSucceeded
            .ShouldBeFalse();
    }

    [Fact]
    public async Task WithUserHasNoValidRoles_Returns()
    {
        // Arrange
        var identity = new ClaimsIdentity
        (
            [
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "user")
            ],
            "any"
        );

        var authorizationHandlerContext = new AuthorizationHandlerContext
        (
            [new ReviewerAccessRequirement()],
            new ClaimsPrincipal(identity),
            null
        );

        var assignedRoleHandler = Mocker.CreateInstance<ReviewerAccessRequirementHandler>();

        // Act
        await assignedRoleHandler.HandleAsync(authorizationHandlerContext);

        // Assert
        authorizationHandlerContext
            .HasFailed
            .ShouldBeFalse();

        authorizationHandlerContext
            .HasSucceeded
            .ShouldBeFalse();

        // Verify
        Mocker
            .GetMock<ILogger<ReviewerAccessRequirementHandler>>()
            .Verify(logger => logger.IsEnabled(LogLevel.Error), Times.Once);
    }

    [Fact]
    public async Task WithNoValidStatuses_Returns()
    {
        // Arrange
        var identity = new ClaimsIdentity
        (
            [
                new Claim(ClaimTypes.Role, "reviewer"),
                new Claim(ClaimTypes.Role, "user")
            ],
            "any"
        );

        var authorizationHandlerContext = new AuthorizationHandlerContext
        (
            [new ReviewerAccessRequirement()],
            new ClaimsPrincipal(identity),
            new DefaultHttpContext()
        );

        var assignedRoleHandler = Mocker.CreateInstance<ReviewerAccessRequirementHandler>();

        // Act
        await assignedRoleHandler.HandleAsync(authorizationHandlerContext);

        // Assert
        authorizationHandlerContext
            .HasFailed
            .ShouldBeFalse();

        authorizationHandlerContext
            .HasSucceeded
            .ShouldBeFalse();

        // Verify
        Mocker
            .GetMock<ILogger<ReviewerAccessRequirementHandler>>()
            .Verify(logger => logger.IsEnabled(LogLevel.Error), Times.Once);
    }

    [Fact]
    public async Task WithValidStatuses_Returns()
    {
        // Arrange
        var identity = new ClaimsIdentity
        (
            [
                new Claim(ClaimTypes.Role, "reviewer"),
                new Claim(ClaimTypes.Role, "user")
            ],
            "any"
        );

        var routeValues = new RouteValuesFeature
        {
            RouteValues = new RouteValueDictionary
            {
                { "status", "pending" }
            }
        };

        var routing = new RoutingFeature
        {
            RouteData = new RouteData(routeValues.RouteValues)
        };

        var httpContext = new DefaultHttpContext();
        httpContext.Features.Set<IRoutingFeature>(routing);

        var authorizationHandlerContext = new AuthorizationHandlerContext
        (
            [new ReviewerAccessRequirement()],
            new ClaimsPrincipal(identity),
            httpContext
        );

        var assignedRoleHandler = Mocker.CreateInstance<ReviewerAccessRequirementHandler>();

        // Act
        await assignedRoleHandler.HandleAsync(authorizationHandlerContext);

        // Assert
        authorizationHandlerContext
            .HasFailed
            .ShouldBeFalse();

        authorizationHandlerContext
            .HasSucceeded
            .ShouldBeTrue();

        // Verify
        Mocker
            .GetMock<ILogger<ReviewerAccessRequirementHandler>>()
            .Verify(logger => logger.IsEnabled(LogLevel.Information), Times.Once);
    }
}