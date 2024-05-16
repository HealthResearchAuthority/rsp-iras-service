using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.SeedData;

namespace Rsp.IrasService.Infrastructure
{
    public class IrasContext : DbContext
    {
        public IrasContext(DbContextOptions<IrasContext> options) : base(options)
        {
        }

        public DbSet<IrasApplication> IrasApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed the data as part of initial migration
            modelBuilder
                .Entity<IrasApplication>()
                .HasData(IrasApplicationData.Seed());
        }
    }
}