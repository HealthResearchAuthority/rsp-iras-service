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
            .NewConfig<ApplicationRequest, ResearchApplication>()
            .Map(dest => dest.CreatedDate, source => source.StartDate)
            .Map(dest => dest.UpdatedDate, _ => DateTime.Now);

        config
            .NewConfig<ResearchApplication, ApplicationRequest>()
            .Map(dest => dest.StartDate, source => source.CreatedDate);

        config
            .NewConfig<RespondentDto, Respondent>()
            .Map(dest => dest.Email, source => source.EmailAddress);

        config
            .NewConfig<RespondentAnswerDto, RespondentAnswer>()
            .Ignore(ra => ra.RespondentId)
            .Ignore(ra => ra.ApplicationId)
            .Map(dest => dest.Category, source => source.CategoryId)
            .Map(dest => dest.Section, source => source.SectionId)
            .Map(dest => dest.Response, source => source.AnswerText)
            .Map(dest => dest.SelectedOptions, source => source.SelectedOption, source => !string.IsNullOrWhiteSpace(source.SelectedOption))
            .Map(dest => dest.SelectedOptions, source => string.Join(',', source.Answers), source => source.Answers.Count > 0);

        config
            .NewConfig<RespondentAnswer, RespondentAnswerDto>()
            .Map(dest => dest.CategoryId, source => source.Category)
            .Map(dest => dest.SectionId, source => source.Section)
            .Map(dest => dest.AnswerText, source => source.Response)
            .Map(dest => dest.SelectedOption, source => source.SelectedOptions, source => source.OptionType == "Single")
            .Map(dest => dest.Answers, source => source.SelectedOptions!.Split(',', StringSplitOptions.RemoveEmptyEntries), source => source.OptionType == "Multiple");
    }
}