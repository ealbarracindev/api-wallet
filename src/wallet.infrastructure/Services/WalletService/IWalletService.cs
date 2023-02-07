using wallet.infrastructure.Helpers.CurrencyHelper;

namespace wallet.infrastructure.Services.WalletService;

public interface IWalletService
{
    Task Add(int userId, string currency);
    Task TopUp(int userId, string currency, decimal amount);
    Task Withdraw(int userId, string currency, decimal amount);
    Task Transfer(int userId, string fromCurrency, string toCurrency, decimal amount, ICurrencyHelper currencyHelper);    
}
