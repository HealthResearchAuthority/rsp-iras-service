using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ReviewBodyAuditTrailServiceTests;

public class GetAuditTrailForReviewBody : TestServiceBase<ReviewBodyAuditTrailService>
{
    private readonly RegulatoryBodyAuditTrailRepository _repo;
    private readonly RspContext _context;

    public GetAuditTrailForReviewBody()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
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