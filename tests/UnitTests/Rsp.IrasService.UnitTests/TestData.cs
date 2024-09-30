using AutoFixture;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;

namespace Rsp.IrasService.UnitTests;

/// <summary>
/// Data seeding class
/// </summary>
internal static class TestData
{
    /// <summary>
    /// Seeds the data with specified number of records
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="generator">Test data generator</param>
    /// <param name="records">Number of records to seed</param>
    /// <param name="updateStatus">
    /// Indicates whether to update the pending status. If true, index 2 and 4 will be updated
    /// </param>
    public static async Task<IList<ResearchApplication>> SeedData(IrasContext context, Generator<ResearchApplication> generator, int records, bool updateStatus = false)
    {
        // seed data using bogus
        var applications = generator
            .Take(records)
            .ToList();

        if (updateStatus)
        {
            // set the status to pending for a couple of records
            applications[2].Status = "pending";
            applications[4].Status = "pending";
        }

        await context.ResearchApplications.AddRangeAsync(applications);

        context.SaveChanges();

        return applications;
    }
}