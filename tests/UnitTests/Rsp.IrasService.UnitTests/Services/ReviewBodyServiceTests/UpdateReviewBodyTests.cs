using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ReviewBodyServiceTests;

public class UpdateReviewBodyTests : TestServiceBase<ReviewBodyService>

{
    private readonly IrasContext _context;
    private readonly ReviewBodyRepository _reviewBodyRepository;

    public UpdateReviewBodyTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _reviewBodyRepository = new ReviewBodyRepository(_context);
    }

    [Theory, AutoData]
    public async Task Updates_ReviewBody_Correctly(int records, Generator<RegulatoryBody> generator)
    {
        // Arrange
        Mocker.Use<IReviewBodyRepository>(_reviewBodyRepository);
        Sut = Mocker.CreateInstance<ReviewBodyService>();

        // Act

        // Seed data using number of records to seed
        await TestData.SeedData(_context, generator, records);
        var reviewBodies = await Sut.GetReviewBodies(1, 100, null);

        // Act
        var result = await Sut.UpdateReviewBody(reviewBodies.ReviewBodies.First());

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ReviewBodyDto>();
    }
}