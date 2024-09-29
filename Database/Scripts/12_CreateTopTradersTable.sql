CREATE TABLE TopTraders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TokenId INT NOT NULL,
    WalletId INT NOT NULL,
    NetworkId INT NOT NULL,
    Volume DECIMAL(38, 18),
    TradeCount INT,
    TradeBuyCount INT,
    TradeSellCount INT,
    VolumeBuy DECIMAL(38, 18),
    VolumeSell DECIMAL(38, 18),
    Period VARCHAR(50),
    UpdatedAt DATETIME2,
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId),
    FOREIGN KEY (WalletId) REFERENCES Wallets(WalletId),
    FOREIGN KEY (NetworkId) REFERENCES Networks(NetworkId)
);

CREATE INDEX IDX_TopTraders_TokenId ON TopTraders(TokenId);
CREATE INDEX IDX_TopTraders_WalletId ON TopTraders(WalletId);
