using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Helpers;

namespace Rsp.IrasService.UnitTests.Infrastructure.HelpersTests;

public class SponsorOrganisationAuditTrailHandlerTests
{
    private readonly SponsorOrganisationAuditTrailHandler _handler;

    public SponsorOrganisationAuditTrailHandlerTests()
    {
        _handler = new SponsorOrganisationAuditTrailHandler();
    }

    [Fact]
    public void CanHandle_ShouldReturnTrue_WhenEntityIsSponsorOrganisation()
    {
        // Arrange
        var sponsorOrganisation = new SponsorOrganisation();

        // Act
        var result = _handler.CanHandle(sponsorOrganisation);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void CanHandle_ShouldReturnFalse_WhenEntityIsNotSponsorOrganisation()
    {
        // Arrange
        var nonSponsorOrganisation = new object();

        // Act
        var result = _handler.CanHandle(nonSponsorOrganisation);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GenerateAuditTrails_ShouldReturnEmptyList_WhenEntityIsNotSponsorOrganisation()
    {
        // Arrange
        var entity = new DocumentType { Id = Guid.NewGuid(), Name = "name" };
        var systemAdminEmail = "adminEmail";
        var entityEntry = MockEntityEntry(entity);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GenerateAuditTrails_ShouldGenerateAuditTrailForAddedEntity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sponsorOrganisation = new SponsorOrganisation { Id = id, RtsId = "name" };
        var systemAdminEmail = "adminEmail";
        var entityEntry = MockEntityEntry(sponsorOrganisation);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        var auditTrail = result.Single();
        auditTrail.Description.ShouldBe("name created");
    }

    [Fact]
    public void GenerateAuditTrails_ShouldGenerateAuditTrailForModifiedStatus()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sponsorOrganisation = new SponsorOrganisation { Id = id, RtsId = "name", IsActive = false };
        var modifiedSponsorOrganisation = new SponsorOrganisation { Id = id, RtsId = "name", IsActive = true };
        var systemAdminEmail = "adminEmail";
        var entityEntry = MockEntityEntry(sponsorOrganisation, modifiedSponsorOrganisation);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        var auditTrail = result.Single();
        auditTrail.Description.ShouldBe("name enabled");
        auditTrail.SponsorOrganisationId.ShouldBe(id);
        auditTrail.User.ShouldBe(systemAdminEmail);
    }

    [Fact]
    public void GenerateAuditTrails_ShouldSkipUnchangedProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sponsorOrganisation = new SponsorOrganisation { Id = id, RtsId = "name" };
        var modifiedSponsorOrganisation = new SponsorOrganisation { Id = id, RtsId = "name" };
        var systemAdminEmail = "adminEmail";
        var entityEntry = MockEntityEntry(sponsorOrganisation, modifiedSponsorOrganisation);

        // Act
        var result = _handler.GenerateAuditTrails(entityEntry, systemAdminEmail);

        // Assert
        result.ShouldBeEmpty();
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