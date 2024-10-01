-- Set NetworkId for Solana to 9
DECLARE @NetworkId INT = 9;

-- Insert wallets into the Wallets table for the Solana network
INSERT INTO Wallets (NetworkId, Address, TotalUsdValue, LastUpdated)
VALUES
(@NetworkId, '3K3xQCT7pQ4W3Eecg7o9jSKbi4kv3rHxuRPCAMNYrY43', 0, GETDATE()),
(@NetworkId, '9yspZZWZCa2Hqg5Um5LBjTmSuiicAX72PpP91a6vF1ts', 0, GETDATE()),
(@NetworkId, 'CJM26EeHNqY5YX1FzDgVJfAcxC4vyyqrs59R4Gi1zaeM', 0, GETDATE()),
(@NetworkId, 'BrHTHHtsLKSarAVYLMuPRXibVqG6cWSUzdF7gcva68ta', 0, GETDATE()),
(@NetworkId, '3bg4uGH5YsG6bq2kiX8CqWy6h1NVyQiV5D6DJhMRswmu', 0, GETDATE()),
(@NetworkId, '7LY8ytiqe1Fo7tTNeayccogsfbr1zemZgxSkQcoeBspg', 0, GETDATE()),
(@NetworkId, '5FxZsAPc4RYRbY3wv6aFAvPiTK7L7XkYMpA6FSMMELwr', 0, GETDATE()),
(@NetworkId, '3GuWyYQqhiB5y6vRchcHt3c2xPazGJS8YLjNjo9cGWUc', 0, GETDATE()),
(@NetworkId, '8bN1KikSUyhNEXfJ5D6D5PDhGYo3UwyfFf8GaMJPHdt9', 0, GETDATE()),
(@NetworkId, '6WeZnDirHDQvFQdERJK5YspKDnnBZzGgDyeFDPcvMLY3', 0, GETDATE()),
(@NetworkId, 'Hnx4CzgA5KmNBVPc3rWNzkZMt2i4kt8fEgZZuL92EUX5', 0, GETDATE()),
(@NetworkId, 'BYLvusKdcgE8usrzSFFGLBQ6AvxHhjeWW1o8t14VbUNC', 0, GETDATE()),
(@NetworkId, 'GAUSb485G65iinPYpYWMCNPFbr29GamrFknm5Dj3eiXM', 0, GETDATE()),
(@NetworkId, '5iZuNFp4S1ZX2n8af9MhAprp2YfNQCoNsHasVdqkfTu9', 0, GETDATE()),
(@NetworkId, 'CDFjReWxtBnyQCuB5xu5RW1WMeYUxZEBWFvhwWegPcpm', 0, GETDATE()),
(@NetworkId, '6Xbq5SQYvdm7XcmJ2HPeF9Wzr1ewTQjXWXvaWNJNaNtE', 0, GETDATE()),
(@NetworkId, 'DHhwhGYzx5pXDt3Wf8UFZnENgq6xk2mTQDB36tCFNQ5q', 0, GETDATE()),
(@NetworkId, '3S4zcjqeutpdU1Ld8JpjRWHxJLxhGpPySHvTPKdurWAP', 0, GETDATE()),
(@NetworkId, '4iGUMSs9PrKkfF4HFhMxEVdPomxNedyXwforsDcNV3iE', 0, GETDATE()),
(@NetworkId, 'J1Nmib12QCSmRXrLtR85AH5UqVFeiCZu5rgRibhMhy27', 0, GETDATE()),
(@NetworkId, 'DkirezkSQLYND5ppjNMad8tyoEvZEYa5j4pwUaAEPyc7', 0, GETDATE()),
(@NetworkId, 'DwnNHu4c43tmGwbuE9ZSwKweQ4iffaUjhQWkfraRN5Cf', 0, GETDATE()),
(@NetworkId, '4ZuhrBZhC2wAyfEXpmuXDo6vv87y19DWKaB6JkgRDk11', 0, GETDATE()),
(@NetworkId, 'F84EkcdqBY8EdniBXCo6bAKnw3JKn9f6mHxyzzsDof6i', 0, GETDATE()),
(@NetworkId, 'B22HLnK77ZjbyUHtybYDUX1R65LDhw6scuz9edGEFcs7', 0, GETDATE()),
(@NetworkId, '3fRijoS7yv2TKh9QCaJDTzpYt8vZamZFVHqscoj8u9U7', 0, GETDATE()),
(@NetworkId, '71iSb7KMXt2UteNbjZKq2dpuAchkWM1qemmwyBgnLrHA', 0, GETDATE()),
(@NetworkId, '4ZneA676s44S6rsEGqzKReRUbYBgNBpzJNT2zUvXTBvQ', 0, GETDATE()),
(@NetworkId, '6By3BS1AKGoaCDQFwZU7hjHDAsVfuyfqEYPB6Td7e4WC', 0, GETDATE()),
(@NetworkId, '6ZtbZaaBEeF8BH3c915fCRZdKZmnNiJV9Nr93iDkFYZb', 0, GETDATE()),
(@NetworkId, 'FnMyyS31iswe1FTHXmthYvgwAxuACKmQNbKUTXDKe3J2', 0, GETDATE()),
(@NetworkId, '446bgbRDhFVWMYDqxekpZTJxR7nqBdZefe3JAk5ESWtY', 0, GETDATE()),
(@NetworkId, '24S42MJcfZHzF8oh7UUGbvwsbWCe8qmYqc6uPF6oqneY', 0, GETDATE()),
(@NetworkId, '6iJAuvybeRq1PBk3eXM8whZsrnkH988H4evfkpEkvF7e', 0, GETDATE()),
(@NetworkId, '7xxY78nX7Ppg7DokhebdTpxp7j7JeSbcKsnLotuSPfeo', 0, GETDATE()),
(@NetworkId, '4ZAuzeGBx9c7o6vkcP7SRVao6tP7dQTXHnhCd9KnodAK', 0, GETDATE());

