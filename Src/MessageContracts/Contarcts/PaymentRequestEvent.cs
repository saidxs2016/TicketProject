namespace Contracts;

public record PaymentRequestEvent
{
    public Guid Uid { get; set; }

    public string? Title { get; set; }

    public decimal Price { get; set; }

    public bool IsSold { get; set; }

    public string? Message { get; set; }

}
