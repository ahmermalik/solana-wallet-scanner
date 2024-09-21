CREATE TABLE TokenMetrics (
    TokenId INT PRIMARY KEY,
    TopPerformingWallets VARCHAR(MAX), -- Can be JSON or reference to another table
    CorrelationData VARCHAR(MAX), -- Can be JSON or reference to another table
    LastUpdated DATETIME2,
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId)
);
