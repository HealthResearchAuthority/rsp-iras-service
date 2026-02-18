using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Domain.Entities;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.RespondentServiceTests;

public class GetResponses : TestServiceBase<RespondentService>
{
    private readonly IrasContext _context;
    private readonly TestRespondentRepository _respondentRepository;

    public GetResponses()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        var featureManager = new Mock<IFeatureManager>();
        _respondentRepository = new TestRespondentRepository(new RespondentRepository(_context, featureManager.Object));
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
                ProjectRecordId = fixedApplicationId, UserId = fixedRespondentId, QuestionId = "Q1",
                VersionId = "v1", Category = "Category-1", Section = "Section-1", Response = "Answer1", OptionType = "Single",
                SelectedOptions = "OptionA"
            },
            new()
            {
                ProjectRecordId = fixedApplicationId, UserId = fixedRespondentId, QuestionId = "Q2",
                VersionId = "v1", Category = "Category-2", Section = "Section-2", Response = "Answer2", OptionType = "Multiple",
                SelectedOptions = "OptionB,OptionC"
            },
            new()
            {
                ProjectRecordId = "OtherApplication", UserId = "OtherRespondent", QuestionId = "Q3",
                VersionId = "v1", Category = "Category-1", Section = "Section-3", Response = "Answer3", OptionType = "Single",
                SelectedOptions = "OptionD"
            } // Should be filtered out
        };

        await _context.ProjectRecordAnswers.AddRangeAsync(respondentAnswers);
        await _context.SaveChangesAsync();

        // Populate test repository with effective answers
        _respondentRepository.SetEffectiveAnswers(respondentAnswers);

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
                ProjectRecordId = fixedApplicationId, UserId = fixedRespondentId, QuestionId = "Q1",
                VersionId = "v1", Category = fixedCategoryId, Section = "Section-1", Response = "Answer1", OptionType = "Single",
                SelectedOptions = "OptionA"
            },
            new()
            {
                ProjectRecordId = fixedApplicationId, UserId = fixedRespondentId, QuestionId = "Q2",
                VersionId = "v1", Category = "Category-2", Section = "Section-2", Response = "Answer2", OptionType = "Multiple",
                SelectedOptions = "OptionB,OptionC"
            }, // Should be filtered out
            new()
            {
                ProjectRecordId = fixedApplicationId, UserId = fixedRespondentId, QuestionId = "Q3",
                VersionId = "v1", Category = fixedCategoryId, Section = "Section-3", Response = "Answer3", OptionType = "Single",
                SelectedOptions = "OptionD"
            }
        };

        await _context.ProjectRecordAnswers.AddRangeAsync(respondentAnswers);
        await _context.SaveChangesAsync();

        // Populate test repository with effective answers
        _respondentRepository.SetEffectiveAnswers(respondentAnswers);

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
        var result = await Sut.GetModificationChangeResponses(modificationChangeId, projectRecordId);

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
        var result = await Sut.GetModificationChangeResponses(modificationChangeId, projectRecordId, categoryId);

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
    ///     Tests that responses are returned for document types
    /// </summary>
    [Theory, AutoData]
    public async Task GetDocumentsByType_ShouldReturnAnswers(string projectRecordId, string documentTypeId)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        // Optionally seed data here if needed for the test

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        var result = await Sut.GetDocumentsByType(projectRecordId, documentTypeId);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeAssignableTo<IEnumerable<DocumentTypeResponse>>();
    }
}

/// <summary>
/// Test-specific repository wrapper that allows populating EffectiveProjectRecordAnswers
/// </summary>
internal class TestRespondentRepository : IProjectPersonnelRepository
{
    private readonly IProjectPersonnelRepository _innerRepository;
    private List<EffectiveProjectRecordAnswer> _effectiveAnswers = [];

    public TestRespondentRepository(IProjectPersonnelRepository innerRepository)
    {
        _innerRepository = innerRepository;
    }

    public void SetEffectiveAnswers(IEnumerable<ProjectRecordAnswer> answers)
    {
        _effectiveAnswers = answers.Select(a => new EffectiveProjectRecordAnswer
        {
            ProjectRecordId = a.ProjectRecordId,
            UserId = a.UserId,
            QuestionId = a.QuestionId,
            VersionId = a.VersionId,
            Category = a.Category,
            Section = a.Section,
            Response = a.Response,
            OptionType = a.OptionType,
            SelectedOptions = a.SelectedOptions
        }).ToList();
    }

