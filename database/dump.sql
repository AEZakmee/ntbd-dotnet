CREATE DATABASE mydatabase;

\c mydatabase;

CREATE TABLE dataentities (
    id serial PRIMARY KEY,
    name varchar(255) NOT NULL
);

INSERT INTO dataentities (name) VALUES ('John');
INSERT INTO dataentities (name) VALUES ('Jane');
INSERT INTO dataentities (name) VALUES ('Bob');

CREATE TABLE queries (
    id serial PRIMARY KEY,
    query TEXT NOT NULL,
    executionTime INTEGER NOT NULL
);