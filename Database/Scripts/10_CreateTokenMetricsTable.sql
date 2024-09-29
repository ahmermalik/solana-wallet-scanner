CREATE TABLE TokenMetrics (
    TokenId INT PRIMARY KEY,
    NetworkId INT NOT NULL,
    TopPerformingWallets VARCHAR(MAX), -- Can be JSON or reference to another table
    CorrelationData VARCHAR(MAX),      -- Can be JSON or reference to another table
    LastUpdated DATETIME2,
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId),
    FOREIGN KEY (NetworkId) REFERENCES Networks(NetworkId)
);
