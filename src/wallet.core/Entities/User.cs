using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wallet.core.Entities;

[Table("Users")]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
    public int Id { get; set; }

    public string Name { get; set; }

    [ForeignKey("UserId")]
    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}