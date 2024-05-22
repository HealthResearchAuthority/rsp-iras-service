using MediatR;
using Rsp.IrasService.Application.Requests;
using Rsp.IrasService.Application.Responses;

namespace Rsp.IrasService.Application.Commands;

public class UpdateApplicationCommand(int id, CreateApplicationRequest createApplicationRequest) : IRequest<CreateApplicationResponse>
{
    public int Id { get; set; } = id;

    public CreateApplicationRequest CreateApplicationRequest { get; set; } = createApplicationRequest;
}