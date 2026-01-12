using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ReviewBodyServiceTests;

/// <summary>
///     Covers the tests for GetReviewBodies method
/// </summary>
public class GetReviewBodiesTests : TestServiceBase<ReviewBodyService>
{
    private readonly RegulatoryBodyRepository _reviewBodyRepository;
    private readonly RspContext _context;

    public GetReviewBodiesTests()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
        _reviewBodyRepository = new RegulatoryBodyRepository(_context);
    }

    [Theory, InlineAutoData(5)]
    public async Task Returns_Correct_ReviewBodies(int records, Generator<RegulatoryBody> generator)
    {
        // Arrange
        Mocker.Use<IRegulatoryBodyRepository>(_reviewBodyRepository);
        Sut = Mocker.CreateInstance<ReviewBodyService>();

        // Seed data using number of records to seed
        await TestData.SeedData(_context, generator, records);

        // Act
        var result = await Sut.GetReviewBodies(1, 100, nameof(ReviewBodyDto.RegulatoryBodyName), "asc", null);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(records);
    }
}