using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;
using Rsp.IrasService.Infrastructure.Repositories;
using Rsp.IrasService.Services;

namespace Rsp.IrasService.UnitTests.Services.RespondentServiceTests;

public class GetResponses : TestServiceBase<RespondentService>
{
    private readonly IrasContext _context;
    private readonly RespondentRepository _respondentRepository;

    public GetResponses()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        _respondentRepository = new RespondentRepository(_context);
    }

    /// <summary>
    ///     Tests that GetResponses correctly retrieves respondent answers for a given application.
    /// </summary>
    [Fact]
    public async Task GetResponses_ShouldReturnAnswers_ForApplicationId()
    {
        // Arrange
        var respondentService = new RespondentService(_respondentRepository);
        var fixedApplicationId = "ApplicationId-123";
        var fixedRespondentId = "RespondentId-123"; // Explicitly setting RespondentId

        var respondentAnswers = new List<ProjectRecordAnswer>
        {
            new()
            {
                ProjectRecordId = fixedApplicationId, ProjectPersonnelId = fixedRespondentId, QuestionId = "Q1",
                VersionId = "v1", Category = "Category-1", Section = "Section-1", Response = "Answer1", OptionType = "Single",
                SelectedOptions = "OptionA"
            },
            new()
            {
                ProjectRecordId = fixedApplicationId, ProjectPersonnelId = fixedRespondentId, QuestionId = "Q2",
                VersionId = "v1", Category = "Category-2", Section = "Section-2", Response = "Answer2", OptionType = "Multiple",
                SelectedOptions = "OptionB,OptionC"
            },
            new()
            {
                ProjectRecordId = "OtherApplication", ProjectPersonnelId = "OtherRespondent", QuestionId = "Q3",
                VersionId = "v1", Category = "Category-1", Section = "Section-3", Response = "Answer3", OptionType = "Single",
                SelectedOptions = "OptionD"
            } // Should be filtered out
        };

        await _context.ProjectRecordAnswers.AddRangeAsync(respondentAnswers);
        await _context.SaveChangesAsync();

        // Act
        var result = await respondentService.GetResponses(fixedApplicationId);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(2);
        result.All(a => a.QuestionId is "Q1" or "Q2").ShouldBeTrue();
    }

    /// <summary>
    ///     Tests that GetResponses correctly retrieves respondent answers for a given application and category.
    /// </summary>
    [Fact]
    public async Task GetResponses_ShouldReturnAnswers_ForApplicationIdAndCategoryId()
    {
        // Arrange
        var respondentService = new RespondentService(_respondentRepository);
        var fixedApplicationId = "ApplicationId-123";
        var fixedRespondentId = "RespondentId-123"; // Explicitly setting RespondentId
        var fixedCategoryId = "Category-1";

        var respondentAnswers = new List<ProjectRecordAnswer>
        {
            new()
            {
                ProjectRecordId = fixedApplicationId, ProjectPersonnelId = fixedRespondentId, QuestionId = "Q1",
                VersionId = "v1", Category = fixedCategoryId, Section = "Section-1", Response = "Answer1", OptionType = "Single",
                SelectedOptions = "OptionA"
            },
            new()
            {
                ProjectRecordId = fixedApplicationId, ProjectPersonnelId = fixedRespondentId, QuestionId = "Q2",
                VersionId = "v1", Category = "Category-2", Section = "Section-2", Response = "Answer2", OptionType = "Multiple",
                SelectedOptions = "OptionB,OptionC"
            }, // Should be filtered out
            new()
            {
                ProjectRecordId = fixedApplicationId, ProjectPersonnelId = fixedRespondentId, QuestionId = "Q3",
                VersionId = "v1", Category = fixedCategoryId, Section = "Section-3", Response = "Answer3", OptionType = "Single",
                SelectedOptions = "OptionD"
            }
        };

        await _context.ProjectRecordAnswers.AddRangeAsync(respondentAnswers);
        await _context.SaveChangesAsync();

        // Act
        var result = await respondentService.GetResponses(fixedApplicationId, fixedCategoryId);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(2);
        result.All(a => a.CategoryId == fixedCategoryId).ShouldBeTrue();
    }

    /// <summary>
    ///     Tests that responses are returned for given modificationChangeId and projectRecordId
    /// </summary>
    [Theory, AutoData]
    public async Task GetResponses_ShouldReturnAnswers_For_ModificationChangeId_ProjectRecordId(Guid modificationChangeId, string projectRecordId)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        // Optionally seed data here if needed for the test

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        var result = await Sut.GetResponses(modificationChangeId, projectRecordId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    ///     Tests that responses are returned for given modificationChangeId, projectRecordId, and categoryId
    /// </summary>
    [Theory, AutoData]
    public async Task GetResponses_ShouldReturnAnswers_For_ModificationChangeId_ProjectRecordId_CategoryId(Guid modificationChangeId, string projectRecordId, string categoryId)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        // Optionally seed data here if needed for the test

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        var result = await Sut.GetResponses(modificationChangeId, projectRecordId, categoryId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IEnumerable<RespondentAnswerDto>>();
    }

    /// <summary>
    ///     Tests that responses are returned for document types
    /// </summary>
    [Theory, AutoData]
    public async Task GetDocumentTypes_ShouldReturnAnswers()
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        // Optionally seed data here if needed for the test

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        var result = await Sut.GetDocumentTypeResponses();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IEnumerable<DocumentTypeResponse>>();
    }

    /// <summary>
    ///     Tests that responses are returned for document types
    /// </summary>
    [Theory, AutoData]
    public async Task GetModificationAreaOfChanges_ShouldReturnAnswers()
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        // Optionally seed data here if needed for the test

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        var result = await Sut.GetModificationAreaOfChanges();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IEnumerable<ModificationAreaOfChangeDto>>();
    }

    /// <summary>
    ///     Tests that documents are returned for given modificationChangeId, projectRecordId, and personnelId
    /// </summary>
    [Theory, AutoData]
    public async Task GetModificationDocuments_ShouldReturnAnswers_For_ModificationChangeId_ProjectRecordId_PersonnelId(Guid modificationChangeId, string projectRecordId, string personnelId)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        // Optionally seed data here if needed for the test

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        var result = await Sut.GetModificationDocumentResponses(modificationChangeId, projectRecordId, personnelId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IEnumerable<ModificationDocumentDto>>();
    }

    /// <summary>
    ///     Tests that organisations are returned for given modificationChangeId, projectRecordId, and personnelId
    /// </summary>
    [Theory, AutoData]
    public async Task GetModificationOrganisations_ShouldReturnAnswers_For_ModificationChangeId_ProjectRecordId_PersonnelId(Guid modificationChangeId, string projectRecordId, string personnelId)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        // Optionally seed data here if needed for the test

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        var result = await Sut.GetModificationParticipatingOrganisationResponses(modificationChangeId, projectRecordId, personnelId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IEnumerable<ModificationParticipatingOrganisationDto>>();
    }

    /// <summary>
    ///     Tests that organisation answer is returned for given organisationId
    /// </summary>
    [Theory, AutoData]
    public async Task GetModificationOrganisationAnswer_ShouldReturnAnswers_For_OrganisationId(Guid organisationId)
    {
        //// Arrange
        //Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        //// Optionally seed data here if needed for the test

        //Sut = Mocker.CreateInstance<RespondentService>();

        //// Act
        //var result = await Sut.GetModificationParticipatingOrganisationAnswerResponses(organisationId);

        //// Assert
        //result.ShouldNotBeNull();
        //result.ShouldBeAssignableTo<ModificationParticipatingOrganisationAnswerDto>();

        // Arrange
        var respondentService = new RespondentService(_respondentRepository);
        var fixedorganisationId = Guid.NewGuid();
        var fixedApplicationId = "ApplicationId-123";
        var fixedRespondentId = "RespondentId-123"; // Explicitly setting RespondentId

        var respondentAnswers = new ModificationParticipatingOrganisation
        {
            Id = fixedorganisationId,
            ProjectModificationChangeId = fixedorganisationId,
            ProjectPersonnelId = fixedRespondentId,
            ProjectRecordId = fixedApplicationId,
            OrganisationId = "Organisation-123",
        };

        var organisationAnswer = new ModificationParticipatingOrganisationAnswer
        {
            Id = Guid.NewGuid(),
            ModificationParticipatingOrganisationId = fixedorganisationId,
            QuestionId = "Q1",
            VersionId = "v1",
            Category = "Category-1",
            Section = "Section-1",
            Response = "Answer1",
            OptionType = "Single",
            SelectedOptions = "OptionA"
        };

        await _context.ModificationParticipatingOrganisations.AddAsync(respondentAnswers);
        await _context.ModificationParticipatingOrganisationAnswers.AddAsync(organisationAnswer);
        await _context.SaveChangesAsync();

        // Act
        var result = await respondentService.GetModificationParticipatingOrganisationAnswerResponses(respondentAnswers.Id);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<ModificationParticipatingOrganisationAnswerDto>();
    }
}