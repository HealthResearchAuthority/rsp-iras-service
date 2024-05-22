using MediatR;
using Rsp.IrasService.Application.Requests;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Commands;

public class CreateApplicationCommand(CreateApplicationRequest createApplicationRequest) : IRequest<CreateApplicationResponse>
{
    public CreateApplicationRequest CreateApplicationRequest { get; set; } = createApplicationRequest;
}