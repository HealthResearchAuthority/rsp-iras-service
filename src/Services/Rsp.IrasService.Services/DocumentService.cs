using Mapster;
using Microsoft.AspNetCore.Http;
using Rsp.Service.Application.Contracts.Repositories;
using Rsp.Service.Application.Contracts.Services;
using Rsp.Service.Application.DTOS.Requests;
using Rsp.Service.Application.DTOS.Responses;
using Rsp.Service.Domain.Entities;

namespace Rsp.Service.Services;

/// <summary>
///     Service for managing modification documents for project records.
/// </summary>
public class DocumentService(IDocumentRepository documentRepository, IHttpContextAccessor httpContextAccessor) : IDocumentService
{
    public Task<int?> UpdateModificationDocument(ModificationDocumentDto modificationDocumentDto)
    {
        // Map the request DTO to the domain entity
        var modificationDocument = modificationDocumentDto.Adapt<ModificationDocument>();

        return documentRepository.UpdateModificationDocumentStatus(modificationDocument);
    }

    public async Task<ProjectDocumentsAuditTrailResponse> GetProjectDocumentsAuditTrail
    (
       string projectRecordId,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection
    )
    {
        var modifications = await documentRepository.GetProjectDocumentsAuditTrail(pageNumber, pageSize, sortField, sortDirection, projectRecordId);
        var totalCount = documentRepository.GetProjectDocumentsAuditTrail(pageNumber, pageSize, sortField, sortDirection, projectRecordId, true).Result.Count();

        return new ProjectDocumentsAuditTrailResponse
        {
            Items = modifications.Adapt<IEnumerable<ModificationDocumentsAuditTrailDto>>(),
            TotalCount = totalCount
        };
    }
}