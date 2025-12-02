using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.UnitTests.Infrastructure.HelpersTests;

public class ReviewBodyAuditTrailHandlerTests
{
    private readonly ReviewBodyAuditTrailHandler _handler;

    public ReviewBodyAuditTrailHandlerTests()
    {
        _handler = new ReviewBodyAuditTrailHandler();
    }

    [Fact]
    public void CanHandle_ShouldReturnTrue_WhenEntityIsReviewBody()
    {
        // Arrange
        var reviewBody = new RegulatoryBody();

        // Act
        var result = _handler.CanHandle(reviewBody);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanHandle_ShouldReturnFalse_WhenEntityIsNotReviewBody()
    {
        // Arrange
        var nonReviewBody = new object();

        // Act
        var result = _handler.CanHandle(nonReviewBody);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GenerateAuditTrails_ShouldReturnEmptyList_WhenEntityIsNotReviewBody()
    {
        // Arrange
        var entity = new DocumentType { Id = Guid.NewGuid(), Name = "name" };
        var systemAdminEmail = "adminEmail";

        var entityEntry = MockEntityEntry(entity);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GenerateAuditTrails_ShouldGenerateAuditTrailForAddedEntity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var reviewBody = new RegulatoryBody { Id = id, EmailAddress = "test@example.com", RegulatoryBodyName = "name" };
        var systemAdminEmail = "adminEmail";

        var entityEntry = MockEntityEntry(reviewBody);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        var auditTrail = Assert.Single(result);
        Assert.Equal("name was created", auditTrail.Description);
        Assert.Equal(id, auditTrail.RegulatoryBodyId);
        Assert.Equal(systemAdminEmail, auditTrail.User);
    }

    [Fact]
    public void GenerateAuditTrails_ShouldGenerateAuditTrailForModifiedStatus()
    {
        // Arrange
        var id = Guid.NewGuid();
        var reviewBody = new RegulatoryBody { Id = id, EmailAddress = "test@example.com", RegulatoryBodyName = "name", IsActive = false };
        var modifiedReviewBody = new RegulatoryBody { Id = id, EmailAddress = "test@example.com", RegulatoryBodyName = "name", IsActive = true };

        var systemAdminEmail = "adminEmail";

        var entityEntry = MockEntityEntry(reviewBody, modifiedReviewBody);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        var auditTrail = Assert.Single(result);
        Assert.Equal("name was enabled", auditTrail.Description);
        Assert.Equal(id, auditTrail.RegulatoryBodyId);
        Assert.Equal(systemAdminEmail, auditTrail.User);
    }

    [Fact]
    public void GenerateAuditTrails_ShouldGenerateAuditTrailForModifiedProperty()
    {
        // Arrange
        var id = Guid.NewGuid();
        var reviewBody = new RegulatoryBody { Id = id, EmailAddress = "test@example.com", RegulatoryBodyName = "name" };
        var modifiedReviewBody = new RegulatoryBody { Id = id, EmailAddress = "test@example.com", RegulatoryBodyName = "new name" };
        var systemAdminEmail = "adminEmail";

        var entityEntry = MockEntityEntry(reviewBody, modifiedReviewBody);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        var auditTrail = Assert.Single(result);
        Assert.Equal("RegulatoryBodyName was changed from name to new name", auditTrail.Description);
        Assert.Equal(id, auditTrail.RegulatoryBodyId);
        Assert.Equal(systemAdminEmail, auditTrail.User);
    }

    [Fact]
    public void GenerateAuditTrails_ShouldGenerateAuditTrailForModifiedListProperty()
    {
        // Arrange
        var id = Guid.NewGuid();
        var reviewBody = new RegulatoryBody
        {
            Id = id,
            EmailAddress = "test@example.com",
            RegulatoryBodyName = "name",
            Countries = ["country1", "country2"],
            IsActive = false
        };
        var modifiedReviewBody = new RegulatoryBody
        {
            Id = id,
            EmailAddress = "test@example.com",
            RegulatoryBodyName = "name",
            Countries = ["country1"],
            IsActive = false
        };
        var systemAdminEmail = "adminEmail";

        var entityEntry = MockEntityEntry(reviewBody, modifiedReviewBody);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        var auditTrail = Assert.Single(result);
        Assert.Equal("Countries was changed from country1, country2 to country1", auditTrail.Description);
        Assert.Equal(id, auditTrail.RegulatoryBodyId);
        Assert.Equal(systemAdminEmail, auditTrail.User);
    }

    [Fact]
    public void GenerateAuditTrails_ShouldSkipUnchangedProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var reviewBody = new RegulatoryBody { Id = id, EmailAddress = "test@example.com", RegulatoryBodyName = "name" };
        var modifiedReviewBody = new RegulatoryBody { Id = id, EmailAddress = "test@example.com", RegulatoryBodyName = "name" };
        var systemAdminEmail = "adminEmail";

        var entityEntry = MockEntityEntry(reviewBody, modifiedReviewBody);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        Assert.Empty(result);
    }

    private static EntityEntry MockEntityEntry(object entity, object? modifiedEntity = null)
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new IrasContext(options);

        var entry = context.Entry(entity);

        if (modifiedEntity == null)
        {
            entry.State = EntityState.Added;
        }
        else
        {
            entry.State = EntityState.Modified;
            entry.CurrentValues.SetValues(modifiedEntity);
        }

        return entry;
    }
}