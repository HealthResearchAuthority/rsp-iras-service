namespace Rsp.IrasService.Domain.Interfaces;

public interface IUpdatable
{
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}