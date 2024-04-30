using Microsoft.EntityFrameworkCore;
using Rsp.IrasService.Domain.Models;

namespace Rsp.IrasService.Infrastructure
{
    public class IrasContext : DbContext
    {
        public IrasContext(DbContextOptions<IrasContext> options) : base(options) 
        {
        }

        public DbSet<IrasApplication> IrasApplications { get; set; }
    }
}
