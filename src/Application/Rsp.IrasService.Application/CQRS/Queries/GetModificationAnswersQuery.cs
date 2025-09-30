using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Queries;

/// <summary>
/// Query to retrieve respondent answers for a specific project modification.
/// </summary>
public class GetModificationAnswersQuery : IRequest<IEnumerable<RespondentAnswerDto>>
{
    /// <summary>
    /// Gets or sets the project modification change identifier.
    /// </summary>
    public Guid ProjectModificationId { get; set; }

    /// <summary>
    /// Gets or sets the project record identifier.
    /// </summary>
    public string ProjectRecordId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the optional category identifier for filtering answers.
    /// </summary>
    public string? CategoryId { get; set; }
}