CREATE PROCEDURE sp_InsertWhaleActivity
    @Token NVARCHAR(50),
    @WalletAddress NVARCHAR(100),
    @Amount DECIMAL(18, 8),
    @ActivityType NVARCHAR(20),
    @Timestamp DATETIME
AS
BEGIN
    INSERT INTO WhaleActivities (Token, WalletAddress, Amount, ActivityType, Timestamp)
    VALUES (@Token, @WalletAddress, @Amount, @ActivityType, @Timestamp);
END
