using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Constants;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.ProjectModificationServiceTests;

public class GetModificationsTests : TestServiceBase<ProjectModificationService>
{
    private readonly IrasContext _context;
    private readonly ProjectModificationRepository _modificationRepository;

    public GetModificationsTests()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

        _context = new IrasContext(options);
        _modificationRepository = new ProjectModificationRepository(_context);

        // Add nation code mappings used in filtering
        ProjectRecordConstants.NationIdMap["OPT0018"] = "England";
        ProjectRecordConstants.NationIdMap["OPT0019"] = "Wales";
        ProjectRecordConstants.NationIdMap["OPT0020"] = "Wales";
    }

    [Fact]
    public void Returns_Filtered_Modification()
    {
        // Arrange
        var recordId = Guid.NewGuid().ToString();
        var modificationId = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid().ToString();

        var record = new ProjectRecord
        {
            Id = recordId,
            IrasId = 123456,
            UserId = userId,
            CreatedBy = "Test",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Test",
            UpdatedDate = DateTime.Now,
            FullProjectTitle = "Description",
            ShortProjectTitle = "Short"
        };

        var modification = new ProjectModification
        {
            ProjectRecordId = recordId,
            CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ModificationIdentifier = modificationId,
            ProjectModificationChanges = new List<ProjectModificationChange>
            {
                new()
                {
                    AreaOfChange = ProjectRecordConstants.ParticipatingOrgs,
                    SpecificAreaOfChange = ProjectRecordConstants.MajorModificationAreaOfChange,
                    CreatedBy = "Test",
                    UpdatedBy = "Test",
                    UpdatedDate = DateTime.Now,
                    Status = "Active",
                    Id = Guid.NewGuid()
                }
            },
            CreatedBy = "Test",
            UpdatedBy = "Test",
            UpdatedDate = DateTime.Now,
            Status = "Active",
            SentToRegulatorDate = new DateTime(2023, 12, 31, 0, 0, 0, DateTimeKind.Utc),
            SentToSponsorDate = new DateTime(2023, 11, 30, 0, 0, 0, DateTimeKind.Utc),
        };

        var answers = new List<ProjectRecordAnswer>
        {
            new()
            {
                UserId = userId,
                ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.ChiefInvestigatorFirstName,
                Response = "Dr",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                UserId = userId,
                ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.ChiefInvestigatorLastName,
                Response = "Smith",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                UserId = userId, ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.LeadNation, SelectedOptions = "OPT0018",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                UserId = userId, ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.ParticipatingNation, SelectedOptions = "OPT0019,OPT0020",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                UserId = userId, ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.ShortProjectTitle, Response = "Short Title",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                UserId = userId, ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.SponsorOrganisation, Response = "1111",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            }
        };

        _context.ProjectRecords.Add(record);
        _context.ProjectModifications.Add(modification);
        _context.ProjectRecordAnswers.AddRange(answers);
        _context.SaveChanges();

        var search = new ModificationSearchRequest
        {
            IrasId = "123456",
            ChiefInvestigatorName = "Smith",
            ShortProjectTitle = "Short",
            SponsorOrganisation = "1111",
            FromDate = new DateTime(2023, 12, 31, 0, 0, 0, DateTimeKind.Utc),
            ToDate = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc),
            LeadNation = new List<string> { "England" },
            ParticipatingNation = new List<string> { "Wales" }
        };

        // Act
        var results1 = _modificationRepository.GetModifications(search, 1, 10, "CreatedAt", "asc");
        var results2 = _modificationRepository.GetModifications(search, 1, 10, "CreatedAt", "asc", record.Id);

        // Assert
        var list1 = results1.ToList();
        list1.Count.ShouldBe(1);
        list1[0].ChiefInvestigatorFirstName.ShouldBe("Dr");
        list1[0].ChiefInvestigatorLastName.ShouldBe("Smith");
        list1[0].LeadNation.ShouldBe("England");
        list1[0].ParticipatingNation.ShouldBe("Wales, Wales");

        var list2 = results1.ToList();
        list2.Count.ShouldBe(1);
        list2[0].ChiefInvestigatorFirstName.ShouldBe("Dr");
        list2[0].ChiefInvestigatorLastName.ShouldBe("Smith");
        list2[0].LeadNation.ShouldBe("England");
        list2[0].ParticipatingNation.ShouldBe("Wales, Wales");
    }

    [Fact]
    public void Returns_Modifications_By_Expanded_Status_Mapping()
    {
        // Arrange
        var recordId = Guid.NewGuid().ToString();

        var record = new ProjectRecord
        {
            Id = recordId,
            IrasId = 111111,
            UserId = "u1",
            CreatedBy = "t",
            CreatedDate = DateTime.UtcNow,
            UpdatedBy = "t",
            UpdatedDate = DateTime.UtcNow,
            FullProjectTitle = "Title",
            ShortProjectTitle = "Short"
        };

        var modification1 = new ProjectModification
        {
            ProjectRecordId = recordId,
            ModificationIdentifier = Guid.NewGuid().ToString(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedBy = "t",
            UpdatedBy = "t",
            Status = ModificationStatus.RequestRevisions // maps to InDraft (UI)
        };

        var modification2 = new ProjectModification
        {
            ProjectRecordId = recordId,
            ModificationIdentifier = Guid.NewGuid().ToString(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedBy = "t",
            UpdatedBy = "t",
            Status = ModificationStatus.InDraft
        };

        _context.ProjectRecords.Add(record);
        _context.ProjectModifications.AddRange(modification1, modification2);
        _context.SaveChanges();

        var search = new ModificationSearchRequest
        {
            Status = ModificationStatus.InDraft,   // user-visible UI filter
        };

        // Act
        var results = _modificationRepository.GetModifications(
            search, pageNumber: 1, pageSize: 10, sortField: "CreatedAt", sortDirection: "asc"
        ).ToList();

        // Assert
        results.Count.ShouldBe(2); // BOTH should match
        results.Any(r => r.Status == ModificationStatus.RequestRevisions).ShouldBeTrue();
        results.Any(r => r.Status == ModificationStatus.InDraft).ShouldBeTrue();
    }

    [Theory]
    [InlineData("111")]
    [InlineData("11111")]
    public void Returns_No_Modifications_When_SponsorOrganisation_Does_Not_Match_Exactly(string sponsorValue)
    {
        // Arrange
        var recordId = Guid.NewGuid().ToString();
        var modificationId = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid().ToString();

        var record = new ProjectRecord
        {
            Id = recordId,
            IrasId = 123456,
            UserId = userId,
            CreatedBy = "Test",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Test",
            UpdatedDate = DateTime.Now,
            FullProjectTitle = "Description",
            ShortProjectTitle = "Title"
        };

        var modification = new ProjectModification
        {
            ProjectRecordId = recordId,
            CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ModificationIdentifier = modificationId,
            ProjectModificationChanges = new List<ProjectModificationChange>
        {
            new()
            {
                AreaOfChange = ProjectRecordConstants.ParticipatingOrgs,
                SpecificAreaOfChange = ProjectRecordConstants.MajorModificationAreaOfChange,
                CreatedBy = "Test",
                UpdatedBy = "Test",
                UpdatedDate = DateTime.Now,
                Status = "Active",
                Id = Guid.NewGuid()
            }
        },
            CreatedBy = "Test",
            UpdatedBy = "Test",
            UpdatedDate = DateTime.Now,
            Status = "Active",
            SentToRegulatorDate = new DateTime(2023, 12, 31, 0, 0, 0, DateTimeKind.Utc)
        };

        var answers = new List<ProjectRecordAnswer>
    {
        new()
        {
            UserId = userId,
            ProjectRecordId = recordId,
            QuestionId = ProjectRecordConstants.SponsorOrganisation,
            Response = sponsorValue,
            Category = "Cat1",
            Section = "Section1",
            VersionId = "1"
        }
    };

        _context.ProjectRecords.Add(record);
        _context.ProjectModifications.Add(modification);
        _context.ProjectRecordAnswers.AddRange(answers);
        _context.SaveChanges();

        var search = new ModificationSearchRequest
        {
            IrasId = "123456",
            SponsorOrganisation = "1111",
            FromDate = new DateTime(2023, 12, 31, 0, 0, 0, DateTimeKind.Utc),
            ToDate = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var results = _modificationRepository.GetModifications(search, 1, 10, "CreatedAt", "asc");

        // Assert
        results.ShouldBeEmpty();
    }

    [Theory]
    [AutoData]
    public async Task GetModifications_ShouldReturnMappedResponse(
    ModificationSearchRequest searchRequest,
    List<ProjectModificationResult> domainModifications,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection)
    {
        // Arrange
        var mockRepo = new Mock<IProjectModificationRepository>();
        mockRepo.Setup(r => r.GetModifications(searchRequest, pageNumber, pageSize, sortField, sortDirection, null))
                .Returns(domainModifications);

        mockRepo.Setup(r => r.GetModificationsCount(searchRequest, null))
                .Returns(domainModifications.Count);

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var service = new ProjectModificationService(mockRepo.Object, mockHttpContextAccessor.Object);

        // Act
        var result = await service.GetModifications(searchRequest, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.Modifications.Count().ShouldBe(domainModifications.Count);
        result.TotalCount.ShouldBe(domainModifications.Count);
    }

    [Theory]
    [AutoData]
    public async Task GetModificationsForProject_ShouldReturnMappedResponseWithProjectId(
    string projectRecordId,
    ModificationSearchRequest searchRequest,
    List<ProjectModificationResult> domainModifications,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection)
    {
        // Arrange
        var mockRepo = new Mock<IProjectModificationRepository>();
        mockRepo.Setup(r => r.GetModifications(searchRequest, pageNumber, pageSize, sortField, sortDirection, projectRecordId))
                .Returns(domainModifications);

        mockRepo.Setup(r => r.GetModificationsCount(searchRequest, projectRecordId))
                .Returns(domainModifications.Count);

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var service = new ProjectModificationService(mockRepo.Object, mockHttpContextAccessor.Object);

        // Act
        var result = await service.GetModificationsForProject(projectRecordId, searchRequest, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.Modifications.Count().ShouldBe(domainModifications.Count);
        result.TotalCount.ShouldBe(domainModifications.Count);
        result.ProjectRecordId.ShouldBe(projectRecordId);
    }

    [Theory, AutoData]
    public async Task GetModificationsBySponsorOrganisationUserId_ShouldReturnMappedResponse(
    Guid sponsorOrganisationUserId,
    SponsorAuthorisationsModificationsSearchRequest searchQuery,
    List<ProjectModificationResult> domainModifications,
    int pageNumber,
    int pageSize,
    string sortField,
    string sortDirection, string rtsId)
    {
        // Arrange
        var mockRepo = new Mock<IProjectModificationRepository>();
        mockRepo.Setup(r => r.GetModificationsBySponsorOrganisationUser(searchQuery, pageNumber, pageSize, sortField, sortDirection, sponsorOrganisationUserId, rtsId))
                .Returns(domainModifications);

        mockRepo.Setup(r => r.GetModificationsBySponsorOrganisationUserCount(searchQuery, sponsorOrganisationUserId, rtsId))
                .Returns(domainModifications.Count);

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var service = new ProjectModificationService(mockRepo.Object, mockHttpContextAccessor.Object);

        // Act
        var result = await service.GetModificationsBySponsorOrganisationUserId(sponsorOrganisationUserId, searchQuery, pageNumber, pageSize, sortField, sortDirection, rtsId);

        // Assert
        result.ShouldNotBeNull();
        result.Modifications.Count().ShouldBe(domainModifications.Count);
        result.TotalCount.ShouldBe(domainModifications.Count);
    }

    [Theory, AutoData]
    public async Task GetModificationsByIds_ShouldReturnMappedResult
    (
        List<string> ids,
        List<ProjectModificationResult> domainModifications
    )
    {
        // Arrange
        var mockRepo = new Mock<IProjectModificationRepository>();

        mockRepo
            .Setup(r => r.GetModificationsByIds(ids))
            .Returns(domainModifications);

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var service = new ProjectModificationService(mockRepo.Object, mockHttpContextAccessor.Object);

        // Act
        var result = await service.GetModificationsByIds(ids);

        // Assert
        result.ShouldNotBeNull();
        result.Modifications.Count().ShouldBe(domainModifications.Count);
    }

    [Fact]
    public void Sorts_By_UiRevisionStatus_Ascending()
    {
        // Arrange
        var recordId = Guid.NewGuid().ToString();

        var record = new ProjectRecord
        {
            Id = recordId,
            IrasId = 111111,
            UserId = "u1",
            CreatedBy = "t",
            CreatedDate = DateTime.UtcNow,
            UpdatedBy = "t",
            UpdatedDate = DateTime.UtcNow,
            FullProjectTitle = "Title",
            ShortProjectTitle = "Short"
        };
        _context.ProjectRecords.Add(record);

        var mod1 = new ProjectModification
        {
            ProjectRecordId = recordId,
            ModificationIdentifier = Guid.NewGuid().ToString(),
            Status = ModificationStatus.ReviseAndAuthorise,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedBy = "t",
            UpdatedBy = "t"
        };

        var mod2 = new ProjectModification
        {
            ProjectRecordId = recordId,
            ModificationIdentifier = Guid.NewGuid().ToString(),
            Status = ModificationStatus.RequestRevisions,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedBy = "t",
            UpdatedBy = "t"
        };

        var mod3 = new ProjectModification
        {
            ProjectRecordId = recordId,
            ModificationIdentifier = Guid.NewGuid().ToString(),
            Status = "SomethingElse",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedBy = "t",
            UpdatedBy = "t"
        };

        _context.ProjectModifications.AddRange(mod1, mod2, mod3);
        _context.SaveChanges();

        var search = new ModificationSearchRequest(); // brak filtrowania

        // Act
        var results = _modificationRepository.GetModifications(
            search, 1, 10, "Status", "asc"
        ).ToList();

        var expectedOrder = new[]
        {
            ModificationStatus.RequestRevisions, // InDraft (lowest)
            "SomethingElse",
            ModificationStatus.ReviseAndAuthorise // WithSponsor (highest)
        };

        results.Count.ShouldBe(3);
        results.Select(r => r.Status).ShouldBe(expectedOrder);
    }

    [Fact]
    public void Sorts_By_UiRevisionStatus_Descending()
    {
        // Arrange
        var recordId = Guid.NewGuid().ToString();

        var record = new ProjectRecord
        {
            Id = recordId,
            IrasId = 111111,
            UserId = "u1",
            CreatedBy = "t",
            CreatedDate = DateTime.UtcNow,
            UpdatedBy = "t",
            UpdatedDate = DateTime.UtcNow,
            FullProjectTitle = "Title",
            ShortProjectTitle = "Short"
        };
        _context.ProjectRecords.Add(record);

        var mod1 = new ProjectModification
        {
            ProjectRecordId = recordId,
            ModificationIdentifier = Guid.NewGuid().ToString(),
            Status = ModificationStatus.ReviseAndAuthorise,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedBy = "t",
            UpdatedBy = "t"
        };

        var mod2 = new ProjectModification
        {
            ProjectRecordId = recordId,
            ModificationIdentifier = Guid.NewGuid().ToString(),
            Status = ModificationStatus.RequestRevisions,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedBy = "t",
            UpdatedBy = "t"
        };

        var mod3 = new ProjectModification
        {
            ProjectRecordId = recordId,
            ModificationIdentifier = Guid.NewGuid().ToString(),
            Status = "SomethingElse",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedBy = "t",
            UpdatedBy = "t"
        };

        _context.ProjectModifications.AddRange(mod1, mod2, mod3);
        _context.SaveChanges();

        var search = new ModificationSearchRequest();

        // Act
        var results = _modificationRepository.GetModifications(
            search, 1, 10, "Status", "desc"
        ).ToList();

        var expectedOrder = new[]
        {
            ModificationStatus.ReviseAndAuthorise,
            "SomethingElse",
            ModificationStatus.RequestRevisions
        };

        results.Count.ShouldBe(3);
        results.Select(r => r.Status).ShouldBe(expectedOrder);
    }
}