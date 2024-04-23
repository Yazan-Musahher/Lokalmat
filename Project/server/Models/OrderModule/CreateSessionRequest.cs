namespace server.Models.OrderModule;

public class CreateSessionRequest
{
    public List<Guid> ProductIds { get; set; }
    public string UserId { get; set; }
}