using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;

namespace Rsp.IrasService.UnitTests.Infrastructure.ProjectClosuresRepositoryTests;

public class ProjectClosureRepositoryTests
{
    private readonly IrasContext _context;
    private readonly ProjectClosureRepository _repository;

    public ProjectClosureRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
           .Options;

        _context = new IrasContext(options);
        _repository = new ProjectClosureRepository(_context);
    }

    [Fact]
    public async Task CreateProjectClosure_Persists_And_Returns_Entity()
    {
        var projectClosures = new ProjectClosure
        {
            Id = Guid.NewGuid(),
            ProjectClosureNumber = 1,
            TransactionId = "C1234/1",
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
        var create = await _repository.CreateProjectClosure(projectClosures);

        // Assert
        Assert.NotNull(create);
        Assert.Equal("With sponsor", create.Status);
        Assert.Equal("Abc", create.ShortProjectTitle);
    }

    private async Task<Guid> SeedScenarioAsync(
            string mainRtsId = "RTS-ABC",
            IEnumerable<ProjectClosure>? closures = null,
            Dictionary<string, string>? recordIdToRtsMap = null,
            string defaultCategory = "SponsorOrganisation",
            string defaultSection = "Core",
            string defaultVersionId = "v1")
    {
        // Create sponsor user with RTS id
        var userId = Guid.NewGuid();
        _context.SponsorOrganisationsUsers.Add(new SponsorOrganisationUser
        {
            Id = userId,
            RtsId = mainRtsId,
            SponsorRole = "Sponsor"
        });

        // Default closures if none provided
        var list = (closures?.ToList() ?? new List<ProjectClosure>
        {
            new ProjectClosure
            {
                Id = Guid.NewGuid(),
                ProjectRecordId = "PR-1",
                ProjectClosureNumber = 1,
                TransactionId = "C1234/1",
                ShortProjectTitle = "Alpha",
                Status = "With sponsor",
                IrasId = 100,
                SentToSponsorDate = DateTime.UtcNow.AddDays(-3),
                ClosureDate = DateTime.UtcNow.AddDays(-2),
                DateActioned = DateTime.UtcNow.AddDays(-2),
                UserId = "seed-user-1",
                CreatedBy = "seed",
                UpdatedBy = "seed"
            },
            new ProjectClosure
            {
                Id = Guid.NewGuid(),
                ProjectRecordId = "PR-2",
                ShortProjectTitle = "Bravo",
                ProjectClosureNumber = 1,
                TransactionId = "C1234/1",
                Status = "Closed",
                IrasId = 200,
                SentToSponsorDate = DateTime.UtcNow.AddDays(-2),
                ClosureDate = DateTime.UtcNow.AddDays(-1),
                DateActioned = DateTime.UtcNow.AddDays(-1),
                UserId = "seed-user-2",
                CreatedBy = "seed",
                UpdatedBy = "seed"
            },
            new ProjectClosure
            {
                Id = Guid.NewGuid(),
                ProjectRecordId = "PR-3",
                ProjectClosureNumber = 1,
                TransactionId = "C1234/1",
                ShortProjectTitle = "Charlie",
                Status = "With sponsor",
                IrasId = 300,
                SentToSponsorDate = DateTime.UtcNow.AddDays(-1),
                ClosureDate = DateTime.UtcNow,
                DateActioned = DateTime.UtcNow,
                UserId = "seed-user-3",
                CreatedBy = "seed",
                UpdatedBy = "seed"
            }
        });

        _context.ProjectClosures.AddRange(list);

        // Map each ProjectRecordId to an RTS id (defaults to the main user's RTS id)
        var map = recordIdToRtsMap ?? list.ToDictionary(c => c.ProjectRecordId, _ => mainRtsId);

        foreach (var kvp in map)
        {
            _context.ProjectRecordAnswers.Add(new ProjectRecordAnswer
            {
                // Required properties (string types per your model)
                UserId = Guid.NewGuid().ToString(),
                Category = defaultCategory,
                Section = defaultSection,
                VersionId = defaultVersionId,

                // Join/filter data
                ProjectRecordId = kvp.Key,
                QuestionId = ProjectRecordConstants.SponsorOrganisation,
                Response = kvp.Value
            });
        }

        foreach (var c in list)
        {
            if (!await _context.ProjectRecords.AnyAsync(r => r.Id == c.ProjectRecordId))
            {
                _context.ProjectRecords.Add(new ProjectRecord
                {
                    Id = c.ProjectRecordId,
                    Status = ProjectRecordStatus.Active,
                    CreatedBy = "seed",
                    UpdatedBy = "seed",
                    CreatedDate = DateTime.UtcNow.AddDays(-7),
                    UpdatedDate = DateTime.UtcNow.AddDays(-7),
                    FullProjectTitle = $"Full title for {c.ProjectRecordId}",
                    ShortProjectTitle = c.ShortProjectTitle ?? $"Short {c.ProjectRecordId}",
                    UserId = c.UserId ?? Guid.NewGuid().ToString(),
                });
            }
        }

        await _context.SaveChangesAsync();
        return userId;
    }

    // ---------------------------------------------------------------------
    // Filter: should return only closures linked to user's RTS id
    // ---------------------------------------------------------------------
    [Fact]
    public async Task GetProjectClosuresBySponsorOrganisationUser_ShouldReturnOnlyClosuresLinkedToUsersRtsId()
    {
        var closures = new[]
        {
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-1", ShortProjectTitle = "Alpha",   Status = "Open",          IrasId = 100, SentToSponsorDate = DateTime.UtcNow.AddDays(-3), ClosureDate = DateTime.UtcNow.AddDays(-2), DateActioned = DateTime.UtcNow.AddDays(-2), UserId = "u1", CreatedBy = "seed", UpdatedBy = "seed", TransactionId="C2414/1", ProjectClosureNumber=1 },
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-2", ShortProjectTitle = "Bravo",   Status = "Closed",        IrasId = 200, SentToSponsorDate = DateTime.UtcNow.AddDays(-2), ClosureDate = DateTime.UtcNow.AddDays(-1), DateActioned = DateTime.UtcNow.AddDays(-1), UserId = "u2", CreatedBy = "seed", UpdatedBy = "seed",TransactionId="C2414/1", ProjectClosureNumber=1 },
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-3", ShortProjectTitle = "Charlie", Status = "With sponsor",  IrasId = 300, SentToSponsorDate = DateTime.UtcNow.AddDays(-1), ClosureDate = DateTime.UtcNow,            DateActioned = DateTime.UtcNow,            UserId = "u3", CreatedBy = "seed", UpdatedBy = "seed",TransactionId="C2414/1", ProjectClosureNumber=1 }
        };

        var map = new Dictionary<string, string>
        {
            ["PR-1"] = "RTS-ABC", // include
            ["PR-2"] = "RTS-ABC", // include
            ["PR-3"] = "RTS-XYZ"  // exclude
        };

        var userId = await SeedScenarioAsync(mainRtsId: "RTS-ABC", closures: closures, recordIdToRtsMap: map);

        var searchQuery = new ProjectClosuresSearchRequest { SearchTerm = null };

        var list = _repository.GetProjectClosuresBySponsorOrganisationUser(
            searchQuery, pageNumber: 1, pageSize: 10, sortField: nameof(ProjectClosure.IrasId), sortDirection: "asc", sponsorOrganisationUserId: userId).ToList();

        var count = _repository.GetProjectClosuresBySponsorOrganisationUserCount(searchQuery, userId);

        list.Count.ShouldBe(2);
        count.ShouldBe(2);
        list.Select(x => x.ProjectRecordId).ShouldBe(new[] { "PR-1", "PR-2" }, ignoreOrder: false);
    }

    // ---------------------------------------------------------------------
    // Sorting by supported fields/directions
    // ---------------------------------------------------------------------
    [Theory]
    [InlineData(nameof(ProjectClosure.ShortProjectTitle), "asc")]
    [InlineData(nameof(ProjectClosure.ShortProjectTitle), "desc")]
    [InlineData(nameof(ProjectClosure.IrasId), "asc")]
    [InlineData(nameof(ProjectClosure.IrasId), "desc")]
    [InlineData(nameof(ProjectClosure.SentToSponsorDate), "asc")]
    [InlineData(nameof(ProjectClosure.SentToSponsorDate), "desc")]
    [InlineData(nameof(ProjectClosure.ClosureDate), "asc")]
    [InlineData(nameof(ProjectClosure.ClosureDate), "desc")]
    [InlineData(nameof(ProjectClosure.Status), "asc")]
    [InlineData(nameof(ProjectClosure.Status), "desc")]
    public async Task GetProjectClosuresBySponsorOrganisationUser_ShouldSortByFieldAndDirection(string sortField, string sortDirection)
    {
        var closures = new[]
        {
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-1", ShortProjectTitle = "Bravo", Status = "Closed", IrasId = 200, SentToSponsorDate = new DateTime(2025,1,2,0,0,0,DateTimeKind.Utc), ClosureDate = new DateTime(2025,1,3,0,0,0,DateTimeKind.Utc), DateActioned = new DateTime(2025,1,3,0,0,0,DateTimeKind.Utc), UserId = "u1", CreatedBy = "seed", UpdatedBy = "seed",TransactionId="C2414/1", ProjectClosureNumber=1 },
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-2", ShortProjectTitle = "Alpha", Status = "Open",   IrasId = 100, SentToSponsorDate = new DateTime(2025,1,1,0,0,0,DateTimeKind.Utc), ClosureDate = new DateTime(2025,1,2,0,0,0,DateTimeKind.Utc), DateActioned = new DateTime(2025,1,2,0,0,0,DateTimeKind.Utc), UserId = "u2", CreatedBy = "seed", UpdatedBy = "seed",TransactionId="C2414/1", ProjectClosureNumber=1 }
        };

        var userId = await SeedScenarioAsync(closures: closures);

        var searchQuery = new ProjectClosuresSearchRequest { SearchTerm = null };

        var list = _repository.GetProjectClosuresBySponsorOrganisationUser(
            searchQuery, pageNumber: 1, pageSize: 10, sortField, sortDirection, userId).ToList();

        list.Count.ShouldBe(2);

        object firstKey = sortField switch
        {
            nameof(ProjectClosure.ShortProjectTitle) => list.First().ShortProjectTitle!,
            nameof(ProjectClosure.IrasId) => list.First().IrasId!,
            nameof(ProjectClosure.SentToSponsorDate) => list.First().SentToSponsorDate,
            nameof(ProjectClosure.ClosureDate) => list.First().ClosureDate,
            nameof(ProjectClosure.Status) => list.First().Status!,
            _ => throw new InvalidOperationException("Unsupported sort field in test")
        };

        object expectedFirstKey = sortField switch
        {
            nameof(ProjectClosure.ShortProjectTitle) => sortDirection == "asc" ? "Alpha" : "Bravo",
            nameof(ProjectClosure.IrasId) => sortDirection == "asc" ? 100 : 200,
            nameof(ProjectClosure.SentToSponsorDate) => sortDirection == "asc"
                ? new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                : new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc),
            nameof(ProjectClosure.ClosureDate) => sortDirection == "asc"
                ? new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc)
                : new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc),
            nameof(ProjectClosure.Status) => sortDirection == "asc" ? "Closed" : "Open",
            _ => throw new InvalidOperationException("Unsupported sort field in test")
        };

        firstKey.ShouldBe(expectedFirstKey);
    }

    // ---------------------------------------------------------------------
    // Pagination
    // ---------------------------------------------------------------------
    [Fact]
    public async Task GetProjectClosuresBySponsorOrganisationUser_ShouldPaginate()
    {
        var userId = await SeedScenarioAsync(); // default Alpha, Bravo, Charlie

        var searchQuery = new ProjectClosuresSearchRequest { SearchTerm = null };
        var sortField = nameof(ProjectClosure.ShortProjectTitle);
        var sortDirection = "asc";
        var pageNumber = 2;
        var pageSize = 1;

        // Asc: Alpha, Bravo, Charlie → page 2 → "Bravo"
        var page = _repository.GetProjectClosuresBySponsorOrganisationUser(
            searchQuery, pageNumber, pageSize, sortField, sortDirection, userId).ToList();

        var total = _repository.GetProjectClosuresBySponsorOrganisationUserCount(searchQuery, userId);

        page.Count.ShouldBe(1);
        page[0].ShortProjectTitle.ShouldBe("Bravo");
        total.ShouldBe(3);
    }

    // ---------------------------------------------------------------------
    // SearchTerm filter on IrasId (contains)
    // ---------------------------------------------------------------------
    [Fact]
    public async Task GetProjectClosuresBySponsorOrganisationUserCount_ShouldRespectSearchTerm_On_IrasId()
    {
        var closures = new[]
        {
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-1", ShortProjectTitle = "A", Status = "Open",   IrasId = 101, SentToSponsorDate = DateTime.UtcNow, ClosureDate = DateTime.UtcNow, DateActioned = DateTime.UtcNow, UserId = "u1", CreatedBy = "seed", UpdatedBy = "seed",TransactionId="C2414/1", ProjectClosureNumber=1 },
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-2", ShortProjectTitle = "B", Status = "Closed", IrasId = 202, SentToSponsorDate = DateTime.UtcNow, ClosureDate = DateTime.UtcNow, DateActioned = DateTime.UtcNow, UserId = "u2", CreatedBy = "seed", UpdatedBy = "seed",TransactionId="C2414/1", ProjectClosureNumber=1 }
        };

        var userId = await SeedScenarioAsync(closures: closures);

        var searchQuery = new ProjectClosuresSearchRequest { SearchTerm = "202" };

        var total = _repository.GetProjectClosuresBySponsorOrganisationUserCount(searchQuery, userId);

        var list = _repository.GetProjectClosuresBySponsorOrganisationUser(
            searchQuery, pageNumber: 1, pageSize: 10, sortField: nameof(ProjectClosure.IrasId), sortDirection: "asc", sponsorOrganisationUserId: userId).ToList();

        total.ShouldBe(1);
        list.Count.ShouldBe(1);
        list[0].IrasId.ShouldBe(202);
        list[0].ShortProjectTitle.ShouldBe("B");
    }

    // ---------------------------------------------------------------------
    // Unknown sort field: sorting is skipped → original order
    // ---------------------------------------------------------------------
    [Fact]
    public async Task GetProjectClosuresBySponsorOrganisationUser_WhenSortFieldUnknown_ShouldReturnOriginalOrder()
    {
        var closures = new[]
        {
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-1", ShortProjectTitle = "T1", Status = "S1", IrasId = 10, SentToSponsorDate = DateTime.UtcNow.AddDays(-3), ClosureDate = DateTime.UtcNow.AddDays(-2), DateActioned = DateTime.UtcNow.AddDays(-2), UserId = "u1", CreatedBy = "seed", UpdatedBy = "seed",TransactionId="C2414/1", ProjectClosureNumber=1 },
            new ProjectClosure { Id = Guid.NewGuid(), ProjectRecordId = "PR-2", ShortProjectTitle = "T2", Status = "S2", IrasId = 20, SentToSponsorDate = DateTime.UtcNow.AddDays(-2), ClosureDate = DateTime.UtcNow.AddDays(-1), DateActioned = DateTime.UtcNow.AddDays(-1), UserId = "u2", CreatedBy = "seed", UpdatedBy = "seed",TransactionId="C2414/1", ProjectClosureNumber=1 }
        };

        var userId = await SeedScenarioAsync(closures: closures);

        var searchQuery = new ProjectClosuresSearchRequest { SearchTerm = null };

        var list = _repository.GetProjectClosuresBySponsorOrganisationUser(
            searchQuery, pageNumber: 1, pageSize: 10, sortField: "UnknownField", sortDirection: "asc", sponsorOrganisationUserId: userId).ToList();

        list.Count.ShouldBe(2);
        list[0].ProjectRecordId.ShouldBe("PR-1");
        list[1].ProjectRecordId.ShouldBe("PR-2");
    }

    // ---------------------------------------------------------------------
    // User not found or no RTS → empty results
    // ---------------------------------------------------------------------
    [Fact]
    public async Task GetProjectClosuresBySponsorOrganisationUser_WhenUserHasNoRtsIdOrNotFound_ReturnsEmpty()
    {
        // Seed a valid user to ensure context has some data (not used in the call)
        var _ = await SeedScenarioAsync(mainRtsId: "RTS-ABC");

        var notExistingUserId = Guid.NewGuid();
        var searchQuery = new ProjectClosuresSearchRequest { SearchTerm = null };

        var list = _repository.GetProjectClosuresBySponsorOrganisationUser(
            searchQuery, pageNumber: 1, pageSize: 10, sortField: nameof(ProjectClosure.IrasId), sortDirection: "asc", sponsorOrganisationUserId: notExistingUserId).ToList();

        var total = _repository.GetProjectClosuresBySponsorOrganisationUserCount(searchQuery, sponsorOrganisationUserId: notExistingUserId);

        list.ShouldBeEmpty();
        total.ShouldBe(0);
    }

    [Fact]
    public async Task UpdateProjectClosureStatus_Authorised_Updates_Closure_And_Closes_ProjectRecord()
    {
        // Arrange
        var userId = "user-123";

        // Seed a scenario with at least one ProjectClosure & ProjectRecord linked
        var _ = await SeedScenarioAsync(mainRtsId: "RTS-XYZ");

        // Pick any existing project closure
        var existingClosure = await _context.ProjectClosures
            .Include(pc => pc.ProjectRecord)
            .FirstOrDefaultAsync();

        existingClosure.ShouldNotBeNull();

        var projectRecordId = existingClosure!.ProjectRecordId;

        // Before update: ensure record not closed yet
        existingClosure.Status.ShouldNotBe(nameof(ProjectClosureStatus.Authorised));
        existingClosure.ProjectRecord.Status.ShouldNotBe(ProjectRecordStatus.Closed);

        var spec = new GetProjectClosureSpecification(projectRecordId);

        // Act
        var updated = await _repository.UpdateProjectClosureStatus(
            spec,
            status: nameof(ProjectClosureStatus.Authorised),
            userId: userId);

        // Assert: return value
        updated.ShouldNotBeNull();

        // Refresh from context to check persisted values
        var persistedClosure = await _context.ProjectClosures
            .Include(pc => pc.ProjectRecord)
            .FirstOrDefaultAsync(pc => pc.ProjectRecordId == projectRecordId);

        persistedClosure.ShouldNotBeNull();
        persistedClosure!.Status.ShouldBe(nameof(ProjectClosureStatus.Authorised));
        persistedClosure.UpdatedBy.ShouldBe(userId);
        persistedClosure.DateActioned.ShouldNotBe(default); // DateActioned set

        var persistedRecord = persistedClosure.ProjectRecord;
        persistedRecord.ShouldNotBeNull();
        persistedRecord.Status.ShouldBe(ProjectRecordStatus.Closed);
        persistedRecord.UpdatedBy.ShouldBe(userId);
        persistedRecord.UpdatedDate.ShouldNotBe(default); // UpdatedDate set
    }

    [Fact]
    public async Task UpdateProjectClosureStatus_NotAuthorised_Updates_Closure_Without_Closing_ProjectRecord()
    {
        // Arrange
        var userId = "tester-42";

        var _ = await SeedScenarioAsync(mainRtsId: "RTS-ABC");

        var existingClosure = await _context.ProjectClosures
            .Include(pc => pc.ProjectRecord)
            .FirstOrDefaultAsync();

        existingClosure.ShouldNotBeNull();

        var projectRecordId = existingClosure!.ProjectRecordId;

        var spec = new GetProjectClosureSpecification(projectRecordId);

        // Act: set to NotAuthorised
        var updated = await _repository.UpdateProjectClosureStatus(
            spec,
            status: nameof(ProjectClosureStatus.NotAuthorised),
            userId: userId);

        // Assert: return value
        updated.ShouldNotBeNull();

        var persistedClosure = await _context.ProjectClosures
            .Include(pc => pc.ProjectRecord)
            .FirstOrDefaultAsync(pc => pc.ProjectRecordId == projectRecordId);

        persistedClosure.ShouldNotBeNull();
        persistedClosure!.Status.ShouldBe(nameof(ProjectClosureStatus.NotAuthorised));
        persistedClosure.UpdatedBy.ShouldBe(userId);
        persistedClosure.DateActioned.ShouldNotBe(default);

        var persistedRecord = persistedClosure.ProjectRecord;
        persistedRecord.ShouldNotBeNull();
        persistedRecord.Status.ShouldNotBe(ProjectRecordStatus.Closed);
    }

    [Fact]
    public async Task UpdateProjectClosureStatus_Spec_Not_Matching_Returns_Null_And_Makes_No_Changes()
    {
        // Arrange
        var userId = "no-change-user";
        var _ = await SeedScenarioAsync(mainRtsId: "RTS-DEF");

        // existing closure to get baseline counts
        var beforeCount = await _context.ProjectClosures.CountAsync();

        // Spec points to a non-existent project record id
        var nonExistingProjectRecordId = "PR-NOT-THERE";
        var spec = new GetProjectClosureSpecification(nonExistingProjectRecordId);

        // Act
        var updated = await _repository.UpdateProjectClosureStatus(
            spec,
            status: nameof(ProjectClosureStatus.Authorised),
            userId: userId);

        // Assert
        updated.ShouldBeNull();

        // Ensure nothing got added/removed and no accidental changes occurred
        var afterCount = await _context.ProjectClosures.CountAsync();
        afterCount.ShouldBe(beforeCount);

        // Optional: verify none of the closures have been modified to Authorised or UpdatedBy=userId
        var anyModified = await _context.ProjectClosures
            .AnyAsync(pc => pc.Status == nameof(ProjectClosureStatus.Authorised) && pc.UpdatedBy == userId);
        anyModified.ShouldBeFalse();
    }
}