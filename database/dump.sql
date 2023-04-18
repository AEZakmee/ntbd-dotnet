CREATE DATABASE mydatabase;

\c mydatabase;

CREATE TABLE dataentities (
    in serial PRIMARY KEY,
    name varchar(255) NOT NULL
);

INSERT INTO dataentities (name) VALUES ('John');
INSERT INTO dataentities (name) VALUES ('Jane');
INSERT INTO dataentities (name) VALUES ('Bob');
