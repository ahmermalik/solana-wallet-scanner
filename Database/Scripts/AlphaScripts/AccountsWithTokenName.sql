-- query to find a token with the ticker symbol 'lol' and return the accounts that hold it along with the amount

SELECT
    w.WalletId,
    w.Address AS WalletAddress,
    COALESCE(wh.Balance, 0) AS Balance,
    COALESCE(wh.ValueUsd, 0) AS ValueUsd,
    n.Name AS NetworkName
FROM
    Tokens t
INNER JOIN
    WalletHoldings wh ON t.TokenId = wh.TokenId
INNER JOIN
    Wallets w ON wh.WalletId = w.WalletId
INNER JOIN
    Networks n ON t.NetworkId = n.NetworkId
WHERE
    t.Symbol = 'lol'
    AND t.NetworkId = w.NetworkId;
