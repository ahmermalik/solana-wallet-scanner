CREATE TABLE Wallets (
    WalletId INT IDENTITY(1,1) PRIMARY KEY,
    NetworkId INT NOT NULL,
    Address VARCHAR(255) NOT NULL,
    Name VARCHAR(255), 
    TotalUsdValue DECIMAL(38, 18),
    LastUpdated DATETIME2,
    FOREIGN KEY (NetworkId) REFERENCES Networks(NetworkId)
);

-- Unique index to ensure wallets are unique per network
CREATE UNIQUE INDEX IDX_Wallets_NetworkId_Address ON Wallets(NetworkId, Address);
