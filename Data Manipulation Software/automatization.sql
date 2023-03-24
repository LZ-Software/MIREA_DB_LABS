CREATE FUNCTION check_task_completed()
    RETURNS TRIGGER
    AS
    $update_task_trigger$
    BEGIN
        IF (OLD.dt_finished IS NOT NULL) THEN
        RAISE EXCEPTION 'Задание уже выполнено';
        END IF;
        RETURN NEW;
    END;
    $update_task_trigger$ LANGUAGE plpgsql;
CREATE TRIGGER update_task_trigger
    BEFORE UPDATE ON tasks
    FOR EACH ROW
        EXECUTE PROCEDURE check_task_completed();

CREATE EXTENSION pg_cron;
SELECT cron.schedule('0 0 * * *',
    $$DELETE FROM tasks WHERE dt_finished < (now() - interval '12 month'$$);
