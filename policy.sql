DROP ROLE IF EXISTS manager;
DROP ROLE IF EXISTS worker;
DROP ROLE IF EXISTS administrator;

CREATE ROLE manager;
CREATE ROLE worker;
CREATE ROLE administrator WITH LOGIN ENCRYPTED PASSWORD '69dungeon_master69';

GRANT ALL ON ALL TABLES IN SCHEMA business TO administrator;
GRANT ALL ON ALL FUNCTIONS IN SCHEMA business TO administrator;
GRANT ALL ON ALL PROCEDURES IN SCHEMA business TO administrator;

GRANT SELECT ON business.contracts, business.tasks, business.contact_info, business.task_type, business.person,
    business.organization TO manager, worker;

GRANT INSERT ON business.tasks, business.contact_info, business.contracts, business.person, business.organization
    TO manager;

GRANT UPDATE ON business.tasks, business.contact_info, business.contracts, business.person, business.organization
    TO manager;

GRANT UPDATE(dt_finished, priority) ON business.tasks TO worker;

ALTER TABLE business.tasks ENABLE ROW LEVEL SECURITY ;

CREATE POLICY select_tasks ON business.tasks FOR SELECT TO manager, worker
USING (
    (SELECT username FROM business.user_login WHERE user_login.id = tasks.author_id) = current_user OR
    (SELECT username FROM business.user_login WHERE user_login.id = tasks.executor_id) = current_user
    );

CREATE POLICY select_for_managers ON business.tasks FOR SELECT TO manager
USING (
    (SELECT title FROM business.roles WHERE roles.id = (SELECT role_id FROM business.person_role
    WHERE person_id = tasks.author_id) = 'Сотрудник')
    );

CREATE POLICY access_all ON business.tasks FOR ALL TO administrator USING (true) WITH CHECK (true);

CREATE POLICY insert_task ON business.tasks FOR INSERT TO manager WITH CHECK (true);

CREATE POLICY update_task ON business.tasks FOR UPDATE TO manager USING (true) WITH CHECK(tasks.dt_finished IS NULL
    AND ((SELECT username FROM business.user_login WHERE user_login.id = author_id) = current_user));

CREATE POLICY update_state ON business.tasks FOR UPDATE TO manager, worker
USING (dt_finished IS NULL) WITH CHECK((dt_finished IS NOT NULL)
    AND ((SELECT username FROM business.user_login WHERE user_login.id = author_id) = current_user
    OR (SELECT username FROM business.user_login WHERE user_login.id = executor_id) = current_user)
    );