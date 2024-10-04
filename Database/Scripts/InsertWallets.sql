-- Set NetworkId for Solana to 9
DECLARE @NetworkId INT = 9;

-- Insert wallets into the Wallets table for the Solana network
INSERT INTO Wallets (NetworkId, Address, TotalUsdValue, LastUpdated)
VALUES
(@NetworkId, '8CGJPWdM7xZX5E2ZE7qw5TiNUgpLyUeXJAnHr5goezeD', 0, GETDATE()),
(@NetworkId, 'GpCaBXxX5jjxnWBC2cdevDMeTPE3RVGzRMXQuvfjvCKv', 0, GETDATE()),
(@NetworkId, '8icKqfmu95pCmfQLc2cLkmhB7LqN8tRaaE4cvKnGpwpq', 0, GETDATE()),
(@NetworkId, '4EhZSMz5DMadWLzNmv8Mo2Du6yx67xbyRXEQjnNitywt', 0, GETDATE()),
(@NetworkId, '5zsbHMxdgLUPMFPHTwCykxbbmQ6R7dd8T9vhzWKfuTdm', 0, GETDATE()),
(@NetworkId, '8TSfBNY3NGBu2PnWs5Wu4yDT5wgVFGjx1Pdga6fjkYqo', 0, GETDATE()),
(@NetworkId, 'G2GagoL5PsyvqpqqNduARTSyFsp12TJhKWwaddrj5ra7', 0, GETDATE()),
(@NetworkId, '4Su8XsbBcWshtPnrdMbTznxSBtU5F7Fx5bQFFqYhmTpx', 0, GETDATE()),
(@NetworkId, '6gqHVs2uWPQCDxUGHCpG9JjrDHx62sWfWYe1WuDRdyDo', 0, GETDATE()),
(@NetworkId, 'fv6VdGMYtG2jtCgC5n4xttsD8pC3GhVVJtq8SWmqCRN', 0, GETDATE()),
(@NetworkId, '2qEkxEjGnuNVZwxzHjUYbWmQs3uiMQFiATy2SASPFCdJ', 0, GETDATE()),
(@NetworkId, '87Hnwjwp28WPBLrc1JcuxWxQJenknT2zdN3atbNgtN7t', 0, GETDATE()),
(@NetworkId, '8sx3y726uYjJYwpp6SF2HqSPcf1nfFtfjpaKo9SJ5Mxb', 0, GETDATE()),
(@NetworkId, 'B9KiiJoiJoJGG66DArz3kTYwCpJWiiS9egHsrpZLK5LR', 0, GETDATE()),
(@NetworkId, '6BE7yFZyvQRyg2vveW5GHPgvgorb6f8BWVfJir7zRVXH', 0, GETDATE()),
(@NetworkId, '8fxFVTteo51AxG9yacH1hHZbEqmGk8avyvdHLsRHHV3g', 0, GETDATE()),
(@NetworkId, '8SpKtvMXbtzvnKWWoLjzwWqGeJabJJz25MD3Se4HATw7', 0, GETDATE()),
(@NetworkId, 'FPyhXxNYqYq6ybX13EaeLMEwGPpKsorsPr9ozPKFfPBm', 0, GETDATE()),
(@NetworkId, '6JQxPMqoXnRvqoWLui5kFVTQdMJwHHRxY2VW34E6d9nZ', 0, GETDATE()),
(@NetworkId, '3cfEDYtXi5VFtGxVVcjPfEuZtXcrq8scGP7SipSQdeqn', 0, GETDATE()),
(@NetworkId, '5k7oe6TaouyXPKuVvi9jx1whfrNMzYcy5MwL2H6PfBx8', 0, GETDATE()),
(@NetworkId, 'D96cTc6JoJ4Q6kEkJZZsA7rfXHzj3DqyK3yqGnvYrPC8', 0, GETDATE()),
(@NetworkId, '79sJvLQ3QrL88Uc9jgV3iTT5Ft19xtMbjmAkhgoKh38W', 0, GETDATE()),
(@NetworkId, 'RXuT7AGbPdvohJtuzkt21LLBRuKTtiX17DaGXEUrukU', 0, GETDATE()),
(@NetworkId, '8JpHsGn6Y1MXBtJc9oTheoXAufRRp2LoimPLq1CSRAHZ', 0, GETDATE()),
(@NetworkId, '7VoM8eNQU6tJkfW6qwGPKX51VKQvjfiAxo4DR8EEh3yA', 0, GETDATE()),
(@NetworkId, '9Tqv2JcsY8SNrZbQLdNcjSviNDDvdMcTDbnPCL6MKyQ7', 0, GETDATE()),
(@NetworkId, 'Gks2BGVLPMkrZFDqEHkEGa6kjCFTCvpUucqzsVY7cxkb', 0, GETDATE()),
(@NetworkId, 'GrTu9D6nYt6GDwaQR9U2WQmXLK8uugWn5FJyyUm6k95X', 0, GETDATE()),
(@NetworkId, 'DFJumFMVGfPBXjyNK19Viibp3e3yedyzArS2LwLLbwtn', 0, GETDATE()),
(@NetworkId, 'FLVANj7PfBpj14V8FEoQG6Z9Y2WXrgX7zMgUGPvK6pn6', 0, GETDATE());

