using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ReviewBodyServiceTests;

public class UpdateReviewBodyTests : TestServiceBase<ReviewBodyService>

{
    private readonly RspContext _context;
    private readonly RegulatoryBodyRepository _reviewBodyRepository;

    public UpdateReviewBodyTests()
    {
        var options = new DbContextOptionsBuilder<RspContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new RspContext(options);
        _reviewBodyRepository = new RegulatoryBodyRepository(_context);
    }

    [Theory, AutoData]
    public async Task Updates_ReviewBody_Correctly(int records, Generator<RegulatoryBody> generator)
    {
        // Arrange
        Mocker.Use<IRegulatoryBodyRepository>(_reviewBodyRepository);
        Sut = Mocker.CreateInstance<ReviewBodyService>();

        // Act

        // Seed data using number of records to seed
        await TestData.SeedData(_context, generator, records);
        var reviewBodies = await Sut.GetReviewBodies(1, 100, nameof(ReviewBodyDto.RegulatoryBodyName), "asc", null);

        // Act
        var result = await Sut.UpdateReviewBody(reviewBodies.ReviewBodies.First());

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<ReviewBodyDto>();
    }
}