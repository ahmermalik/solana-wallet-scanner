CREATE TABLE Tokens (
    TokenId INT IDENTITY(1,1) PRIMARY KEY,
    Address VARCHAR(255) UNIQUE NOT NULL,
    Symbol VARCHAR(50) NOT NULL,
    Name VARCHAR(255),
    Decimals INT,
    Liquidity DECIMAL(38, 18),
    MarketCap DECIMAL(38, 18),
    Price DECIMAL(38, 18),
    Volume24hUSD DECIMAL(38, 18),
    PriceChangePercent24h DECIMAL(18, 8),
    LogoURI VARCHAR(255),
    LastUpdated DATETIME2
);
