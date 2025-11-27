using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rsp.IrasService.Application.Behaviors;
using Rsp.IrasService.Application.CQRS.Queries;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;

namespace Rsp.IrasService.UnitTests.Application.Behaviors;

public class AllowedStatusesPopulationBehaviorTests
{
    [Fact]
    public async Task NoHttpContext_DoesNotThrowAndCallsNext()
    {
        // Arrange
        var accessor = new Mock<IHttpContextAccessor>();

        accessor
            .Setup(a => a.HttpContext)
            .Returns(default(HttpContext));

        var behavior = new AllowedStatusesPopulationBehavior<GetApplicationsQuery, IEnumerable<ApplicationResponse>>(accessor.Object);

        var query = new GetApplicationsQuery();

        var nextCalled = false;
        Task<IEnumerable<ApplicationResponse>> next()
        {
            nextCalled = true;
            return Task.FromResult<IEnumerable<ApplicationResponse>>([]);
        }

        // Act
        await behavior.Handle(query, next, CancellationToken.None);

        // Assert
        nextCalled.ShouldBeTrue();
        query.AllowedStatuses.ShouldBeEmpty();
    }

    [Fact]
    public async Task UnauthenticatedUser_DoesNotPopulateAllowedStatuses()
    {
        // Arrange
        var accessor = new Mock<IHttpContextAccessor>();

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/applications";

        // identity with no auth -> IsAuthenticated = false
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

        accessor
            .Setup(a => a.HttpContext)
            .Returns(httpContext);

        var behavior = new AllowedStatusesPopulationBehavior<GetApplicationsQuery, IEnumerable<ApplicationResponse>>(accessor.Object);

        var query = new GetApplicationsQuery();

        var nextCalled = false;
        Task<IEnumerable<ApplicationResponse>> next()
        {
            nextCalled = true;
            return Task.FromResult<IEnumerable<ApplicationResponse>>([]);
        }

        // Act
        await behavior.Handle(query, next, CancellationToken.None);

        // Assert
        nextCalled.ShouldBeTrue();
        query.AllowedStatuses.ShouldBeEmpty();
    }

    [Fact]
    public async Task PopulatesAllowedStatuses_FromApplicationClaim_WhenPathContainsApplications()
    {
        // Arrange
        var accessor = new Mock<IHttpContextAccessor>();

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/applications";

        var claims = new[]
        {
            new Claim("allowed_statuses/projectrecord", "AppStatus")
        };

        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

        accessor
            .Setup(a => a.HttpContext)
            .Returns(httpContext);

        var behavior = new AllowedStatusesPopulationBehavior<GetApplicationsQuery, IEnumerable<ApplicationResponse>>(accessor.Object);

        var query = new GetApplicationsQuery(); // AllowedStatuses initially empty

        var called = false;

        Task<IEnumerable<ApplicationResponse>> next()
        {
            called = true;
            return Task.FromResult<IEnumerable<ApplicationResponse>>([]);
        }

        // Act
        await behavior.Handle(query, next, CancellationToken.None);

        // Assert
        called.ShouldBeTrue();
        query.AllowedStatuses.ShouldNotBeNull();
        query.AllowedStatuses.ShouldContain("AppStatus");
    }

    [Fact]
    public async Task DoesNotOverride_WhenAllowedStatusesAlreadyProvided()
    {
        // Arrange
        var accessor = new Mock<IHttpContextAccessor>();

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/applications";

        var claims = new[]
        {
            new Claim("allowed_statuses/projectrecord", "AppStatus")
        };

        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

        accessor
            .Setup(a => a.HttpContext)
            .Returns(httpContext);

        var behavior = new AllowedStatusesPopulationBehavior<GetApplicationsQuery, IEnumerable<ApplicationResponse>>(accessor.Object);

        var query = new GetApplicationsQuery
        {
            AllowedStatuses = ["PreExisting"]
        };

        var nextCalled = false;
        Task<IEnumerable<ApplicationResponse>> next()
        {
            nextCalled = true;
            return Task.FromResult<IEnumerable<ApplicationResponse>>([]);
        }

        // Act
        await behavior.Handle(query, next, CancellationToken.None);

        // Assert
        nextCalled.ShouldBeTrue();
        query.AllowedStatuses.ShouldHaveSingleItem();
        query.AllowedStatuses.ShouldContain("PreExisting");
    }

