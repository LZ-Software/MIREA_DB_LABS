DROP ROLE IF EXISTS manager;
DROP ROLE IF EXISTS worker;
DROP ROLE IF EXISTS administrator;

CREATE ROLE manager;
CREATE ROLE worker;
CREATE ROLE administrator WITH LOGIN ENCRYPTED PASSWORD 'dungeon_master69';

GRANT USAGE ON SCHEMA public to administrator;
GRANT ALL ON ALL TABLES IN SCHEMA public TO administrator;
GRANT ALL ON ALL FUNCTIONS IN SCHEMA public TO administrator;
GRANT ALL ON ALL PROCEDURES IN SCHEMA public TO administrator;
GRANT ALL ON ALL SEQUENCES IN SCHEMA public to administrator;

ALTER TABLE tasks ENABLE ROW LEVEL SECURITY;

GRANT SELECT ON contracts, tasks, contact_info, task_type, person,
    organization, user_login, person_role, roles TO manager, worker;

GRANT ALL ON All Sequences In Schema public TO manager, worker;

GRANT INSERT ON tasks, contact_info, contracts, person, organization
    TO manager;

GRANT UPDATE ON tasks, contact_info, contracts, person, organization
    TO manager;

GRANT DELETE ON contracts to manager, worker;

GRANT UPDATE(dt_finished, priority) ON tasks TO worker;

CREATE POLICY select_tasks ON tasks FOR SELECT TO manager, worker
USING
(
    (SELECT username FROM user_login WHERE user_login.id = tasks.author_id) = current_user
        OR
    (SELECT username FROM user_login WHERE user_login.id = tasks.executor_id) = current_user
);

CREATE POLICY select_for_managers ON tasks FOR SELECT TO manager
USING
(
    (SELECT title FROM roles WHERE roles.id = (SELECT role_id FROM person_role WHERE person_id = tasks.author_id)) = 'Сотрудник'
);

CREATE POLICY access_all ON tasks FOR ALL TO administrator
USING (true) WITH CHECK (true);

CREATE POLICY insert_task ON tasks FOR INSERT TO manager
WITH CHECK (true);

CREATE POLICY update_task ON tasks FOR UPDATE TO manager
USING (true) WITH CHECK(tasks.dt_finished IS NULL AND ((SELECT username FROM user_login WHERE user_login.id = author_id) = current_user));

CREATE POLICY update_state ON tasks FOR UPDATE TO manager, worker
USING (dt_finished IS NULL) WITH CHECK((dt_finished IS NOT NULL)
    AND ((SELECT username FROM user_login WHERE user_login.id = author_id) = current_user
    OR (SELECT username FROM user_login WHERE user_login.id = executor_id) = current_user)
    );