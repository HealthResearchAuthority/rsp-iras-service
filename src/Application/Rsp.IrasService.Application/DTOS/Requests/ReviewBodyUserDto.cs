namespace Rsp.IrasService.Application.DTOS.Requests;

public class ReviewBodyUserDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateAdded { get; set; }
}