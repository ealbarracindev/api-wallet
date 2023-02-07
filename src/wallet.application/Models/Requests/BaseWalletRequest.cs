using System;

namespace wallet.application.Models.Requests;

public class BaseWalletRequest
{
    public int UserId { get; set; }
    public decimal Amount { get; set; }
}

