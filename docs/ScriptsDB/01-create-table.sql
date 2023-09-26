CREATE TABLE bank_account (
	id text NOT NULL PRIMARY KEY,
	account json NOT NULL
);

CREATE TABLE bank_account_moviment (
	id text NOT NULL PRIMARY KEY,
	account_moviment json NOT NULL
);
