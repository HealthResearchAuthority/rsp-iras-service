using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure;

namespace Rsp.IrasService.UnitTests;

/// <summary>
///     Data seeding class
/// </summary>
public static class TestData
{
    /// <summary>
    ///     Seeds the data with specified number of records
    /// </summary>
    /// <param name="context">Database context</param>
    /// <param name="generator">Test data generator</param>
    /// <param name="records">Number of records to seed</param>
    /// <param name="updateStatus">
    ///     Indicates whether to update the pending status. If true, index 2 and 4 will be updated
    /// </param>
    public static async Task<IList<ResearchApplication>> SeedData(IrasContext context,
        Generator<ResearchApplication> generator, int records, bool updateStatus = false)
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

        await context.SaveChangesAsync();

        return applications;
    }

    public static async Task<IList<EmailTemplate>> SeedData(IrasContext context,
        Generator<EmailTemplate> generator, int records)
    {
        // seed data using bogus
        var templates = generator
            .Take(records)
            .ToList();

        await context.EmailTemplates.AddRangeAsync(templates);

        await context.SaveChangesAsync();

        return templates;
    }

    public static async Task<IList<EventType>> SeedData(IrasContext context,
        Generator<EventType> generator, int records)
    {
        // seed data using bogus
        var items = generator
            .Take(records)
            .ToList();

        await context.EventTypes.AddRangeAsync(items);

        await context.SaveChangesAsync();

        return items;
    }

    public static async Task<IList<ReviewBody>> SeedData(IrasContext context,
        Generator<ReviewBody> generator, int records)
    {
        // seed data using bogus
        var items = generator
            .Take(records)
            .ToList();

        await context.ReviewBodies.AddRangeAsync(items);

        await context.SaveChangesAsync();

        return items;
    }

    public static async Task<IList<ReviewBodyAuditTrail>> SeedData(IrasContext context,
        Generator<ReviewBodyAuditTrail> generator, int records)
    {
        // seed data using bogus
        var items = generator
            .Take(records)
            .ToList();

        await context.ReviewBodiesAuditTrail.AddRangeAsync(items);

        await context.SaveChangesAsync();

        return items;
    }
}