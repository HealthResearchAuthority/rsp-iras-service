using Microsoft.EntityFrameworkCore;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Infrastructure;
using Rsp.Service.Infrastructure.Repositories;
using Rsp.Service.Services;

namespace Rsp.Service.UnitTests.Services.RespondentServiceTests;

public class SaveResponses : TestServiceBase<RespondentService>
{
    private readonly IrasContext _context;
    private readonly RespondentRepository _respondentRepository;

    public SaveResponses()
    {
        var options = new DbContextOptionsBuilder<IrasContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;

        _context = new IrasContext(options);
        _respondentRepository = new RespondentRepository(_context);
    }

    /// <summary>
    ///     Tests that SaveResponses correctly saves respondent answers with all fields.
    /// </summary>
    [Fact]
    public async Task SaveResponses_ShouldSaveAllFieldsCorrectly()
    {
        // Arrange
        var respondentService = new RespondentService(_respondentRepository);
        var fixedApplicationId = "ApplicationId-123";
        var fixedRespondentId = "RespondentId-123";

        var respondentAnswersRequest = new RespondentAnswersRequest
        {
            ProjectRecordId = fixedApplicationId,
            Id = fixedRespondentId,
            RespondentAnswers = new List<RespondentAnswerDto>
            {
                new()
                {
                    QuestionId = "Q1",
                    VersionId = "v1",
                    CategoryId = "Category-1",
                    SectionId = "Section-1",
                    AnswerText = "Answer text 1",
                    OptionType = "Single",
                    SelectedOption = "OptionA",
                    Answers = new List<string> { "OptionA" }
                },
                new()
                {
                    QuestionId = "Q2",
                    VersionId = "v1",
                    CategoryId = "Category-2",
                    SectionId = "Section-2",
                    AnswerText = "Answer text 2",
                    OptionType = "Multiple",
                    SelectedOption = null,
                    Answers = new List<string> { "OptionB", "OptionC" }
                }
            }
        };

        // Act
        await respondentService.SaveResponses(respondentAnswersRequest);

        // Assert
        var savedResponses = await _context.ProjectRecordAnswers.ToListAsync();
        savedResponses.ShouldNotBeNull();
        savedResponses.Count.ShouldBe(2);

        foreach (var savedResponse in savedResponses)
        {
            var expectedAnswer =
                respondentAnswersRequest.RespondentAnswers.First(a => a.QuestionId == savedResponse.QuestionId);
            savedResponse.ProjectRecordId.ShouldBe(fixedApplicationId);
            savedResponse.UserId.ShouldBe(fixedRespondentId);
            savedResponse.QuestionId.ShouldBe(expectedAnswer.QuestionId);
            savedResponse.Category.ShouldBe(expectedAnswer.CategoryId);
            savedResponse.Section.ShouldBe(expectedAnswer.SectionId);
            savedResponse.Response.ShouldBe(expectedAnswer.AnswerText);
            savedResponse.OptionType.ShouldBe(expectedAnswer.OptionType);
            savedResponse.SelectedOptions.ShouldBe(expectedAnswer.SelectedOption ??
                                                   string.Join(",", expectedAnswer.Answers));
        }
    }

    /// <summary>
    ///     Tests that SaveResponses does not fail when no answers are provided.
    /// </summary>
    [Fact]
    public async Task SaveResponses_ShouldNotFail_WhenNoAnswersProvided()
    {
        // Arrange
        var respondentService = new RespondentService(_respondentRepository);
        var respondentAnswersRequest = new RespondentAnswersRequest
        {
            ProjectRecordId = "ApplicationId-123",
            Id = "RespondentId-123",
            RespondentAnswers = new List<RespondentAnswerDto>() // Empty list
        };

        // Act
        await respondentService.SaveResponses(respondentAnswersRequest);

        // Assert
        var savedResponses = await _context.ProjectRecordAnswers.ToListAsync();
        savedResponses.ShouldBeEmpty();
    }

    /// <summary>
    ///     Tests that modification documents are saved
    /// </summary>
    [Theory, AutoData]
    public async Task Persists_ModificationDocumentAnswerResponses(List<ModificationDocumentAnswerDto> request)
    {
        // Arrange
        Mocker.Use<IProjectPersonnelRepository>(_respondentRepository);

        Sut = Mocker.CreateInstance<RespondentService>();

        // Act
        await Sut.SaveModificationDocumentAnswerResponses(request);

        // Assert
        (await _context.ModificationDocumentAnswers.CountAsync()).ShouldBe(request.Count);
    }
}