    public Task SaveResponses(ISpecification<ProjectRecordAnswer> specification, List<ProjectRecordAnswer> respondentAnswers)
        => _innerRepository.SaveResponses(specification, respondentAnswers);

    public Task SaveModificationResponses(ISpecification<ProjectModificationAnswer> specification, List<ProjectModificationAnswer> respondentAnswers)
        => _innerRepository.SaveModificationResponses(specification, respondentAnswers);

    public Task SaveModificationChangeResponses(ISpecification<ProjectModificationChangeAnswer> specification, List<ProjectModificationChangeAnswer> respondentAnswers)
        => _innerRepository.SaveModificationChangeResponses(specification, respondentAnswers);

    public Task<IEnumerable<EffectiveProjectRecordAnswer>> GetResponses(ISpecification<EffectiveProjectRecordAnswer> specification)
    {
        // For testing, apply the specification to the in-memory effective answers
        var result = specification.Evaluate(_effectiveAnswers).AsEnumerable();
        return Task.FromResult(result);
    }

    public Task<IEnumerable<ProjectModificationAnswer>> GetResponses(ISpecification<ProjectModificationAnswer> specification)
        => _innerRepository.GetResponses(specification);

    public Task<IEnumerable<ProjectModificationChangeAnswer>> GetResponses(ISpecification<ProjectModificationChangeAnswer> specification)
        => _innerRepository.GetResponses(specification);

    public Task<IEnumerable<DocumentType>> GetResponses(ISpecification<DocumentType> specification)
        => _innerRepository.GetResponses(specification);

    public Task<IEnumerable<ModificationDocumentAnswer>> GetResponses(ISpecification<ModificationDocumentAnswer> specification)
        => _innerRepository.GetResponses(specification);

    public Task<IEnumerable<ModificationDocument>> GetResponses(ISpecification<ModificationDocument> specification)
        => _innerRepository.GetResponses(specification);

    public Task<IEnumerable<ModificationParticipatingOrganisation>> GetResponses(ISpecification<ModificationParticipatingOrganisation> specification)
        => _innerRepository.GetResponses(specification);

    public Task<ModificationParticipatingOrganisationAnswer> GetResponses(ISpecification<ModificationParticipatingOrganisationAnswer> specification)
        => _innerRepository.GetResponses(specification);

    public Task<ModificationDocument> GetResponse(ISpecification<ModificationDocument> specification)
        => _innerRepository.GetResponse(specification);

    public Task SaveModificationDocumentResponses(ISpecification<ModificationDocument> specification, List<ModificationDocument> respondentAnswers)
        => _innerRepository.SaveModificationDocumentResponses(specification, respondentAnswers);

    public Task SaveModificationParticipatingOrganisationResponses(ISpecification<ModificationParticipatingOrganisation> specification, List<ModificationParticipatingOrganisation> respondentAnswers)
        => _innerRepository.SaveModificationParticipatingOrganisationResponses(specification, respondentAnswers);

    public Task SaveModificationParticipatingOrganisationAnswerResponses(ISpecification<ModificationParticipatingOrganisationAnswer> specification, ModificationParticipatingOrganisationAnswer respondentAnswer)
        => _innerRepository.SaveModificationParticipatingOrganisationAnswerResponses(specification, respondentAnswer);

    public Task SaveModificationDocumentAnswerResponses(ISpecification<ModificationDocumentAnswer> specification, List<ModificationDocumentAnswer> respondentAnswer)
        => _innerRepository.SaveModificationDocumentAnswerResponses(specification, respondentAnswer);

    public Task DeleteModificationDocumentResponses(ISpecification<ModificationDocument> specification, List<ModificationDocument> respondentAnswers)
        => _innerRepository.DeleteModificationDocumentResponses(specification, respondentAnswers);

    public Task SaveModificationDocumentsAuditTrail(List<ModificationDocumentsAuditTrail> documentsAuditTrail)
        => _innerRepository.SaveModificationDocumentsAuditTrail(documentsAuditTrail);

    public Task<IEnumerable<ModificationDocument>> GetDocumentsByType(string documentTypeId, string projectRecordId)
        => _innerRepository.GetDocumentsByType(documentTypeId, projectRecordId);

    public Task DeleteModificationDocumentAnswersResponses(ISpecification<ModificationDocument> specification, List<ModificationDocument> respondentAnswers)
        => _innerRepository.DeleteModificationDocumentAnswersResponses(specification, respondentAnswers);
}