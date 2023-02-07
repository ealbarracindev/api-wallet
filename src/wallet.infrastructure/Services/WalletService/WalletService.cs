using wallet.core.Constants;
using wallet.core.Entities;
using wallet.infrastructure.Data;
using wallet.infrastructure.Helpers.CurrencyHelper;
using wallet.infrastructure.Services.UserService;

namespace wallet.infrastructure.Services.WalletService;

public class WalletService : IWalletService
{
    private readonly ApplicationDbContext _db;
    private readonly IUserService _userService;

    public WalletService(ApplicationDbContext applicationDbContext, IUserService userService)
    {
        this._db = applicationDbContext;
        this._userService = userService;
    }
    public async Task Add(int userId, string currency)
    {
        var user = await _userService.Get(userId);
        if (user is null || user.Wallets.FirstOrDefault(x => x.Currency == currency) != null) // wallet with such currency already exists
            return;

        await _db.Wallets.AddAsync(new Wallet() { UserId = userId, Currency = currency });
        await _db.SaveChangesAsync();
    }


    public async Task TopUp(int userId, string currency, decimal amount)
    {
        var wallet = await GetWallet(userId, currency);
        if (wallet is null)
            return;

        wallet.Amount += amount;

        await _db.SaveChangesAsync();
    }

    public async Task Withdraw(int userId, string currency, decimal amount)
    {
        var wallet = await GetWallet(userId, currency);
        if (wallet is null)
            return;

        if (amount > wallet.Amount)
            throw new Exception(ExceptionMessages.NotEnoughFunds);

        wallet.Amount -= amount;

        await _db.SaveChangesAsync();
    }

    public async Task Transfer(int userId, string fromCurrency, string toCurrency, decimal amount, ICurrencyHelper currencyHelper)
    {
        var user = await _userService.Get(userId);

        var fromWallet = user?.Wallets.FirstOrDefault(x => x.Currency == fromCurrency);
        if (fromWallet is null)
            return;

        var toWallet = user.Wallets.FirstOrDefault(x => x.Currency == toCurrency);
        if (toWallet is null)
            return;

        if (amount > fromWallet.Amount)
            throw new Exception(ExceptionMessages.NotEnoughFunds);

        fromWallet.Amount -= amount;
        var toCurrencyAmount = currencyHelper.Convert(fromCurrency, toCurrency, amount);
        toWallet.Amount += toCurrencyAmount;

        await _db.SaveChangesAsync();
    }

    private async Task<Wallet> GetWallet(int userId, string currency)
    {
        var user = await _userService.Get(userId);
        var wallet = user?.Wallets.FirstOrDefault(x => x.Currency == currency);
        return wallet;
    }
}
