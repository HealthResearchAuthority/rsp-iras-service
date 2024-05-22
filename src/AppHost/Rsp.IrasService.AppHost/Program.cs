var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Rsp_IrasService>("rsp-irasservice");

builder.AddProject()

builder.Build().Run();