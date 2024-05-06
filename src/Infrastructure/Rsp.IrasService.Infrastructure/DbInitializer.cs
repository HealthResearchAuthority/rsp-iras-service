using Rsp.IrasService.Domain.Entities;

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
                    ProjectCategory="Project category 1"
                },
                new IrasApplication {
                    Title="Example 2",
                    Location=Location.Scotland,
                    StartDate=DateTime.Now,
                    ApplicationCategories=["Application category 1", "Application category 2"],
                    ProjectCategory="Project category 2"
                }
            };

            context.IrasApplications.AddRange(irasApplications);
            context.SaveChanges();
        }
    }
}