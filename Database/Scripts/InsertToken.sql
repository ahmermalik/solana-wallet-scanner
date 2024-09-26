-- InsertMultipleTokens.sql

INSERT INTO Tokens (
    Address,
    Network,
    Symbol,
    Name,
    Decimals,
    Liquidity,
    MarketCap,
    Price,
    Volume24hUSD,
    PriceChangePercent24h,
    LogoURI,
    LastUpdated
)
VALUES
    ('TQzUXP5eXHwrRMou9KYQQq7wEmu8KQF7mX', 'Tron', 'XLG', 'Xiao Lang', 6, NULL, NULL, NULL, NULL, NULL, 'https://example.com/logo1.png', NULL),
    ('3B5wuUrMEi5yATD7on46hKfej3pfmd7t1RKgrsN3pump', 'Solana', 'Billy', 'Billy Dog', 18, NULL, NULL, NULL, NULL, NULL, 'https://example.com/logo9.png', NULL);
