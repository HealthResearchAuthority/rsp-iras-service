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
            .Map(dest => dest.CreatedDate, source => source.StartDate);

        config
            .NewConfig<ResearchApplication, ApplicationRequest>()
            .Map(dest => dest.StartDate, source => source.CreatedDate);

        config
            .NewConfig<RespondentDto, Respondent>()
            .Map(dest => dest.Email, source => source.EmailAddress);

        //config
        //    .NewConfig<Condition, ConditionDto>();

        //config
        //    .NewConfig<QuestionRule, RuleDto>();

        //// Question to GetQuestionsResponse mappings
        //config
        //    .NewConfig<Question, QuestionDto>()
        //    .MapToConstructor(true)
        //    .Map(dest => dest.Category, source => source.QuestionCategoryId)
        //    .Map(dest => dest.SectionId, source => source.QuestionSection.SectionId)
        //    .Map(dest => dest.Section, source => source.QuestionSection.SectionName)
        //    .Map(dest => dest.IsMandatory, _ => true, source => source.Conformance == "Mandatory")
        //    .Map(dest => dest.IsOptional, _ => true, source => source.Conformance == "Optional")
        //    .Map(dest => dest.Rules, source => source.QuestionRules);
    }
}