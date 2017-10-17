-- Gets all credit transactions involving cash,
-- and gives the name of the transaction the accounts effected,
-- the date and the amount.
SELECT AbstractTransactions.Name, AbstractTransactions.Amount, Accounts.AccountName, AbstractTransactions.Date
FROM AbstractTransactions, Accounts
WHERE AbstractTransactions.CreditAccountId = 8  AND Accounts.Id = AbstractTransactions.DebitAccountId;