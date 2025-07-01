using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyAuditTrailServiceTests;

public class GetAuditTrailForReviewBody : TestServiceBase<ReviewBodyAuditTrailService>
{
    private readonly RegulatoryBodyAuditTrailRepository _repo;
    private readonly IrasContext _context;

    public GetAuditTrailForReviewBody()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _repo = new RegulatoryBodyAuditTrailRepository(_context);
    }

    [Theory, InlineAutoData(10)]
    public async Task Returns_Correct_AuditTrails(int records, Generator<RegulatoryBodyAuditTrail> generator)
    {
        // Arrange
        Mocker.Use<IRegulatoryBodyAuditTrailRepository>(_repo);
        Sut = Mocker.CreateInstance<ReviewBodyAuditTrailService>();

        // Seed data using number of records to seed
        var testData = await TestData.SeedData(_context, generator, records);

        var id = testData.FirstOrDefault()!.RegulatoryBodyId;
        var expectedData = _context.RegulatoryBodiesAuditTrail.Where(x => x.RegulatoryBodyId == id);

        // Act
        var result = await Sut.GetAuditTrailForReviewBody(id, 0, 10);
        var resultItems = result.Items;

        // Assert
        result.ShouldNotBeNull();
        result.Items.Count().ShouldBe(await expectedData.CountAsync());
    }
}