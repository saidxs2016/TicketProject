namespace Web.API.Models;

public record TicketRequestModel
{
    //public int Id { get; set; }
    public Guid? Uid { get; set; }
    public decimal Price { get; set; }

    public string? Token { get; set; }
}
