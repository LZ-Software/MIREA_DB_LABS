INSERT INTO roles(title)
    VALUES
        ('Администратор'),
        ('Менеджер'),
        ('Сотрудник'),
        ('Клиент');

INSERT INTO user_login(username, password)
    VALUES
        ('admin', sha256('mj4ir9^*&$gdhfb87&#(*b')),
        ('manager1', sha256('f6$*86%^&*$fgyu^*&^*%!')),
        ('manager2', sha256('tv^%C%^B&^8t*6rB^&Dd8D8D')),
        ('worker1', sha256('g67F%V&&(*B^&V*&)DV$$S$')),
        ('worker2', sha256('V^%BF%^#C%^DB#&D^*#&FD(@5d')),
        ('client1', sha256('H^&VG%^D$%VTV67v56C%DD$#X')),
        ('client2', sha256('^&FD&^F#DF^*IGD*MBD*G^S&F^')),
        ('client3', sha256('nyBY&G^&V67f^%^FE^&F#6digf6')),
        ('client4', sha256('67v56DC%^*#^&f56f5C84g7v8b'));

INSERT INTO person(user_id, first_name, last_name)
    VALUES
        (get_login_id_by_login('admin'), 'Билли', 'Херингтон'),
        (get_login_id_by_login('manager1'), 'Тодд', 'Говард'),
        (get_login_id_by_login('manager2'), 'Жак', 'Фреско'),
        (get_login_id_by_login('worker1'), 'Илья', 'Луговой'),
        (get_login_id_by_login('worker2'), 'Никита', 'Зарин'),
        (get_login_id_by_login('client1'), 'Дмитрий', 'Пучков'),
        (get_login_id_by_login('client2'), 'Максим', 'Кац'),
        (get_login_id_by_login('client3'), 'Рулон', 'Обоев'),
        (get_login_id_by_login('client4'), 'Ушат', 'Помоев');

INSERT INTO person_role(person_id, role_id)
    VALUES
        (get_person_id_by_login('admin'), get_role_id('Администратор')),
        (get_person_id_by_login('manager1'), get_role_id('Менеджер')),
        (get_person_id_by_login('manager2'), get_role_id('Менеджер')),
        (get_person_id_by_login('worker1'), get_role_id('Сотрудник')),
        (get_person_id_by_login('worker2'), get_role_id('Сотрудник')),
        (get_person_id_by_login('client1'), get_role_id('Клиент')),
        (get_person_id_by_login('client2'), get_role_id('Клиент')),
        (get_person_id_by_login('client3'), get_role_id('Клиент')),
        (get_person_id_by_login('client4'), get_role_id('Клиент'));

INSERT INTO contact_info(person_id, contact, contact_type)
    VALUES
        (get_person_id_by_login('admin'), '+77777777777', 'телефон'),
        (get_person_id_by_login('admin'), 'master@dungeon.com', 'email'),
        (get_person_id_by_login('admin'), 'Москва, Темный переулок, дом 69', 'адрес'),
        (get_person_id_by_login('manager1'), 'info@bethsoft.com', 'email'),
        (get_person_id_by_login('manager1'), '237207, Тульская область, город Подольск, наб. Ломоносова, 07', 'адрес'),
        (get_person_id_by_login('manager2'), '478611, Смоленская область, город Луховицы, бульвар Балканская, 98', 'адрес'),
        (get_person_id_by_login('worker1'), '+79152979221', 'телефон'),
        (get_person_id_by_login('worker1'), 'ilya.l2013@yandex.ru', 'email'),
        (get_person_id_by_login('worker2'), '+79153682126', 'телефон'),
        (get_person_id_by_login('worker2'), 'nikita0zrz@gmail.com', 'email'),
        (get_person_id_by_login('worker2'), 'Барвиха, дом 6', 'адрес'),
        (get_person_id_by_login('client1'), '+79097146321', 'email'),
        (get_person_id_by_login('client1'), '+79187122692', 'телефон'),
        (get_person_id_by_login('client1'), '870701, Пензенская область, город Дорохово, бульвар Сталина, 12', 'адрес'),
        (get_person_id_by_login('client2'), 'rosemary64@gmail.com', 'email'),
        (get_person_id_by_login('client2'), '+79036362845', 'телефон'),
        (get_person_id_by_login('client2'), '038442, Белгородская область, город Талдом, проезд Косиора, 42', 'адрес'),
        (get_person_id_by_login('client3'), 'kyra.conn@gmail.com', 'email'),
        (get_person_id_by_login('client3'), '+79163963611', 'телефон'),
        (get_person_id_by_login('client3'), '016809, Смоленская область, город Домодедово, пр. Домодедовская, 10', 'адрес'),
        (get_person_id_by_login('client4'), '247021, Калининградская область, город Орехово-Зуево, въезд Будапештсткая, 77', 'адрес'),
        (get_person_id_by_login('client4'), '+79008683644', 'телефон');

