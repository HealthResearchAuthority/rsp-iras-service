using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Specifications;
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
                                      FullProjectTitle = projectRecord.FullProjectTitle,
                                      IsActive = projectRecord.IsActive,
                                      Status = projectRecord.Status,
                                      CreatedDate = projectRecord.CreatedDate,
                                      UpdatedDate = projectRecord.UpdatedDate,
                                      CreatedBy = projectRecord.CreatedBy,
                                      UpdatedBy = projectRecord.UpdatedBy,
                                      IrasId = projectRecord.IrasId,
                                      ProjectModifications = projectRecord.ProjectModifications,
                                      ShortProjectTitle = projectRecordAnswer != null && projectRecordAnswer.Response != null ? projectRecordAnswer.Response : projectRecord.ShortProjectTitle
                                  };

        // Apply filtering to ProjectRecords
        var query = joinedProjectTitles
            .WithSpecification(projectsSpecification);

        // Count before pagination
        var count = await query.CountAsync();

        // Apply sorting
        query = (sortField?.ToLower(), sortDirection?.ToLower()) switch
        {
            ("title", "asc") => query.OrderBy(x => x.ShortProjectTitle),
            ("title", "desc") => query.OrderByDescending(x => x.ShortProjectTitle),
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

    public async Task<(IEnumerable<CompleteProjectRecordResponse>, int)> GetPaginatedProjectRecords
    (
        ProjectRecordSearchRequest request,
        int pageIndex,
        int? pageSize,
        string? sortField,
        string? sortDirection
    )
    {
        var records = irasContext
            .ProjectRecords
            .Include(x => x.ProjectRecordAnswers)
            .AsQueryable();

        // apply filters
        records = ApplyFilters(request, records);

        var count = await records.CountAsync();

        var projectRecords = GenerateCompleteProjectRecordObjects(records);

        // Apply sorting
        projectRecords = (sortField?.ToLower(), sortDirection?.ToLower()) switch
        {
            ("title", "asc") => projectRecords.OrderBy(x => x.ShortProjectTitle),
            ("title", "desc") => projectRecords.OrderByDescending(x => x.ShortProjectTitle),
            ("irasid", "asc") => projectRecords.OrderBy(x => x.IrasId),
            ("irasid", "desc") => projectRecords.OrderByDescending(x => x.IrasId),
            ("leadnation", "asc") => projectRecords.OrderBy(x => x.LeadNation),
            ("leadnation", "desc") => projectRecords.OrderByDescending(x => x.LeadNation),
            _ => projectRecords.OrderBy(x => x.IrasId),
        };

        // Apply pagination
        if (pageSize.HasValue)
        {
            projectRecords = projectRecords
                .Skip((pageIndex - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        var result = projectRecords.ToList();

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

        entity.ShortProjectTitle = irasApplication.ShortProjectTitle;
        entity.FullProjectTitle = irasApplication.FullProjectTitle;
        entity.UpdatedDate = irasApplication.UpdatedDate;
        entity.CreatedDate = irasApplication.CreatedDate;
        entity.UpdatedBy = irasApplication.UpdatedBy;
        entity.Status = irasApplication.Status;
        entity.IrasId = irasApplication.IrasId;

        await irasContext.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Deletes a project record from the database that matches the given specification.
    /// </summary>
    /// <param name="specification">Specification to identify the project record to delete.</param>
    public async Task DeleteProjectRecord(GetApplicationSpecification specification)
    {
        // Find the project record matching the specification
        var projectRecord = await irasContext
            .ProjectRecords
            .WithSpecification(specification)
            .SingleOrDefaultAsync();

        // If found, remove it from the context and save changes
        if (projectRecord != null)
        {
            irasContext.ProjectRecords.Remove(projectRecord);

            await irasContext.SaveChangesAsync();
        }
    }

    private IQueryable<ProjectRecord> ApplyFilters(ProjectRecordSearchRequest request, IQueryable<ProjectRecord> records)
    {
        // get only active records
        if (request.ActiveProjectsOnly)
        {
            records = records.Where(x => x.IsActive);
        }

        // match IRAS ID
        if (!string.IsNullOrEmpty(request.IrasId))
        {
            records = records.Where(x => x.IrasId != null && x.IrasId.ToString()!.Contains(request.IrasId));
        }

        // Filter by short project title
        if (!string.IsNullOrEmpty(request.ShortProjectTitle))
        {
            records = records.Where(x => x.ProjectRecordAnswers.Any(
                y =>
                    y.QuestionId == ProjectRecordConstants.ShortProjectTitle
                    && y.Response != null
                    && y.Response.Contains(request.ShortProjectTitle)));
        }

        // Filter by chief investigator name
        if (!string.IsNullOrEmpty(request.ChiefInvestigatorName))
        {
            records = records.Where(x => x.ProjectRecordAnswers.Any(
                y =>
                    y.QuestionId == ProjectRecordConstants.ChiefInvestigator
                    && y.Response != null
                    && y.Response.Contains(request.ChiefInvestigatorName)));
        }

        // Filter by sponsor organisation
        if (!string.IsNullOrEmpty(request.SponsorOrganisation))
        {
            records = records.Where(x => x.ProjectRecordAnswers.Any(
                y =>
                    y.QuestionId == ProjectRecordConstants.SponsorOrganisation
                    && y.Response != null
                    && y.Response.Contains(request.SponsorOrganisation)));
        }

        // Filter by lead nation
        if (request.LeadNation.Any())
        {
            var leadNationCode = request.LeadNation.Select(
                x => ProjectRecordConstants.NationIdMap.FirstOrDefault(y =>
                    y.Value.Equals(x, StringComparison.InvariantCultureIgnoreCase)).Key);

            records = records.Where(x => x.ProjectRecordAnswers.Any(
                y =>
                    y.QuestionId == ProjectRecordConstants.LeadNation
                    && y.SelectedOptions != null
                    && leadNationCode.Any(r => y.SelectedOptions.Contains(r))));
        }

        // Filter by participating nation
        if (request.ParticipatingNation.Any())
        {
            var participatingNationCode = request.ParticipatingNation.Select(
                x => ProjectRecordConstants.NationIdMap.FirstOrDefault(y =>
                    y.Value.Equals(x, StringComparison.InvariantCultureIgnoreCase)).Key);

            records = records.Where(x => x.ProjectRecordAnswers.Any(
            y =>
                y.QuestionId == ProjectRecordConstants.ParticipatingNation
                && y.SelectedOptions != null
                && participatingNationCode.Any(r => y.SelectedOptions.Contains(r))));
        }

        return records;
    }

    private IEnumerable<CompleteProjectRecordResponse> GenerateCompleteProjectRecordObjects(IQueryable<ProjectRecord> records)
    {
        var result = records.Select(x => new CompleteProjectRecordResponse
        {
            Id = x.Id.ToString(),
            IrasId = x.IrasId,
            ChiefInvestigator = x.ProjectRecordAnswers
                       .Where(a => a.QuestionId == ProjectRecordConstants.ChiefInvestigator)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
            LeadNation = x.ProjectRecordAnswers
                       .Where(a => a.QuestionId == ProjectRecordConstants.LeadNation)
                       .Select(a => a.SelectedOptions)
                       .FirstOrDefault() ?? string.Empty,
            ParticipatingNation = x.ProjectRecordAnswers
                       .Where(a => a.QuestionId == ProjectRecordConstants.ParticipatingNation)
                       .Select(a => a.SelectedOptions)
                       .FirstOrDefault() ?? string.Empty,
            ShortProjectTitle = x.ProjectRecordAnswers
                       .Where(a => a.QuestionId == ProjectRecordConstants.ShortProjectTitle)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
            SponsorOrganisation = x.ProjectRecordAnswers
                       .Where(a => a.QuestionId == ProjectRecordConstants.SponsorOrganisation)
                       .Select(a => a.Response)
                       .FirstOrDefault() ?? string.Empty,
            CreatedDate = x.CreatedDate,
            Status = x.Status
        });

        return NormaliseRecords(result);
    }

    private IEnumerable<CompleteProjectRecordResponse> NormaliseRecords(IQueryable<CompleteProjectRecordResponse> records)
    {
        var recordsList = records.ToList();
        foreach (var record in recordsList)
        {
            record.LeadNation = ProjectRecordConstants.NationIdMap.TryGetValue(record.LeadNation, out var leadCountryName) ?
                leadCountryName :
                string.Empty;

            record.ParticipatingNation = ProjectRecordConstants.NationIdMap.TryGetValue(record.ParticipatingNation, out var participatingCountryName) ?
                participatingCountryName :
                string.Empty;
        }

        return recordsList;
    }
}