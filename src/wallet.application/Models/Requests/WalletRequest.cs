using System;

namespace wallet.application.Models.Requests;

public class WalletRequest: BaseWalletRequest
{
    public string Currency { get; set; }
}

