namespace Contracts;

public record PaymentSuccessEvent
{
    public Guid Uid { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }
}
