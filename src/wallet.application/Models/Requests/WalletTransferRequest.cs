namespace wallet.application.Models.Requests;

public class WalletTransferRequest:BaseWalletRequest
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
}

