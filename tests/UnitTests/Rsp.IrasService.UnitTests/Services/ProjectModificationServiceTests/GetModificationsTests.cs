using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.ProjectModificationServiceTests;

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
        var projectPersonnelId = Guid.NewGuid().ToString();

        var record = new ProjectRecord
        {
            Id = recordId,
            IrasId = 123456,
            ProjectPersonnelId = projectPersonnelId,
            CreatedBy = "Test",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Test",
            UpdatedDate = DateTime.Now,
            Description = "Description",
            Title = "Title"
        };

        var modification = new ProjectModification
        {
            ProjectRecordId = recordId,
            CreatedDate = new DateTime(2024, 1, 1),
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
            SentToRegulatorDate = new DateTime(2023, 12, 31)
        };

        var answers = new List<ProjectRecordAnswer>
        {
            new()
            {
                ProjectPersonnelId = projectPersonnelId,
                ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.ChiefInvestigator,
                Response = "Dr Smith",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                ProjectPersonnelId = projectPersonnelId, ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.LeadNation, SelectedOptions = "OPT0018",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                ProjectPersonnelId = projectPersonnelId, ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.ParticipatingNation, SelectedOptions = "OPT0019,OPT0020",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                ProjectPersonnelId = projectPersonnelId, ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.ShortProjectTitle, Response = "Short Title",
                Category = "Cat1",
                Section = "Section1",
                VersionId = "1"
            },
            new()
            {
                ProjectPersonnelId = projectPersonnelId, ProjectRecordId = recordId,
                QuestionId = ProjectRecordConstants.SponsorOrganisation, Response = "Test Org",
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
            SponsorOrganisation = "Test Org",
            FromDate = new DateTime(2023, 12, 31),
            ToDate = new DateTime(2024, 1, 2),
            LeadNation = new List<string> { "England" },
            ParticipatingNation = new List<string> { "Wales" }
        };

        // Act
        var results1 = _modificationRepository.GetModifications(search, 1, 10, "CreatedAt", "asc");
        var results2 = _modificationRepository.GetModifications(search, 1, 10, "CreatedAt", "asc", record.Id);

        // Assert
        var list1 = results1.ToList();
        list1.Count.ShouldBe(1);
        list1[0].ChiefInvestigator.ShouldBe("Dr Smith");
        list1[0].LeadNation.ShouldBe("England");
        list1[0].ParticipatingNation.ShouldBe("Wales, Wales");

        var list2 = results1.ToList();
        list2.Count.ShouldBe(1);
        list2[0].ChiefInvestigator.ShouldBe("Dr Smith");
        list2[0].LeadNation.ShouldBe("England");
        list2[0].ParticipatingNation.ShouldBe("Wales, Wales");
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

        var service = new ProjectModificationService(mockRepo.Object);

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

        var service = new ProjectModificationService(mockRepo.Object);

        // Act
        var result = await service.GetModificationsForProject(projectRecordId, searchRequest, pageNumber, pageSize, sortField, sortDirection);

        // Assert
        result.ShouldNotBeNull();
        result.Modifications.Count().ShouldBe(domainModifications.Count);
        result.TotalCount.ShouldBe(domainModifications.Count);
        result.ProjectRecordId.ShouldBe(projectRecordId);
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

        var service = new ProjectModificationService(mockRepo.Object);

        // Act
        var result = await service.GetModificationsByIds(ids);

        // Assert
        result.ShouldNotBeNull();
        result.Modifications.Count().ShouldBe(domainModifications.Count);
    }
}