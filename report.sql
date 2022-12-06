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
    COPY (SELECT to_jsonb(json_agg(json_tasks)) FROM (SELECT * FROM tasks) as json_tasks) TO 'C:/Users/Public/report.json';
END;
$$ LANGUAGE plpgsql;


create function make_report(path TEXT) RETURNS void
AS $$ DECLARE
statement TEXT;
BEGIN
statement := format('copy(select json_agg(task) ::TEXT from
(select task. task_id, task.creation_date, task. deadline_date,
task_status.status_name, task.description, employee. full_name from task join task_status on task. status_id = task_status.status_id join employee on employee employee_id = task.executor_id) as task) TO **8s/tasks.json'';', path);
EXECUTE statement;
RETURN;
END; $$
LANGUAGE plpgsql;


SELECT tsk.id, c.username, a.username, e.username, cont.extra_data, t.title, tsk.priority, tsk.data, tsk.dt_created, tsk.dt_finished, tsk.dt_deadline FROM tasks tsk
    JOIN user_login c ON tsk.contact_id = c.id
    JOIN user_login a ON tsk.author_id = a.id
    JOIN user_login e ON tsk.executor_id = e.id
    JOIN contracts cont ON tsk.contract_id = cont.id
    JOIN task_type t ON tsk.task_type_id = t.id;


CALL generate_json_report();