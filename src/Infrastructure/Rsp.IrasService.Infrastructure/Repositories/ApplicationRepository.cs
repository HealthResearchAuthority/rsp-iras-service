using Rsp.IrasService.Application.Contracts;
using Rsp.IrasService.Domain.Entities;

namespace Rsp.IrasService.Infrastructure.Repositories;

public class ApplicationRepository(IrasContext irasContext) : IApplicationRepository
{
    public async Task<IrasApplication> CreateApplication(IrasApplication irasApplication)
    {
        var entity = await irasContext.IrasApplications.AddAsync(irasApplication);

        await irasContext.SaveChangesAsync();

        return entity.Entity;
    }
}