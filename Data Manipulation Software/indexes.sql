CREATE INDEX person_name_index ON person(first_name, last_name);
CREATE INDEX person_login_index ON user_login(username);
CREATE INDEX person_contact_index ON contact_info(contact);

CREATE INDEX organization_name_index ON organization(name);
CREATE INDEX organization_delegate_index ON organization(person_id);