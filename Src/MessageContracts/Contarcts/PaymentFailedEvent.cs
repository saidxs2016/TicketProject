namespace Contarcts;

public record PaymentFailedEvent
{
    public Guid Uid { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }
}
