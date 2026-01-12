using MediatR;
using Rsp.Service.Application.DTOS.Requests;

namespace Rsp.Service.Application.CQRS.Queries;

/// <summary>
/// Query to retrieve respondent answers for a specific project modification change.
/// </summary>
public class GetModificationChangeAnswersQuery : IRequest<IEnumerable<RespondentAnswerDto>>
{
    /// <summary>
    /// Gets or sets the project modification change identifier.
    /// </summary>
    public Guid ProjectModificationChangeId { get; set; }

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the optional category identifier for filtering answers.
    /// </summary>
    public string? CategoryId { get; set; }
}