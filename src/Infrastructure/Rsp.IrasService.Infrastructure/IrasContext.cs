using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Domain.Entities;
using Rsp.IrasService.Infrastructure.EntitiesConfiguration;

namespace Rsp.IrasService.Infrastructure
{
    public class IrasContext(DbContextOptions<IrasContext> options) : DbContext(options)
    {
        public DbSet<ResearchApplication> ResearchApplications { get; set; }
        public DbSet<Respondent> Respondents { get; set; }
        public DbSet<RespondentAnswer> RespondentAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ResearchApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new RespondentConfiguration());
            modelBuilder.ApplyConfiguration(new RespondentAnswerConfiguration());
        }
    }
}