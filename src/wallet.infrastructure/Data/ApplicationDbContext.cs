using Microsoft.EntityFrameworkCore;
using wallet.core.Entities;

namespace wallet.infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Wallet> Wallets { get; set; }
    
}
