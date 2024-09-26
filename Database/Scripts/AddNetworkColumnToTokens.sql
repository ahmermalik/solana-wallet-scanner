-- AddNetworkColumnToTokens.sql

ALTER TABLE Tokens
ADD Network VARCHAR(50) NOT NULL DEFAULT 'Unknown';
