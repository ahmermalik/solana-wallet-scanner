CREATE TABLE DumpEvents (
    DumpEventId INT IDENTITY(1,1) PRIMARY KEY,
    TokenId INT NOT NULL,
    NetworkId INT NOT NULL,
    VolumeSold DECIMAL(38, 18),
    PriceDropPercent DECIMAL(18, 8),
    DetectedAt DATETIME2,
    Details VARCHAR(1000),
    FOREIGN KEY (TokenId) REFERENCES Tokens(TokenId),
    FOREIGN KEY (NetworkId) REFERENCES Networks(NetworkId)
);

CREATE INDEX IDX_DumpEvents_TokenId_DetectedAt ON DumpEvents(TokenId, DetectedAt);
