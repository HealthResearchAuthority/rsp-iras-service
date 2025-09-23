﻿using MediatR;
using Rsp.IrasService.Application.DTOS.Requests;

namespace Rsp.IrasService.Application.CQRS.Commands;

/// <summary>
/// Command to save document answers for a modification.
/// </summary>
public class SaveModificationDocumentAnswersCommand(List<ModificationDocumentAnswerDto> request) : IRequest
{
    /// <summary>
    /// Gets or sets the modification document answers request containing the document answers to be saved.
    /// </summary>
    public List<ModificationDocumentAnswerDto> ModificationDocumentAnswerRequest { get; set; } = request;
}