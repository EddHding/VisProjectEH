------|-- NAME ----------|-- AMOUNT --|-- CREDIT_ACCOUNT --|-- DEBIT_ACCOUNT --|-- DATE --|
--1-- | Cash Withdrawal  |    100     |        Bank        |       Cash        |   ----   |
--2-- | Cash Deposit     |    250     |        Cash        |       Bank        |   ----   |
-- Use if statement
--
SELECT AbstractTransactions.Name, AbstractTransactions.Amount, Accounts.AccountName, Accounts.AccountName, AbstractTransactions.Date
FROM AbstractTransactions, Accounts
WHERE (AbstractTransactions.CreditAccountId = 8  AND Accounts.Id = AbstractTransactions.DebitAccountId) OR (AbstractTransactions.DebitAccountId = 8  AND Accounts.Id = AbstractTransactions.CreditAccountId);