INSERT INTO account (info)
VALUES('{ "customer": { "id": 1, "name": "Fabio" }, "number": "1234","transactionType": 1, "operationValue": 100 }');

INSERT INTO account (info)
VALUES('{ "customer": { "id": 2, "name": "Ligia" }, "number": "9874","transactionType": 1, "operationValue": 500 }');

INSERT INTO account_moviment (info)
VALUES('{ "customer": { "id": 1, "name": "Fabio" }, "number": "1234","transactionType": 1, "operationValue": 100, "operationDate": "24-08-2023 09:30:00" }'),
      ('{ "customer": { "id": 1, "name": "Fabio" }, "number": "1234","transactionType": 1, "operationValue": 300, "operationDate": "25-08-2023 11:42:00" }'),
      ('{ "customer": { "id": 2, "name": "Ligia" }, "number": "9874","transactionType": 1, "operationValue": 500, "operationDate": "25-08-2023 11:44:00" }');         