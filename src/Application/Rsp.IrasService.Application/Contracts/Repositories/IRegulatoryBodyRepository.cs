﻿using Ardalis.Specification;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Application.Contracts.Repositories;

public interface IRegulatoryBodyRepository
{
    Task<IEnumerable<RegulatoryBody>> GetRegulatoryBodies(ISpecification<RegulatoryBody> specification);

    Task<RegulatoryBody?> GetRegulatoryBody(ISpecification<RegulatoryBody> specification);

    Task<RegulatoryBody> CreateRegulatoryBody(RegulatoryBody regulatoryBody);

    Task<RegulatoryBody> UpdateRegulatoryBody(RegulatoryBody regulatoryBody);

    Task<RegulatoryBody?> DisableRegulatoryBody(Guid id);

    Task<RegulatoryBody?> EnableRegulatoryBody(Guid id);

    Task<RegulatoryBodyUser> AddUserToRegulatoryBody(RegulatoryBodyUser user);

    Task<RegulatoryBodyUser?> RemoveUserFromRegulatoryBody(Guid regulatoryBodyId, Guid userId);

    Task<int> GetRegulatoryBodyCount(ReviewBodySearchRequest searchQuery );

    Task<List<RegulatoryBodyUser>> GetRegulatoryBodiesUsersByUserId(Guid userId);
    Task<List<RegulatoryBodyUser>> GetRegulatoryBodiesUsersByIds(List<Guid> ids);
}