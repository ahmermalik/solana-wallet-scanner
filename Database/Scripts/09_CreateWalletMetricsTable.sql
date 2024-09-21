CREATE TABLE WalletMetrics (
    WalletId INT PRIMARY KEY,
    Profitability DECIMAL(18, 8),
    WinLossRatio DECIMAL(18, 8),
    AverageHoldTime DECIMAL(18, 8),
    CostBasis DECIMAL(38, 18),
    TradeFrequency INT,
    TradeSizeAverage DECIMAL(38, 18),
    LastUpdated DATETIME2,
    FOREIGN KEY (WalletId) REFERENCES Wallets(WalletId)
);
