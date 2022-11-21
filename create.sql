DROP SCHEMA IF EXISTS business CASCADE;
DROP DATABASE IF EXISTS mirea;

CREATE DATABASE mirea;
CREATE SCHEMA business;

CREATE TYPE business.contact_enum AS ENUM ('email', 'телефон', 'адрес');
CREATE TYPE business.priority_enum AS ENUM ('низкий', 'средний', 'высокий');

CREATE TABLE IF NOT EXISTS business.user_login
(
    id SERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(20) NOT NULL UNIQUE,
    password VARCHAR(256) NOT NULL
);

CREATE TABLE IF NOT EXISTS business.person
(
    id SERIAL PRIMARY KEY NOT NULL UNIQUE,
    user_id INTEGER REFERENCES business.user_login(id) ON DELETE CASCADE NOT NULL UNIQUE,
    first_name VARCHAR(128) NOT NULL,
    last_name VARCHAR(128) NOT NULL
);

CREATE TABLE IF NOT EXISTS business.contact_info
(
    id SERIAL PRIMARY KEY NOT NULL UNIQUE,
    person_id INTEGER REFERENCES business.person(id) ON DELETE CASCADE NOT NULL,
    contact VARCHAR(128) NOT NULL UNIQUE,
    contact_type business.contact_enum NOT NULL
);

CREATE TABLE IF NOT EXISTS business.roles
(
    id SERIAL PRIMARY KEY NOT NULL UNIQUE,
    title VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS business.person_role
(
    id SERIAL PRIMARY KEY NOT NULL UNIQUE,
    person_id INTEGER REFERENCES business.person(id) ON DELETE CASCADE NOT NULL UNIQUE,
    role_id INTEGER REFERENCES business.roles(id) ON DELETE RESTRICT NOT NULL
);

CREATE TABLE IF NOT EXISTS business.organization
(
    id SERIAL PRIMARY KEY NOT NULL UNIQUE,
    name VARCHAR(128) NOT NULL UNIQUE,
    person_id INTEGER REFERENCES business.person(id) ON DELETE CASCADE NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS business.task_type
(
    id SERIAL PRIMARY KEY NOT NULL UNIQUE,
    title VARCHAR(128) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS business.contracts
(
    id SERIAL PRIMARY KEY NOT NULL UNIQUE,
    extra_data json NOT NULL
);

CREATE TABLE IF NOT EXISTS business.tasks
(
    id SERIAL PRIMARY KEY NOT NULL UNIQUE,
    contact_id INTEGER REFERENCES business.person(id) ON DELETE CASCADE NOT NULL,
    author_id INTEGER REFERENCES business.person(id) ON DELETE NO ACTION NOT NULL,
    executor_id INTEGER REFERENCES business.person(id) ON DELETE NO ACTION NOT NULL,
    contract_id INTEGER REFERENCES business.contracts(id) ON DELETE CASCADE NULL,
    task_type_id INTEGER REFERENCES business.task_type(id) ON DELETE NO ACTION NULL,
    priority business.priority_enum NOT NULL,
    data VARCHAR(256) NULL,
    dt_created TIMESTAMP NOT NULL,
    dt_finished TIMESTAMP NULL,
    dt_deadline TIMESTAMP NULL
);

CREATE FUNCTION get_login_id_by_login(name VARCHAR)
    RETURNS INTEGER
    LANGUAGE plpgsql AS
    $func$
    DECLARE ret INTEGER;
    BEGIN
       SELECT id INTO ret FROM business.user_login WHERE username = name;
       RETURN ret;
    END
    $func$;

CREATE FUNCTION get_person_id_by_login(name VARCHAR)
    RETURNS INTEGER
    LANGUAGE plpgsql AS
    $func$
    DECLARE ret INTEGER;
    BEGIN
       SELECT P.id INTO ret FROM business.user_login ul
       INNER JOIN business.person p ON ul.id = p.user_id
       WHERE ul.username = name;
       RETURN ret;
    END
    $func$;

CREATE FUNCTION get_role_id(name VARCHAR)
    RETURNS INTEGER
    LANGUAGE plpgsql AS
    $func$
    DECLARE ret INTEGER;
    BEGIN
       SELECT id INTO ret FROM business.roles WHERE title = name;
       RETURN ret;
    END
    $func$;

CREATE FUNCTION get_task_type_id(name VARCHAR)
    RETURNS INTEGER
    LANGUAGE plpgsql AS
    $func$
    DECLARE ret INTEGER;
    BEGIN
       SELECT id INTO ret FROM business.task_type WHERE title = name;
       RETURN ret;
    END
    $func$;
