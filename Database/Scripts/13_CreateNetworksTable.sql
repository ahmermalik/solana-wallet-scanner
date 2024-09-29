CREATE TABLE Networks (
    NetworkId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Symbol VARCHAR(10) NOT NULL, -- e.g., 'ETH', 'SOL', 'TRX'
    RPC_Endpoint VARCHAR(255),    -- Optional, for network interactions
    Explorer_URL VARCHAR(255)     -- Optional, for block explorer links
);