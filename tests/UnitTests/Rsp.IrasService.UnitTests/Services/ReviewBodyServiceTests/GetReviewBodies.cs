using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyServiceTests;

/// <summary>
///     Covers the tests for GetReviewBodies method
/// </summary>
public class GetReviewBodiesTests : TestServiceBase<ReviewBodyService>
{
    private readonly RegulatoryBodyRepository _reviewBodyRepository;
    private readonly IrasContext _context;

    public GetReviewBodiesTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
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