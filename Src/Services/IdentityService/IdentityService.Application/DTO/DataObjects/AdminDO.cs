using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IdentityService.Application.DTO.DataObjects;

public partial class AdminDO
{
    public AdminDO()
    {
        Uid = Guid.NewGuid();
    }
    public Guid Uid { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Username { get; set; } = "admin";
    public string? Password { get; set; } = "123";

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiration { get; set; }

    public string? Token { get; set; }



    ///////////////////////////// EXTERNAL /////////////////////////////


    public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions
    {
        IgnoreReadOnlyFields = true,
        IgnoreReadOnlyProperties = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });
}