INSERT INTO organization(name, person_id)
    VALUES
        ('Dungeon Inc.', get_person_id_by_login('client1')),
        ('Umbrella Corp.', get_person_id_by_login('client2')),
        ('Gym Inc.', get_person_id_by_login('client3')),
        ('LZ Software', get_person_id_by_login('client4'));

INSERT INTO task_type(title)
    VALUES
        ('Отправка оборудования'),
        ('Ремонт оборудования'),
        ('Установка оборудования'),
        ('Звонок'),
        ('Диагностика'),
        ('Встреча с клиентом');

INSERT INTO contracts(extra_data)
    VALUES
        ('{"Серийный номер": "ZM9RM646873865783", "Производитель": "Toshiba", "Окончание гарантии": "01-01-2023"}'),
        ('{"Серийный номер": "VT6ED019075942003", "Производитель": "Bosch", "Точка назначения": "Метро Румянцево, KFC"}'),
        ('{"Серийный номер": ["UN7LG794060753856", "ML6EV666885361170"], "Производитель": "Apple", "Кол-во": 2}'),
        ('{"Серийный номер": "VL1EX420482601375", "Производитель": "Samsung", "Окончание гарантии": "01-05-2022"}'),
        ('{"Серийный номер": "XS5LE011324719550", "Производитель": "Huawei"}');

INSERT INTO tasks(contact_id, author_id, executor_id, contract_id, task_type_id, priority, data, dt_created, dt_finished, dt_deadline)
    VALUES
        (get_person_id_by_login('client1'),
         get_person_id_by_login('manager1'),
         get_person_id_by_login('worker1'),
         1,
         get_task_type_id('Ремонт оборудования'),
         'средний',
         NULL,
         now()::timestamp,
         NULL,
         now()::timestamp + INTERVAL '7 days'),
        (get_person_id_by_login('client2'),
         get_person_id_by_login('manager2'),
         get_person_id_by_login('manager2'),
         5,
         get_task_type_id('Диагностика'),
         'низкий',
         'Не заряжается устройство',
         now()::timestamp,
         NULL,
         now()::timestamp + INTERVAL '1 days'),
        (get_person_id_by_login('client3'),
         get_person_id_by_login('manager1'),
         get_person_id_by_login('worker2'),
         2,
         get_task_type_id('Отправка оборудования'),
         'высокий',
         NULL,
         now()::timestamp,
         NULL,
         now()::timestamp + INTERVAL '2 hours'),
        (get_person_id_by_login('client3'),
         get_person_id_by_login('manager2'),
         get_person_id_by_login('worker2'),
         3,
         get_task_type_id('Установка оборудования'),
         'низкий',
         'Установить обновление ПО',
         now()::timestamp,
         NULL,
         now()::timestamp + INTERVAL '1 day'),
        (get_person_id_by_login('client4'),
         get_person_id_by_login('manager1'),
         get_person_id_by_login('worker1'),
         NULL,
         get_task_type_id('Встреча с клиентом'),
         'средний',
         'Обсудить поставку микрочипов',
         now()::timestamp,
         NULL,
         now()::timestamp + INTERVAL '30 minutes');
