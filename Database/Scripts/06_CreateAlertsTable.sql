CREATE TABLE Alerts (
    AlertId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TokenId INT NULL,
    WalletId INT NULL,
    NetworkId INT NULL,
    AlertType VARCHAR(50),
    Message VARCHAR(1000),
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME2,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId),
    FOREIGN KEY (WalletId) REFERENCES Wallets(WalletId),
    FOREIGN KEY (NetworkId) REFERENCES Networks(NetworkId)
);

CREATE INDEX IDX_Alerts_TokenId_AlertType ON Alerts(TokenId, AlertType);
