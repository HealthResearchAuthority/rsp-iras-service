using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ProjectRecordRepository(IrasContext irasContext) : IProjectRecordRepository
{
    public async Task<ProjectRecord> CreateProjectRecord(ProjectRecord irasApplication, ProjectPersonnel respondent)
    {
        var respondentEntity = await irasContext
            .ProjectPersonnels
            .SingleOrDefaultAsync(r => r.Id == respondent.Id);

        if (respondentEntity == null)
        {
            await irasContext.ProjectPersonnels.AddAsync(respondent);
        }

        irasApplication.ProjectPersonnelId = respondent.Id;

        var entity = await irasContext.ProjectRecords.AddAsync(irasApplication);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<ProjectRecord?> GetProjectRecord(ISpecification<ProjectRecord> specification)
    {
        return await irasContext
            .ProjectRecords
            .WithSpecification(specification)
            .FirstOrDefaultAsync();
    }

    public Task<IEnumerable<ProjectRecord>> GetProjectRecords(ISpecification<ProjectRecord> specification)
    {
        var result = irasContext
            .ProjectRecords
            .WithSpecification(specification)
            .AsEnumerable();

        return Task.FromResult(result);
    }

    public async Task<(IEnumerable<ProjectRecord>, int)> GetPaginatedProjectRecords
    (
        ISpecification<ProjectRecord> projectsSpecification,
        ISpecification<ProjectRecordAnswer> projectTitlesSpecification,
        int pageIndex,
        int? pageSize,
        string? sortField,
        string? sortDirection
    )
    {
        // Apply filtering to ProjectRecordAnswers
        var filteredTitles = irasContext
            .ProjectRecordAnswers
            .WithSpecification(projectTitlesSpecification);

        // Join ProjectRecords with ProjectRecordAnswers (left join)
        var joinedProjectTitles = from projectRecord in irasContext.ProjectRecords
                                  join projectRecordAnswer in filteredTitles
                        on projectRecord.Id equals projectRecordAnswer.ProjectRecordId into titleGroup
                    from projectRecordAnswer in titleGroup.DefaultIfEmpty()
                    select new ProjectRecord
                    {
                        Id = projectRecord.Id,
                        ProjectPersonnelId = projectRecord.ProjectPersonnelId,
                        Description = projectRecord.Description,
                        IsActive = projectRecord.IsActive,
                        Status = projectRecord.Status,
                        CreatedDate = projectRecord.CreatedDate,
                        UpdatedDate = projectRecord.UpdatedDate,
                        CreatedBy = projectRecord.CreatedBy,
                        UpdatedBy = projectRecord.UpdatedBy,
                        IrasId = projectRecord.IrasId,
                        ProjectModifications = projectRecord.ProjectModifications,
                        Title = projectRecordAnswer != null && projectRecordAnswer.Response != null ? projectRecordAnswer.Response : projectRecord.Title
                    };

        // Apply filtering to ProjectRecords
        var query = joinedProjectTitles
            .WithSpecification(projectsSpecification);

        // Count before pagination
        var count = await query.CountAsync();

        // Apply sorting
        query = (sortField?.ToLower(), sortDirection?.ToLower()) switch
        {
            ("title", "asc") => query.OrderBy(x => x.Title),
            ("title", "desc") => query.OrderByDescending(x => x.Title),
            ("status", "asc") => query.OrderBy(x => x.Status),
            ("status", "desc") => query.OrderByDescending(x => x.Status),
            ("createddate", "asc") => query.OrderBy(x => x.CreatedDate),
            ("createddate", "desc") => query.OrderByDescending(x => x.CreatedDate),
            ("irasid", "asc") => query.OrderBy(x => x.IrasId),
            ("irasid", "desc") => query.OrderByDescending(x => x.IrasId),
            _ => query.OrderByDescending(x => x.CreatedDate),
        };

        // Apply pagination
        if (pageSize.HasValue)
        {
            query = query
                .Skip((pageIndex - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        var result = await query.ToListAsync();

        return (result, count);
    }

    public async Task<ProjectRecord?> UpdateProjectRecord(ProjectRecord irasApplication)
    {
        var entity = await irasContext
            .ProjectRecords
            .FirstOrDefaultAsync(record => record.Id == irasApplication.Id);

        if (entity == null)
        {
            return null;
        }

        entity.Title = irasApplication.Title;
        entity.Description = irasApplication.Description;
        entity.UpdatedDate = irasApplication.UpdatedDate;
        entity.CreatedDate = irasApplication.CreatedDate;
        entity.UpdatedBy = irasApplication.UpdatedBy;
        entity.Status = irasApplication.Status;
        entity.IrasId = irasApplication.IrasId;

        await irasContext.SaveChangesAsync();

        return entity;
    }
}