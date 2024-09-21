CREATE TABLE WalletHoldings (
    WalletId INT NOT NULL,
    TokenId INT NOT NULL,
    Balance DECIMAL(38, 18),
    UiAmount DECIMAL(38, 18),
    ValueUsd DECIMAL(38, 18),
    PriceUsd DECIMAL(38, 18),
    PRIMARY KEY (WalletId, TokenId),
    FOREIGN KEY (WalletId) REFERENCES Wallets(WalletId),
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId)
);
