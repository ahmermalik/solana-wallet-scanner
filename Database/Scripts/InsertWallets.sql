-- Declare the NetworkId for Solana
DECLARE @NetworkId INT = 9;

-- Insert the new wallets with Name set to NULL
INSERT INTO Wallets (NetworkId, Address, TotalUsdValue, LastUpdated, Name)
VALUES
    (@NetworkId, '71CPXu3TvH3iUKaY1bNkAAow24k6tjH473SsKprQBABC', 0, GETDATE(), NULL),
 