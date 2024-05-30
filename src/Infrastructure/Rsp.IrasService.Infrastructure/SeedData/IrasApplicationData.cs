using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Domain.Enums;

namespace Rsp.IrasService.Infrastructure.SeedData;

internal static class IrasApplicationData
{
    public static IList<IrasApplication> Seed()
    {
        return
        [
            new()
            {
                Id = 1,
                Title = "Example 1",
                Location = Location.England,
                StartDate = DateTime.Now,
                ApplicationCategories = ["Application category 1", "Application category 2"],
                ProjectCategory = "Project category 1",
                Status = "pending"
            },
            new()
            {
                Id = 2,
                Title = "Example 2",
                Location = Location.Scotland,
                StartDate = DateTime.Now,
                ApplicationCategories = ["Application category 1", "Application category 2"],
                ProjectCategory = "Project category 2",
                Status = "approved"
            }
        ];
    }
}