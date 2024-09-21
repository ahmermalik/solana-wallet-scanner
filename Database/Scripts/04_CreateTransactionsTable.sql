CREATE TABLE Transactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    TxHash VARCHAR(255) UNIQUE NOT NULL,
    WalletId INT NOT NULL,
    TokenId INT NOT NULL,
    BlockNumber BIGINT,
    BlockTime DATETIME2,
    Status BIT,
    FromAddress VARCHAR(255),
    ToAddress VARCHAR(255),
    Amount DECIMAL(38, 18),
    ValueUsd DECIMAL(38, 18),
    TransactionType VARCHAR(50),
    Fee DECIMAL(38, 18),
    FOREIGN KEY (WalletId) REFERENCES Wallets(WalletId),
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId)
);

CREATE INDEX IDX_Transactions_WalletId ON Transactions(WalletId);
CREATE INDEX IDX_Transactions_TokenId ON Transactions(TokenId);
CREATE INDEX IDX_Transactions_BlockTime ON Transactions(BlockTime);
