namespace Rsp.IrasService.Domain.Entities;

public class ReviewBodyUsers
{
    public Guid ReviewBodyId { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateAdded { get; set; }
}