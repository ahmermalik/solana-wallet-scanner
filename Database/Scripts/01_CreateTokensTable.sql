CREATE TABLE Tokens (
    TokenId INT IDENTITY(1,1) PRIMARY KEY,
    NetworkId INT NOT NULL,
    Address VARCHAR(255) NOT NULL,
    Symbol VARCHAR(50) NOT NULL,
    Name VARCHAR(255),
    Decimals INT,
    Liquidity DECIMAL(38, 18),
    MarketCap DECIMAL(38, 18),
    Price DECIMAL(38, 18),
    Volume24hUSD DECIMAL(38, 18),
    PriceChangePercent24h DECIMAL(18, 8),
    LogoURI VARCHAR(255),
    LastUpdated DATETIME2,
    FOREIGN KEY (NetworkId) REFERENCES Networks(NetworkId)
);

-- Unique index to ensure tokens are unique per network
CREATE UNIQUE INDEX IDX_Tokens_NetworkId_Address ON Tokens(NetworkId, Address);
