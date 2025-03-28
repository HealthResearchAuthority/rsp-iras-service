using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyAuditTrailServiceTests;

public class LogAuditTrail : TestServiceBase<ReviewBodyAuditTrailService>
{
    private readonly ReviewBodyAuditTrailRepository _repo;
    private readonly IrasContext _context;

    public LogAuditTrail()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _repo = new ReviewBodyAuditTrailRepository(_context);
    }

    [Theory, AutoData]
    public async Task Creates_AuditTrail_Correctly(IEnumerable<ReviewBodyAuditTrailDto> records)
    {
        // Arrange
        Mocker.Use<IReviewBodyAuditTrailRepository>(_repo);
        Sut = Mocker.CreateInstance<ReviewBodyAuditTrailService>();

        // Act
        var result = await Sut.LogRecords(records);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(records.Count());
    }
}