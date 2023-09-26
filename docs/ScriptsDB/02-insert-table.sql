INSERT INTO bank_account (id, account)
VALUES('1', '{ "customer": { "id": 1, "name": "Fabio" }, "number": "1234","transactionType": 1, "operationValue": 100 }');

INSERT INTO bank_account (id, account)
VALUES('2', '{ "customer": { "id": 2, "name": "Ligia" }, "number": "9874","transactionType": 1, "operationValue": 500 }');

INSERT INTO bank_account_moviment (id, account_moviment)
VALUES('1', '{ "customer": { "id": 1, "name": "Fabio" }, "number": "1234","transactionType": 1, "operationValue": 100, "operationDate": "24-08-2023 09:30:00" }'),
      ('2', '{ "customer": { "id": 1, "name": "Fabio" }, "number": "1234","transactionType": 1, "operationValue": 300, "operationDate": "25-08-2023 11:42:00" }'),
      ('3', '{ "customer": { "id": 2, "name": "Ligia" }, "number": "9874","transactionType": 1, "operationValue": 500, "operationDate": "25-08-2023 11:44:00" }');  