<!DOCTYPE html>
<html lang="ru">
<head>
    <title>Авторизация</title>
    <meta charset="utf-8"/>
    <link rel="stylesheet" type="text/css" href="style.css" media="screen"/>
</head>
<body>
<div class="data_container">
    <?php
    $host = '127.0.0.1';
    $user = 'postgres';
    $password = 'postgres';
    $db = 'mirea';
    $pdo = null;
    try
    {
        $dsn = "pgsql:host=$host;port=5432;dbname=$db;";
        $pdo = new PDO($dsn, $user, $password, [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]);
    }
    catch (PDOException $e)
    {
        echo "<p class='element' id='error'>Не подключен к БД</p>";
    }
    $sql = "
        SELECT 
            tsk.id as Номер,
            contact.username as Заказчик,
            author.username as Автор,
            executor.username as Исполнитель,
            t.title as Тип,
            tsk.priority as Приориетет,
            tsk.dt_created as Создано,
            coalesce(tsk.dt_finished::varchar, 'В работе') as Завершено,
            tsk.dt_deadline as Дедлайн
        FROM tasks tsk
        JOIN user_login contact ON tsk.contact_id = contact.id
        JOIN user_login author ON tsk.author_id = author.id
        JOIN user_login executor ON tsk.executor_id = executor.id
        LEFT JOIN contracts cont ON tsk.contract_id = cont.id
        JOIN task_type t ON tsk.task_type_id = t.id
        ORDER BY Создано DESC";
    $ps = $pdo->prepare($sql, [PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY]);
    $ps->execute();
    $result = $ps->fetchAll();
    echo '<table class="data">';
    echo '<tr>
            <th>Номер</th>
            <th>Заказчик</th>
            <th>Автор</th>
            <th>Исполнитель</th>
            <th>Тип</th>
            <th>Приориетет</th>
            <th>Создано</th>
            <th>Завершено</th>
            <th>Дедлайн</th>
            <th>Действие</th>
          </tr>';
    foreach ($result as $row)
    {
        echo '<tr>';
        echo '<td>'.$row['Номер'].'</td>';
        echo '<td>'.$row['Заказчик'].'</td>';
        echo '<td>'.$row['Автор'].'</td>';
        echo '<td>'.$row['Исполнитель'].'</td>';
        echo '<td>'.$row['Тип'].'</td>';
        echo '<td>'.$row['Приориетет'].'</td>';
        echo '<td>'.$row['Создано'].'</td>';
        echo '<td>'.$row['Завершено'].'</td>';
        echo '<td>'.$row['Дедлайн'].'</td>';
        echo '<td><a href="">Изменить</a></td>';
        echo '</tr>';
    }
    echo '</table>';
    ?>
</div>
</body>
</html>
