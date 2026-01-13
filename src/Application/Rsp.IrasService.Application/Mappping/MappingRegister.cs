using Mapster;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Application.Mappping;

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

        // ApplicationRequest -> ProjectRecord mapping
        config
            .NewConfig<ApplicationRequest, ProjectRecord>()
            .Map(dest => dest.CreatedDate, source => source.StartDate)
            .Map(dest => dest.UpdatedDate, _ => DateTime.Now);

        // ProjectRecord -> ApplicationRequest mapping
        config
            .NewConfig<ProjectRecord, ApplicationRequest>()
            .Map(dest => dest.StartDate, source => source.CreatedDate);

        // RespondentAnswerDto -> ProjectRecordAnswer mapping
        config
            .NewConfig<RespondentAnswerDto, ProjectRecordAnswer>()
            .Ignore(ra => ra.ProjectRecordId)
            .Map(dest => dest.VersionId, source => source.VersionId)
            .Map(dest => dest.Category, source => source.CategoryId)
            .Map(dest => dest.Section, source => source.SectionId)
            .Map(dest => dest.Response, source => source.AnswerText)
            .Map(dest => dest.SelectedOptions, source => source.SelectedOption, source => !string.IsNullOrWhiteSpace(source.SelectedOption))
            .Map(dest => dest.SelectedOptions, source => string.Join(',', source.Answers), source => source.Answers.Count > 0);

        // ProjectRecordAnswer -> RespondentAnswerDto mapping
        config
            .NewConfig<ProjectRecordAnswer, RespondentAnswerDto>()
            .Map(dest => dest.VersionId, source => source.VersionId)
            .Map(dest => dest.CategoryId, source => source.Category)
            .Map(dest => dest.SectionId, source => source.Section)
            .Map(dest => dest.AnswerText, source => source.Response)
            .Map(dest => dest.SelectedOption, source => source.SelectedOptions, source => source.OptionType == "Single")
            .Map(dest => dest.Answers, source => source.SelectedOptions!.Split(',', StringSplitOptions.RemoveEmptyEntries), source => source.OptionType == "Multiple");

        // RespondentAnswerDto -> ProjectModificationChangeAnswer mapping
        config
            .NewConfig<RespondentAnswerDto, ProjectModificationChangeAnswer>()
            .Ignore(ra => ra.ProjectModificationChangeId)
            .Ignore(ra => ra.ProjectRecordId)
            .Map(dest => dest.VersionId, source => source.VersionId)
            .Map(dest => dest.Category, source => source.CategoryId)
            .Map(dest => dest.Section, source => source.SectionId)
            .Map(dest => dest.Response, source => source.AnswerText)
            .Map(dest => dest.SelectedOptions, source => source.SelectedOption, source => !string.IsNullOrWhiteSpace(source.SelectedOption))
            .Map(dest => dest.SelectedOptions, source => string.Join(',', source.Answers), source => source.Answers.Count > 0);

        // ProjectModificationChangeAnswer -> RespondentAnswerDto mapping
        config
            .NewConfig<ProjectModificationChangeAnswer, RespondentAnswerDto>()
            .Map(dest => dest.VersionId, source => source.VersionId)
            .Map(dest => dest.CategoryId, source => source.Category)
            .Map(dest => dest.SectionId, source => source.Section)
            .Map(dest => dest.AnswerText, source => source.Response)
            .Map(dest => dest.SelectedOption, source => source.SelectedOptions, source => source.OptionType == "Single")
            .Map(dest => dest.Answers, source => source.SelectedOptions!.Split(',', StringSplitOptions.RemoveEmptyEntries), source => source.OptionType == "Multiple");

        // RespondentAnswerDto -> ProjectModificationAnswer mapping
        config
            .NewConfig<RespondentAnswerDto, ProjectModificationAnswer>()
            .Ignore(ra => ra.ProjectModificationId)
            .Ignore(ra => ra.ProjectRecordId)
            .Map(dest => dest.VersionId, source => source.VersionId)
            .Map(dest => dest.Category, source => source.CategoryId)
            .Map(dest => dest.Section, source => source.SectionId)
            .Map(dest => dest.Response, source => source.AnswerText)
            .Map(dest => dest.SelectedOptions, source => source.SelectedOption, source => !string.IsNullOrWhiteSpace(source.SelectedOption))
            .Map(dest => dest.SelectedOptions, source => string.Join(',', source.Answers), source => source.Answers.Count > 0);

        // ProjectModificationAnswer -> RespondentAnswerDto mapping
        config
            .NewConfig<ProjectModificationAnswer, RespondentAnswerDto>()
            .Map(dest => dest.VersionId, source => source.VersionId)
            .Map(dest => dest.CategoryId, source => source.Category)
            .Map(dest => dest.SectionId, source => source.Section)
            .Map(dest => dest.AnswerText, source => source.Response)
            .Map(dest => dest.SelectedOption, source => source.SelectedOptions, source => source.OptionType == "Single")
            .Map(dest => dest.Answers, source => source.SelectedOptions!.Split(',', StringSplitOptions.RemoveEmptyEntries), source => source.OptionType == "Multiple");

        // ModificationDocumentAnswerDto -> ModificationDocumentAnswer mapping
        config
            .NewConfig<ModificationDocumentAnswerDto, ModificationDocumentAnswer>()
            .Map(dest => dest.Id, src => src.Id, src => src.Id != Guid.Empty) // only map if Id is not empty
            .Map(dest => dest.ModificationDocumentId, source => source.ModificationDocumentId)
            .Map(dest => dest.QuestionId, source => source.QuestionId)
            .Map(dest => dest.VersionId, source => source.VersionId)
            .Map(dest => dest.Category, source => source.CategoryId)
            .Map(dest => dest.Section, source => source.SectionId)
            .Map(dest => dest.Response, source => source.AnswerText)
            .Map(dest => dest.SelectedOptions, source => source.SelectedOption, source => !string.IsNullOrWhiteSpace(source.SelectedOption))
            .Map(dest => dest.SelectedOptions, source => string.Join(',', source.Answers), source => source.Answers.Count > 0);

        // ModificationDocumentAnswer -> ModificationDocumentAnswerDto mapping
        config
            .NewConfig<ModificationDocumentAnswer, ModificationDocumentAnswerDto>()
            .Map(dest => dest.Id, source => source.Id)
            .Map(dest => dest.ModificationDocumentId, source => source.ModificationDocumentId)
            .Map(dest => dest.QuestionId, source => source.QuestionId)
            .Map(dest => dest.VersionId, source => source.VersionId)
            .Map(dest => dest.CategoryId, source => source.Category)
            .Map(dest => dest.SectionId, source => source.Section)
            .Map(dest => dest.AnswerText, source => source.Response)
            .Map(dest => dest.SelectedOption, source => source.SelectedOptions, source => source.OptionType == "Single")
            .Map(dest => dest.Answers, source => source.SelectedOptions!.Split(',', StringSplitOptions.RemoveEmptyEntries), source => source.OptionType == "Multiple");
    }
}