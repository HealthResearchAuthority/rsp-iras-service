namespace Rsp.IrasService.Application.DTOS.Responses;

public class ErrorResponse
{
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string Details { get; set; } = null!;
}