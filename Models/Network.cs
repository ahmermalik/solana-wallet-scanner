using System;
using System.Collections.Generic;

namespace WalletScanner.Models
{
    public class Network
{
    public int NetworkId { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string RPC_Endpoint { get; set; }
    public string Explorer_URL { get; set; }

    public ICollection<Wallet> Wallets { get; set; }
    public ICollection<Token> Tokens { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<WhaleActivity> WhaleActivities { get; set; }
    public ICollection<Alert> Alerts { get; set; }
    public ICollection<DumpEvent> DumpEvents { get; set; }
    public ICollection<TokenMetric> TokenMetrics { get; set; }
    public ICollection<TrendingToken> TrendingTokens { get; set; }
    public ICollection<TopTrader> TopTraders { get; set; }
}
}