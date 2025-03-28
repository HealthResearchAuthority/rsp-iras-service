using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyAuditTrailServiceTests;

public class GenerateAuditTrailDtoFromReviewBody : TestServiceBase<ReviewBodyAuditTrailService>
{
    private readonly ReviewBodyAuditTrailRepository _repo;
    private readonly IrasContext _context;

    public GenerateAuditTrailDtoFromReviewBody()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _repo = new ReviewBodyAuditTrailRepository(_context);
    }

    [Theory, AutoData]
    public void Generates_Correct_AuditTrails_For_Create(ReviewBodyDto dto, string userId)
    {
        // Arrange
        Mocker.Use<IReviewBodyAuditTrailRepository>(_repo);
        Sut = Mocker.CreateInstance<ReviewBodyAuditTrailService>();

        // Act
        var result = Sut.GenerateAuditTrailDtoFromReviewBody(dto, userId, ReviewBodyAuditTrailActions.Create);

        // Assert
        result.Count().ShouldBe(1);
        result.FirstOrDefault()!.ReviewBodyId.ShouldBe(dto.Id);
        result.FirstOrDefault()!.User.ShouldBe(userId);
    }

    [Theory, AutoData]
    public void Generates_Correct_AuditTrails_For_Disable(ReviewBodyDto dto, string userId)
    {
        // Arrange
        Mocker.Use<IReviewBodyAuditTrailRepository>(_repo);
        Sut = Mocker.CreateInstance<ReviewBodyAuditTrailService>();

        // Act
        var result = Sut.GenerateAuditTrailDtoFromReviewBody(dto, userId, ReviewBodyAuditTrailActions.Disable);

        // Assert
        result.Count().ShouldBe(1);
        result.FirstOrDefault()!.ReviewBodyId.ShouldBe(dto.Id);
        result.FirstOrDefault()!.User.ShouldBe(userId);
    }

    [Fact]
    public void Generates_Correct_AuditTrails_For_Update()
    {
        // Arrange
        Mocker.Use<IReviewBodyAuditTrailRepository>(_repo);
        Sut = Mocker.CreateInstance<ReviewBodyAuditTrailService>();

        var dto = new ReviewBodyDto
        {
            Id = Guid.NewGuid(),
            OrganisationName = "New Org",
            Description = "Updated description",
            EmailAddress = "new@example.com"
        };

        var oldDto = new ReviewBodyDto
        {
            Id = dto.Id,
            OrganisationName = "Old Org",
            Description = "Old description",
            EmailAddress = "old@example.com"
        };

        var userId = "User789";

        // Act
        var result = Sut.GenerateAuditTrailDtoFromReviewBody(dto, userId, ReviewBodyAuditTrailActions.Update, oldDto);

        // Assert
        result.Count().ShouldBe(3);
        result.ShouldContain(x => x.Description == "OrganisationName was changed from Old Org to New Org");
        result.ShouldContain(x => x.Description == "Description was changed from Old description to Updated description");
        result.ShouldContain(x => x.Description == "EmailAddress was changed from old@example.com to new@example.com");
    }
}