CREATE PROCEDURE sp_InsertWhaleActivity
    @TokenId INT,
    @WalletId INT,
    @ActivityType VARCHAR(50),
    @Amount DECIMAL(38, 18),
    @ValueUsd DECIMAL(38, 18),
    @Timestamp DATETIME2
AS
BEGIN
    INSERT INTO WhaleActivities (TokenId, WalletId, ActivityType, Amount, ValueUsd, Timestamp)
    VALUES (@TokenId, @WalletId, @ActivityType, @Amount, @ValueUsd, @Timestamp)
END