    [Fact]
    public async Task UsesDocumentClaim_WhenPathContainsDocuments()
    {
        // Arrange
        var accessor = new Mock<IHttpContextAccessor>();

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/documents/list";

        var claims = new[]
        {
            new Claim("allowed_statuses/document", "DocA"),
            new Claim("allowed_statuses/document", "DocB")
        };
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

        accessor
            .Setup(a => a.HttpContext)
            .Returns(httpContext);

        var behavior = new AllowedStatusesPopulationBehavior<GetModificationDocumentsQuery, IEnumerable<ModificationDocumentDto>>(accessor.Object);

        var query = new GetModificationDocumentsQuery { ProjectModificationId = Guid.NewGuid(), ProjectRecordId = "p1" };

        var nextCalled = false;
        Task<IEnumerable<ModificationDocumentDto>> next()
        {
            nextCalled = true;
            return Task.FromResult<IEnumerable<ModificationDocumentDto>>([]);
        }

        // Act
        await behavior.Handle(query, next, CancellationToken.None);

        // Assert
        nextCalled.ShouldBeTrue();
        query.AllowedStatuses.Count.ShouldBe(2);
        query.AllowedStatuses.ShouldContain("DocA");
        query.AllowedStatuses.ShouldContain("DocB");
    }

    [Fact]
    public async Task PopulatesAllowedStatuses_FromModificationClaim_WhenPathContainsProjectModifications()
    {
        // Arrange
        var accessor = new Mock<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/projectmodifications/list";

        var claims = new[] { new Claim("allowed_statuses/modification", "ModStatus") };
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

        accessor
            .Setup(a => a.HttpContext)
            .Returns(httpContext);

        var behavior = new AllowedStatusesPopulationBehavior<GetModificationsQuery, ModificationSearchResponse>(accessor.Object);

        var query = new GetModificationsQuery(new ModificationSearchRequest(), 1, 10, "", "");

        var nextCalled = false;
        Task<ModificationSearchResponse> next()
        {
            nextCalled = true;
            return Task.FromResult(new ModificationSearchResponse());
        }

        // Act
        await behavior.Handle(query, next, CancellationToken.None);

        // Assert
        nextCalled.ShouldBeTrue();
        query.AllowedStatuses.ShouldHaveSingleItem();
        query.AllowedStatuses[0].ShouldBe("ModStatus");
    }

    [Fact]
    public async Task AdminUser_BypassesPopulation()
    {
        // Arrange
        var accessor = new Mock<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/applications";

        // Add role claim for system_administrator
        var claims = new[]
        {
            new Claim(ClaimTypes.Role, "system_administrator"),
            // also include a statuses claim which should be ignored because user is admin
            new Claim("allowed_statuses/projectrecord", "ShouldBeIgnored")
        };

        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

        accessor
            .Setup(a => a.HttpContext)
            .Returns(httpContext);

        var behavior = new AllowedStatusesPopulationBehavior<GetApplicationsQuery, IEnumerable<ApplicationResponse>>(accessor.Object);

        var query = new GetApplicationsQuery();

        var nextCalled = false;
        Task<IEnumerable<ApplicationResponse>> next()
        {
            nextCalled = true;
            return Task.FromResult<IEnumerable<ApplicationResponse>>([]);
        }

        // Act
        await behavior.Handle(query, next, CancellationToken.None);

        // Assert
        nextCalled.ShouldBeTrue();
        // Admin bypass means AllowedStatuses remains empty
        query.AllowedStatuses.ShouldBeEmpty();
    }
}