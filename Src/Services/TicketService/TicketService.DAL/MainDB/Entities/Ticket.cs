using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketService.DAL.MainDB.Entities;

/// <summary>
/// biletler tablosu
/// </summary>
[Table("admin")]
public partial class Ticket
{
    [Key]
    [Column("uid")]
    public Guid Uid { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("price")]
    public decimal? Price { get; set; }

    [Column("is_sold")]
    public bool? IsSold { get; set; }
}
