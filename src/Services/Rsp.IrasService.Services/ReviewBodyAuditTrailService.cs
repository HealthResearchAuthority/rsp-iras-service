using KellermanSoftware.CompareNetObjects;
using Mapster;
using Rsp.IrasService.Application.Constants;
using Rsp.IrasService.Application.Contracts.Repositories;
using Rsp.IrasService.Application.Contracts.Services;
using Rsp.IrasService.Application.DTOS.Requests;
using Rsp.IrasService.Application.DTOS.Responses;
using Rsp.IrasService.Application.Helpers;
using Rsp.IrasService.Application.Specifications;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Services;

public class ReviewBodyAuditTrailService(IReviewBodyAuditTrailRepository repo) : IReviewBodyAuditTrailService
{
    public async Task<ReviewBodyAuditTrailResponse> GetAuditTrailForReviewBody(Guid id, int skip, int take)
    {
        var result = new ReviewBodyAuditTrailResponse();

        var itemsSpecification = new GetReviewBodyAuditTrailSpecification(id, skip, take);
        var countSpecification = new GetReviewBodyAuditTrailCountSpecification(id);

        var items = repo.GetForReviewBody(itemsSpecification);
        var totalCount = await repo.GetTotalNumberOfRecordsForReviewBody(countSpecification);

        var dtos = items.Adapt<IEnumerable<ReviewBodyAuditTrailDto>>();

        result.Items = dtos;
        result.TotalCount = totalCount;

        return result;
    }

    public async Task<IEnumerable<ReviewBodyAuditTrailDto>> LogRecords(IEnumerable<ReviewBodyAuditTrailDto> records)
    {
        var dbRecord = records.Adapt<IEnumerable<ReviewBodyAuditTrail>>();
        var recordCreated = await repo.CreateAuditRecords(dbRecord);

        return recordCreated.Adapt<IEnumerable<ReviewBodyAuditTrailDto>>();
    }

    public IEnumerable<ReviewBodyAuditTrailDto> GenerateAuditTrailDtoFromReviewBody(ReviewBodyDto dto, string userId, string action, ReviewBodyDto? oldDto = null)
    {
        var result = new List<ReviewBodyAuditTrailDto>();

        switch (action)
        {
            case ReviewBodyAuditTrailActions.Create:

                result.Add(new ReviewBodyAuditTrailDto
                {
                    DateTimeStamp = DateTime.UtcNow,
                    User = userId,
                    ReviewBodyId = dto.Id,
                    Description = $"{dto.OrganisationName} was created"
                });

                break;

            case ReviewBodyAuditTrailActions.Disable:

                result.Add(new ReviewBodyAuditTrailDto
                {
                    DateTimeStamp = DateTime.UtcNow,
                    User = userId,
                    ReviewBodyId = dto.Id,
                    Description = $"{dto.OrganisationName} was disabled"
                });

                break;

            case ReviewBodyAuditTrailActions.Enable:

                result.Add(new ReviewBodyAuditTrailDto
                {
                    DateTimeStamp = DateTime.UtcNow,
                    User = userId,
                    ReviewBodyId = dto.Id,
                    Description = $"{dto.OrganisationName} was enabled"
                });

                break;

            case ReviewBodyAuditTrailActions.Update:

                // compare objects
                var compareConfig = new CompareLogic(new ComparisonConfig
                {
                    MaxDifferences = 50,
                    MembersToInclude = new List<string>
                    {
                        nameof(ReviewBodyDto.CommaSeperatedCountries),
                        nameof(ReviewBodyDto.OrganisationName),
                        nameof(ReviewBodyDto.Description),
                        nameof(ReviewBodyDto.EmailAddress)
                    }
                });

                var compareDtosResult = new ObjectPropertyCompareHelper().Compare(dto, oldDto, compareConfig);

                if (!compareDtosResult.AreEqual)
                {
                    foreach (var changedProperty in compareDtosResult.Differences)
                    {
                        var propertyName = (changedProperty.PropertyName == nameof(ReviewBodyDto.CommaSeperatedCountries))
                            ? nameof(ReviewBodyDto.Countries)
                            : changedProperty.PropertyName;

                        // create an audit trail record for each modified property
                        result.Add(new ReviewBodyAuditTrailDto
                        {
                            DateTimeStamp = DateTime.UtcNow,
                            User = userId,
                            ReviewBodyId = dto.Id,
                            Description = $"{propertyName} was changed from {changedProperty.Object2Value} to {changedProperty.Object1Value}"
                        });
                    }
                }

                break;
        }

        return result;
    }
}