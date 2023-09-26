SELECT info FROM account_moviment;

SELECT 
   SUM (CAST (info ->> 'operationValue' AS NUMERIC))
FROM account
WHERE info -> 'customer' ->> 'name' = 'Fabio';
--WHERE info -> 'moviments' ->> 'number' = '1234';

SELECT json_each (info->'moviments')
FROM account_moviment;