CREATE TABLE Wallets (
    WalletId INT IDENTITY(1,1) PRIMARY KEY,
    Address VARCHAR(255) UNIQUE NOT NULL,
    TotalUsdValue DECIMAL(38, 18),
    LastUpdated DATETIME2
);
