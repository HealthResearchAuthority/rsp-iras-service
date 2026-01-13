namespace Rsp.Service.Application.DTOS.Requests;

public class ReviewBodySearchRequest
{
    public string? SearchQuery { get; set; }
    public List<string> Country { get; set; } = [];
    public bool? Status { get; set; }
}