using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaymentService.Application.DTO.DataObjects;

public partial class TicketDO
{
    public TicketDO()
    {
        Uid = Guid.NewGuid();
    }

    public Guid Uid { get; set; }

    public string? Title { get; set; }

    public decimal? Price { get; set; }

    public bool? IsSold { get; set; }



    ///////////////////////////// EXTERNAL /////////////////////////////


    public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions
    {
        IgnoreReadOnlyFields = true,
        IgnoreReadOnlyProperties = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });
}
