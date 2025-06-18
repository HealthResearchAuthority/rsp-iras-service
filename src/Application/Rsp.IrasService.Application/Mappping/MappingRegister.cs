using Mapster;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Mappping;

/// <summary>
/// Register for type mappings
/// </summary>
public class MappingRegister : IRegister
{
    /// <summary>
    /// Register method to configure the mappings
    /// </summary>
    public void Register(TypeAdapterConfig config)
    {
        // register your mapster mappings here only if you
        // need custom mapping or need different settings for mapping

        config
            .NewConfig<ApplicationRequest, ProjectApplication>()
            .Map(dest => dest.CreatedDate, source => source.StartDate)
            .Map(dest => dest.UpdatedDate, _ => DateTime.Now);

        config
            .NewConfig<ProjectApplication, ApplicationRequest>()
            .Map(dest => dest.StartDate, source => source.CreatedDate);

        config
            .NewConfig<RespondentDto, ProjectApplicationRespondent>()
            .Map(dest => dest.Email, source => source.EmailAddress);

        config
            .NewConfig<RespondentAnswerDto, ProjectApplicationRespondentAnswer>()
            .Ignore(ra => ra.Id)
            .Ignore(ra => ra.ProjectApplicationId)
            .Map(dest => dest.Category, source => source.CategoryId)
            .Map(dest => dest.Section, source => source.SectionId)
            .Map(dest => dest.Response, source => source.AnswerText)
            .Map(dest => dest.SelectedOptions, source => source.SelectedOption, source => !string.IsNullOrWhiteSpace(source.SelectedOption))
            .Map(dest => dest.SelectedOptions, source => string.Join(',', source.Answers), source => source.Answers.Count > 0);

        config
            .NewConfig<ProjectApplicationRespondentAnswer, RespondentAnswerDto>()
            .Map(dest => dest.CategoryId, source => source.Category)
            .Map(dest => dest.SectionId, source => source.Section)
            .Map(dest => dest.AnswerText, source => source.Response)
            .Map(dest => dest.SelectedOption, source => source.SelectedOptions, source => source.OptionType == "Single")
            .Map(dest => dest.Answers, source => source.SelectedOptions!.Split(',', StringSplitOptions.RemoveEmptyEntries), source => source.OptionType == "Multiple");
    }
}