using Microsoft.EntityFrameworkCore;
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

        var respondentAnswers = new List<ProjectApplicationRespondentAnswer>
        {
            new()
            {
                ProjectApplicationId = fixedApplicationId, ProjectApplicationRespondentId = fixedRespondentId, QuestionId = "Q1",
                Category = "Category-1", Section = "Section-1", Response = "Answer1", OptionType = "Single",
                SelectedOptions = "OptionA"
            },
            new()
            {
                ProjectApplicationId = fixedApplicationId, ProjectApplicationRespondentId = fixedRespondentId, QuestionId = "Q2",
                Category = "Category-2", Section = "Section-2", Response = "Answer2", OptionType = "Multiple",
                SelectedOptions = "OptionB,OptionC"
            },
            new()
            {
                ProjectApplicationId = "OtherApplication", ProjectApplicationRespondentId = "OtherRespondent", QuestionId = "Q3",
                Category = "Category-1", Section = "Section-3", Response = "Answer3", OptionType = "Single",
                SelectedOptions = "OptionD"
            } // Should be filtered out
        };

        await _context.ProjectApplicationRespondentAnswers.AddRangeAsync(respondentAnswers);
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

        var respondentAnswers = new List<ProjectApplicationRespondentAnswer>
        {
            new()
            {
                ProjectApplicationId = fixedApplicationId, ProjectApplicationRespondentId = fixedRespondentId, QuestionId = "Q1",
                Category = fixedCategoryId, Section = "Section-1", Response = "Answer1", OptionType = "Single",
                SelectedOptions = "OptionA"
            },
            new()
            {
                ProjectApplicationId = fixedApplicationId, ProjectApplicationRespondentId = fixedRespondentId, QuestionId = "Q2",
                Category = "Category-2", Section = "Section-2", Response = "Answer2", OptionType = "Multiple",
                SelectedOptions = "OptionB,OptionC"
            }, // Should be filtered out
            new()
            {
                ProjectApplicationId = fixedApplicationId, ProjectApplicationRespondentId = fixedRespondentId, QuestionId = "Q3",
                Category = fixedCategoryId, Section = "Section-3", Response = "Answer3", OptionType = "Single",
                SelectedOptions = "OptionD"
            }
        };

        await _context.ProjectApplicationRespondentAnswers.AddRangeAsync(respondentAnswers);
        await _context.SaveChangesAsync();

        // Act
        var result = await respondentService.GetResponses(fixedApplicationId, fixedCategoryId);

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(2);
        result.All(a => a.CategoryId == fixedCategoryId).ShouldBeTrue();
    }
}