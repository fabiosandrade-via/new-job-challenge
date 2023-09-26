SELECT account FROM bank_account_moviment;

SELECT 
   SUM (CAST (account ->> 'operationValue' AS NUMERIC))
FROM bank_account
WHERE account -> 'customer' ->> 'name' = 'Fabio';
--WHERE account -> 'moviments' ->> 'number' = '1234';

SELECT json_each (account->'moviments')
FROM account_moviment;