CREATE TABLE DumpEvents (
    DumpEventId INT IDENTITY(1,1) PRIMARY KEY,
    TokenId INT NOT NULL,
    VolumeSold DECIMAL(38, 18),
    PriceDropPercent DECIMAL(18, 8),
    DetectedAt DATETIME2,
    Details VARCHAR(1000),
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId)
);
