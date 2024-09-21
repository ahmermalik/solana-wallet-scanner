CREATE TABLE TrendingTokens (
    TokenId INT PRIMARY KEY,
    Rank INT,
    Volume24hUSD DECIMAL(38, 18),
    Price DECIMAL(38, 18),
    UpdatedAt DATETIME2,
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId)
);
