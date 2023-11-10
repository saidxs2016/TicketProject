namespace Web.API.Models;

public record RequestModel
{
    //public int Id { get; set; }
    public Guid? Uid { get; set; }
    public string? Key { get; set; }
}
