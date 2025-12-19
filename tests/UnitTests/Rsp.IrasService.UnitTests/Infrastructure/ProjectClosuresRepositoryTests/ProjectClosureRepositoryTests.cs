using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;

namespace Rsp.IrasService.UnitTests.Infrastructure.ProjectClosuresRepositoryTests;

public class ProjectClosureRepositoryTests
{
    private readonly IrasContext _context;
    private readonly ProjectClosureRepository _applicationRepository;

    public ProjectClosureRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
           .Options;

        _context = new IrasContext(options);
        _applicationRepository = new ProjectClosureRepository(_context);
    }

    [Fact]
    public async Task CreateProjectClosure_Persists_And_Returns_Entity()
    {
        var projectClosures = new ProjectClosure
        {
            Id = "1",
            ProjectRecordId = "123",
            ShortProjectTitle = "Abc",
            ClosureDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            SentToSponsorDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            DateActioned = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            UserId = "",
            Status = "With sponsor",
            IrasId = 1,
            CreatedBy = "A",
            UpdatedBy = "A"
        };

        //act
        var create = await _applicationRepository.CreateProjectClosure(projectClosures);

        // Assert
        Assert.NotNull(create);
        Assert.Equal("With sponsor", create.Status);
        Assert.Equal("Abc", create.ShortProjectTitle);
    }

    //[Fact]
    //public async Task GetProjectClosures_Returns_Expected_Result()
    //{
    //    var projectClosures = new ProjectClosure
    //    {
    //        Id = "1",
    //        ProjectRecordId = "123",
    //        ShortProjectTitle = "Abc",
    //        ClosureDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
    //        SentToSponsorDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
    //        DateActioned = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
    //        UserId = "",
    //        Status = "With sponsor",
    //        IrasId = 1,
    //        CreatedBy = "A",
    //        UpdatedBy = "A"
    //    };

    //    //act
    //    var create = await _applicationRepository.GetProjectClosure(projectClosures.ProjectRecordId);

    //    // Assert
    //    Assert.NotNull(create);
    //    Assert.Equal("With sponsor", create.Status);
    //    Assert.Equal("Abc", create.ShortProjectTitle);
    //}
}