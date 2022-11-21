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

GRANT UPDATE(dt_finished) ON business.tasks TO worker;

ALTER TABLE business.tasks ENABLE ROW LEVEL SECURITY ;

CREATE POLICY select_tasks ON business.tasks FOR SELECT TO manager, worker
USING (
    (SELECT username FROM business.user_login WHERE (user_login.id = author_id) = current_user OR
    (SELECT username FROM business.user_login WHERE (user_login.id = executor_id) = current_user))
    );

CREATE POLICY select_for_managers ON business.tasks FOR SELECT TO manager
USING (
    ()
    );