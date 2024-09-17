-- sp_DetectDumpEvents: Identifies wallets that have sold a significant amount of a specific token within a defined timeframe.
CREATE PROCEDURE sp_DetectDumpEvents
    @Token NVARCHAR(50),
    @ThresholdAmount DECIMAL(18, 8),
    @StartTime DATETIME,
    @EndTime DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.WalletAddress,
        SUM(t.Amount) AS TotalSold,
        COUNT(t.Id) AS SellCount,
        MAX(t.Timestamp) AS LastSellTime
    FROM 
        Transactions t
    WHERE 
        t.Token = @Token
        AND t.TransactionType = 'sell'
        AND t.Timestamp BETWEEN @StartTime AND @EndTime
    GROUP BY 
        t.WalletAddress
    HAVING 
        SUM(t.Amount) >= @ThresholdAmount
END
