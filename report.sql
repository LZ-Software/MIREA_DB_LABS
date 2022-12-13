CREATE OR REPLACE FUNCTION create_report(worker INTEGER, start_date DATE, end_date DATE)
RETURNS TABLE (total_count INTEGER, finished_in_time INTEGER, finished_late INTEGER, not_finished INTEGER, not_finished_overdue INTEGER)
AS $body$
DECLARE
    total_count INTEGER;
    finished_in_time INTEGER;
    finished_late INTEGER;
    not_finished INTEGER;
    not_finished_overdue INTEGER;
BEGIN
    start_date = start_date::TIMESTAMP;
    end_date = (end_date + INTERVAL '1 day' - INTERVAL '1 microsecond')::TIMESTAMP;
    total_count := (SELECT count(*) FROM tasks -- Всего задач созданных в данный период
                                    WHERE (executor_id = worker)
                                      AND (dt_created BETWEEN start_date AND end_date));
    finished_in_time := (SELECT count(*) FROM tasks -- Задач, завершенных вовремя
                                         WHERE (executor_id = worker)
                                           AND (dt_created BETWEEN start_date AND end_date)
                                           AND (dt_finished BETWEEN start_date AND end_date)
                                           AND (dt_deadline >= dt_finished));
    finished_late := (SELECT count(*) FROM tasks -- Задач, завершенных с опозданием
                                         WHERE (executor_id = worker)
                                           AND (dt_created BETWEEN start_date AND end_date)
                                           AND (dt_finished BETWEEN start_date AND end_date)
                                           AND (dt_deadline <= dt_finished));
    not_finished := (SELECT count(*) FROM tasks -- Задач, еще не завершенных
                                         WHERE (executor_id = worker)
                                           AND (dt_created BETWEEN start_date AND end_date)
                                           AND (dt_finished IS NULL)
                                           AND (dt_deadline >= end_date));
    not_finished_overdue := (SELECT count(*) FROM tasks -- Задач, еще не завершенных, уже с опозданием
                                         WHERE (executor_id = worker)
                                           AND (dt_created BETWEEN start_date AND end_date)
                                           AND (dt_finished IS NULL)
                                           AND (dt_deadline <= end_date));
    RETURN QUERY SELECT total_count, finished_in_time, finished_late, not_finished, not_finished_overdue;
END $body$
LANGUAGE plpgsql;

SELECT * FROM create_report(get_person_id_by_login('worker1'), (current_date - INTERVAL '23 days')::DATE, (current_date + INTERVAL '2 days')::DATE);

SELECT id, dt_created, dt_finished, dt_deadline FROM tasks WHERE executor_id = get_person_id_by_login('worker1');

\copy (SELECT * FROM create_report(get_person_id_by_login('worker1'), (current_date - INTERVAL '23 days')::DATE, (current_date)::DATE)) TO '/home/plmr0/kek.csv' DELIMITER ',' CSV HEADER

CREATE OR REPLACE PROCEDURE generate_json_report()
AS $$ BEGIN
    EXECUTE format('');
    COPY (SELECT to_jsonb(json_agg(json_tasks)) FROM (SELECT * FROM tasks) as json_tasks) TO 'C:/Users/Public/report.json';
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE PROCEDURE generate_report(path TEXT)
AS $$ DECLARE
statement TEXT;
BEGIN
statement := format('copy(select json_agg(task) ::TEXT from (SELECT tsk.id, contact.username, author.username, executor.username, cont.extra_data, t.title, tsk.priority, tsk.data, tsk.dt_created, tsk.dt_finished, tsk.dt_deadline FROM tasks tsk
    JOIN user_login contact ON tsk.contact_id = contact.id
    JOIN user_login author ON tsk.author_id = author.id
    JOIN user_login executor ON tsk.executor_id = executor.id
    LEFT JOIN contracts cont ON tsk.contract_id = cont.id
    JOIN task_type t ON tsk.task_type_id = t.id) as task) TO %L', path);
EXECUTE statement;
RETURN;
END; $$
LANGUAGE plpgsql;

CALL generate_report('C:/Users/Public/report.json');


CREATE OR REPLACE FUNCTION find_client(word_1 VARCHAR(50))
RETURNS TABLE (
client_id INTEGER,
client_username VARCHAR(20),
client_name VARCHAR(128),
client_surname VARCHAR(128),
client_phone VARCHAR(128),
client_email VARCHAR(128),
client_address VARCHAR(128),
client_org VARCHAR(128))
AS $body$
BEGIN
    RETURN QUERY
    SELECT p.id as ID, ul.username,p.first_name, p.last_name, cft.contact as Phone, cfe.contact as Email, cfa.contact as Address, org.name as Organization FROM person p
    JOIN user_login ul ON p.user_id = ul.id
    JOIN person_role pr ON p.id = pr.person_id AND pr.role_id = 4
    LEFT JOIN contact_info cft ON p.id = cft.person_id AND cft.contact_type = 'телефон'
    LEFT JOIN contact_info cfe ON p.id = cfe.person_id AND cfe.contact_type = 'email'
    LEFT JOIN contact_info cfa ON p.id = cfa.person_id AND cfa.contact_type = 'адрес'
    JOIN organization org on p.id = org.person_id
    WHERE ul.username = word_1
       OR p.first_name = word_1
       OR p.last_name = word_1
       OR cft.contact = word_1
       OR cfe.contact = word_1
       OR cfa.contact = word_1;

END;
$body$ LANGUAGE plpgsql;

SELECT * FROM find_client('kyra.conn@gmail.com');

DROP FUNCTION find_client(word_1 VARCHAR(50));
