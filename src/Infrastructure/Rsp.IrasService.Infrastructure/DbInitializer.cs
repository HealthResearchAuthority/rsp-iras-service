using Rsp.IrasService.Domain.Models;

namespace Rsp.IrasService.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(IrasContext context)
        {
            context.Database.EnsureCreated();

            if (context.IrasApplications.Any())
            {
                return;
            }

            var irasApplications = new IrasApplication[]
            {
                new IrasApplication {
                    Title="Example 1",
                    Location=Location.England,
                    StartDate=DateTime.Now,
                    ApplicationCategories=["Application category 1", "Application category 2"],
                    ProjectCategories=["Project category 1", "Project category 2"]
                },
                new IrasApplication {
                    Title="Example 2",
                    Location=Location.Scotland,
                    StartDate=DateTime.Now,
                    ApplicationCategories=["Application category 1", "Application category 2"],
                    ProjectCategories=["Project category 1", "Project category 2"]
                }
            };

            foreach (var application in irasApplications )
            {
                context.IrasApplications.Add(application);
            }
            context.SaveChanges();
        }
    }
}
