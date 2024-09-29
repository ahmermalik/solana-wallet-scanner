-- Set NetworkId for Solana to 9
DECLARE @NetworkId INT = 9;

-- Insert wallets into the Wallets table for the Solana network
INSERT INTO Wallets (NetworkId, Address, TotalUsdValue, LastUpdated)
VALUES
    (@NetworkId, 'J2Q2j6kpSg7tq8JzueCHNTQNcyNnQkvr85RhsFnYZWeG', 0, GETDATE()),
    (@NetworkId, 'FuH81XGWYKFSXSa1FhRw7rF42JK26aEBorJnHTuKo1h6', 0, GETDATE()),
    (@NetworkId, 'Cici3VkBztsxuZucqBKuiT8pfUtbdNTH3sLSVfTyi7rM', 0, GETDATE()),
    (@NetworkId, '4xw4TpgPuuZbSPjTuxamvudBWhwwNfkUjmnHgUhzPq7f', 0, GETDATE()),
    (@NetworkId, 'Hyy8LgMZgKwmVgdAXqnMJMaUfBhcWYxRPncD11bXiSXR', 0, GETDATE()),
    (@NetworkId, 'EUgrgd6gjZtyqpPfnMZMVnFfpN4GWqRMaie4a3cW2fbK', 0, GETDATE()),
    (@NetworkId, 'AdvvtebaRMqqmfhLJwYsmisgyNEK8BnCUkt6gNXAHk7U', 0, GETDATE()),
    (@NetworkId, '5gUuDFHswKi2QMA1qJHf6FEVhNCrHnyAdfWniMaUUPE4', 0, GETDATE()),
    (@NetworkId, 'AnppRemLRErjaq6cMRhDLNPssZvzQzDdWhkPFdT4Z55e', 0, GETDATE());
