namespace Web.API.Models;

public record AccountRequestModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
