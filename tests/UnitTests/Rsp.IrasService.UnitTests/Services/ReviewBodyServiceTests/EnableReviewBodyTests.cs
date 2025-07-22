using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyServiceTests;

public class EnableReviewBodyTests : TestServiceBase<ReviewBodyService>

{
    private readonly IrasContext _context;
    private readonly RegulatoryBodyRepository _reviewBodyRepository;

    public EnableReviewBodyTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _reviewBodyRepository = new RegulatoryBodyRepository(_context);
    }

    [Theory, AutoData]
    public async Task Enable_ReviewBody_Correctly(int records, Generator<RegulatoryBody> generator)
    {
        // Arrange
        Mocker.Use<IRegulatoryBodyRepository>(_reviewBodyRepository);
        Sut = Mocker.CreateInstance<ReviewBodyService>();

        // Seed data using number of records to seed
        await TestData.SeedData(_context, generator, records);
        var reviewBodies = await Sut.GetReviewBodies(1, 100, nameof(ReviewBodyDto.RegulatoryBodyName), "asc", null);

        // Act
        var result = await Sut.EnableReviewBody(reviewBodies.ReviewBodies.First().Id);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ReviewBodyDto>();
    }
}