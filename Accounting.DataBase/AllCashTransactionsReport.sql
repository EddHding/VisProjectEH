------|-- NAME ----------|-- AMOUNT --|-- CREDIT_ACCOUNT --|-- DEBIT_ACCOUNT --|-- DATE --|
--1-- | Cash Withdrawal  |    100     |        Bank        |       Cash        |   ----   |
--2-- | Cash Deposit     |    250     |        Cash        |       Bank        |   ----   |
-- Use if statement
--
SELECT [Transaction Record].Name, [Transaction Record].[AMOUNT], [Account Record].[ACCOUNT NAME], [Account Record].[ACCOUNT NAME], [Transaction Record].[DATE OF TRANSACTION]
FROM [Transaction Record], [Account Record]
WHERE ([Transaction Record].CreditAccountId = 8  AND [Account Record].Id = [Transaction Record].DebitAccountId) OR ([Transaction Record].DebitAccountId = 8  AND [Account Record].Id = [Transaction Record].CreditAccountId);