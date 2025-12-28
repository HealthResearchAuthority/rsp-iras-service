namespace Rsp.IrasService.Domain.Interfaces;

public interface ICreatable
{
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}