CREATE TABLE Transactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    TxHash VARCHAR(255) UNIQUE NOT NULL,
    FromWalletId INT NULL,
    ToWalletId INT NULL,
    TokenId INT NOT NULL,
    NetworkId INT NOT NULL,
    BlockNumber BIGINT,
    BlockTime DATETIME2,
    Status BIT,
    FromAddress VARCHAR(255) NOT NULL,
    ToAddress VARCHAR(255) NOT NULL,
    Amount DECIMAL(38, 18),
    ValueUsd DECIMAL(38, 18),
    TransactionType VARCHAR(50),
    Fee DECIMAL(38, 18),
    FOREIGN KEY (FromWalletId) REFERENCES Wallets(WalletId),
    FOREIGN KEY (ToWalletId) REFERENCES Wallets(WalletId),
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId),
    FOREIGN KEY (NetworkId) REFERENCES Networks(NetworkId)
);

-- Indexes for performance optimization
CREATE INDEX IDX_Transactions_FromWalletId ON Transactions(FromWalletId);
CREATE INDEX IDX_Transactions_ToWalletId ON Transactions(ToWalletId);
CREATE INDEX IDX_Transactions_TokenId ON Transactions(TokenId);
CREATE INDEX IDX_Transactions_BlockTime ON Transactions(BlockTime);
CREATE INDEX IDX_Transactions_NetworkId ON Transactions(NetworkId);
