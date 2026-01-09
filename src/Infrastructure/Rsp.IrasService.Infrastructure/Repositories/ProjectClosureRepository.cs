using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ProjectClosureRepository(IrasContext irasContext) : IProjectClosureRepository
{
    public async Task<ProjectClosure> CreateProjectClosure(ProjectClosure projectClosure)
    {
        var entity = await irasContext.ProjectClosures.AddAsync(projectClosure);
        await irasContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<ProjectClosure?> GetProjectClosure(ISpecification<ProjectClosure> specification)
    {
        return await irasContext
           .ProjectClosures
           .WithSpecification(specification)
           .FirstOrDefaultAsync();
    }

    public async Task<ProjectClosure?> UpdateProjectClosureStatus(ISpecification<ProjectClosure> specification, string status, string userId)
    {
        var projectClosure = await irasContext
           .ProjectClosures
           .WithSpecification(specification)
           .Include(pc => pc.ProjectRecord)
           .FirstOrDefaultAsync();

        if (projectClosure != null)
        {
            projectClosure.Status = status;
            projectClosure.UpdatedBy = userId;
            projectClosure.DateActioned = DateTime.UtcNow;

            if (status == nameof(ProjectClosureStatus.Authorised))
            {
                var projectRecord = projectClosure.ProjectRecord;
                projectRecord.Status = ProjectRecordStatus.Closed;
                projectRecord.UpdatedBy = userId;
                projectRecord.UpdatedDate = DateTime.UtcNow;
            }

            await irasContext.SaveChangesAsync();
        }

        return projectClosure;
    }

    public IEnumerable<ProjectClosure> GetProjectClosuresBySponsorOrganisationUser
    (
       ProjectClosuresSearchRequest searchQuery,
       int pageNumber,
       int pageSize,
       string sortField,
       string sortDirection,
       Guid sponsorOrganisationUserId
    )
    {
        var projectclosures = ProjectClosuresBySponsorOrganisationUserQuery(sponsorOrganisationUserId);
        var filtered = FilterProjectClosuresBySponsorOrganisationUserQuery(projectclosures, searchQuery);
        var sorted = SortProjectClosures(filtered, sortField, sortDirection);

        return sorted
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public int GetProjectClosuresBySponsorOrganisationUserCount(ProjectClosuresSearchRequest searchQuery, Guid sponsorOrganisationUserId)
    {
        var projectclosures = ProjectClosuresBySponsorOrganisationUserQuery(sponsorOrganisationUserId);
        return FilterProjectClosuresBySponsorOrganisationUserQuery(projectclosures, searchQuery).Count();
    }

    private static IEnumerable<ProjectClosure> SortProjectClosures(
      IEnumerable<ProjectClosure> projectClosures, string sortField, string sortDirection)
    {
        Func<ProjectClosure, object>? keySelector = sortField switch
        {
            nameof(ProjectClosure.ShortProjectTitle) => x => x.ShortProjectTitle.ToLowerInvariant(),
            nameof(ProjectClosure.IrasId) => x => x.IrasId,
            nameof(ProjectClosure.SentToSponsorDate) => x => x.SentToSponsorDate,
            nameof(ProjectClosure.ClosureDate) => x => x.ClosureDate,
            nameof(ProjectClosure.Status) => x => x.Status,
            _ => null
        };

        if (keySelector == null)
            return projectClosures;

        return sortDirection == "desc"
            ? projectClosures.OrderByDescending(keySelector)
            : projectClosures.OrderBy(keySelector);
    }

    private IQueryable<ProjectClosure> ProjectClosuresBySponsorOrganisationUserQuery(Guid sponsorOrganisationUserId)
    {
        var rtsId = irasContext.SponsorOrganisationsUsers
            .Where(u => u.Id == sponsorOrganisationUserId)
            .Select(u => u.RtsId)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(rtsId))
        {
            return irasContext.ProjectClosures.Where(_ => false);
        }

        var projectClosures = irasContext.ProjectClosures.AsNoTracking();
        var projectAnswers = irasContext.ProjectRecordAnswers.AsNoTracking();

        var query =
            from pc in projectClosures
            join pa in projectAnswers
                on pc.ProjectRecordId equals pa.ProjectRecordId
            where pa.QuestionId == ProjectRecordConstants.SponsorOrganisation
               && pa.Response == rtsId
            select pc;

        return query;
    }

    private static IEnumerable<ProjectClosure> FilterProjectClosuresBySponsorOrganisationUserQuery(
    IQueryable<ProjectClosure> projectClosures,
    ProjectClosuresSearchRequest searchQuery)
    {
        var term = searchQuery.SearchTerm?.Trim();

        return projectClosures
            .AsEnumerable()
            .Where(x =>
                string.IsNullOrEmpty(term) ||
                (x.IrasId.HasValue && x.IrasId.Value.ToString().Contains(term, StringComparison.OrdinalIgnoreCase))
            );
    }
}