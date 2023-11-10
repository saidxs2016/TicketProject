using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.DAL.MainDB.Entities;

/// <summary>
/// adminler tablosu
/// </summary>
[Table("admin")]
public partial class Admin
{
    [Key]
    [Column("uid")]
    public Guid Uid { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("surname")]
    public string? Surname { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("username")]
    public string? Username { get; set; }

    [Column("password")]
    public string? Password { get; set; }
}
