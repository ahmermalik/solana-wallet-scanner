SELECT TOP 100
    t.TokenId,
    t.Symbol AS TokenSymbol,
    t.Name AS TokenName,
    COUNT(DISTINCT wh.WalletId) AS NumberOfWallets
FROM 
    Tokens t
INNER JOIN 
    WalletHoldings wh ON t.TokenId = wh.TokenId
GROUP BY 
    t.TokenId, t.Symbol, t.Name
ORDER BY 
    NumberOfWallets DESC;
