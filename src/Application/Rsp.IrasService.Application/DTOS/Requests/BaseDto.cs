namespace Rsp.IrasService.Application.DTOS.Requests;

public class BaseDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }
}