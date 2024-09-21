CREATE PROCEDURE sp_DetectDumpEvents
AS
BEGIN
    -- Logic to detect dump events
    SELECT 
        t.TokenId,
        SUM(CASE WHEN ta.ActivityType = 'Sell' THEN ta.Amount ELSE 0 END) AS VolumeSold,
        ((MAX(t.Price) - MIN(t.Price)) / MAX(t.Price)) * 100 AS PriceDropPercent,
        GETDATE() AS DetectedAt,
        'Detected significant dumping activity' AS Details
    FROM WhaleActivities ta
    JOIN Tokens t ON ta.TokenId = t.TokenId
    WHERE ta.Timestamp >= DATEADD(HOUR, -1, GETDATE()) -- Last hour
    GROUP BY t.TokenId
    HAVING SUM(CASE WHEN ta.ActivityType = 'Sell' THEN ta.Amount ELSE 0 END) > @Threshold -- Define @Threshold
END
