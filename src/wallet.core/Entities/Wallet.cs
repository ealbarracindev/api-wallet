using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wallet.core.Entities;

[Table("Wallets")]
public class Wallet
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Currency { get; set; }

    [Required]
    public decimal Amount { get; set; }
}