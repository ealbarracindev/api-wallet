using Microsoft.EntityFrameworkCore;
using wallet.core.Entities;
using wallet.infrastructure.Data;

namespace wallet.infrastructure.Services.UserService;

internal class UserService : IUserService
{
    private readonly ApplicationDbContext _db;

    public UserService(ApplicationDbContext applicationDbContext)
    {
        this._db = applicationDbContext;
    }
    public async Task Add(string name)
    {
        await _db.Users.AddAsync(new User() { Name = name });
    }

    public async Task<User> Get(int userId)
    {
        return await _db.Users.Include(x => x.Wallets)
                              .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<Dictionary<string, decimal>> GetBalance(int userId)
    {
        var user = await Get(userId);
        if (user is null)
            return null;

        var walletBalance = user.Wallets.ToDictionary(k => k.Currency, v => Math.Round(v.Amount, 2));
        return walletBalance;
    }
}
