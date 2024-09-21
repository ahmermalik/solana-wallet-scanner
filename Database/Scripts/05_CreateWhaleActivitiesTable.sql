CREATE TABLE WhaleActivities (
    WhaleActivityId INT IDENTITY(1,1) PRIMARY KEY,
    TokenId INT NOT NULL,
    WalletId INT NOT NULL,
    ActivityType VARCHAR(50),
    Amount DECIMAL(38, 18),
    ValueUsd DECIMAL(38, 18),
    Timestamp DATETIME2,
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId),
    FOREIGN KEY (WalletId) REFERENCES Wallets(WalletId)
);

CREATE INDEX IDX_WhaleActivities_TokenId_Timestamp ON WhaleActivities(TokenId, Timestamp);
