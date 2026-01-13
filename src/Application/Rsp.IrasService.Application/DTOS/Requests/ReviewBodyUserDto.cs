namespace Rsp.Service.Application.DTOS.Requests;

public class ReviewBodyUserDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public DateTime DateAdded { get; set; }
}