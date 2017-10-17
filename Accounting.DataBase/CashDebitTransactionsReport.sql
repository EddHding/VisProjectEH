-- Gets all debit transactions involving cash,
-- and gives the name of the transaction the accounts effected,
-- the date and the amount.
SELECT [Transaction Record].Name, [Transaction Record].AMOUNT, [Account Record].[ACCOUNT NAME], [Transaction Record].[DATE OF TRANSACTION]
FROM [Transaction Record], [Account Record]
WHERE [Transaction Record].DebitAccountId = 8  AND [Account Record].Id = [Transaction Record].CreditAccountId;