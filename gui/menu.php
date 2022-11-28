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
    $sql = 'SELECT * FROM contact_info';
    $ps = $pdo->prepare($sql, [PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY]);
    $ps->execute();
    $result = $ps->fetchAll();
    echo '<table class="data">';
    echo '<tr><th>id</th><th>person_id</th><th>contact</th><th>type</th></tr>';
    foreach ($result as $row)
    {
        echo '<tr>';
        echo '<td>'.$row['id'].'</td>';
        echo '<td>'.$row['person_id'].'</td>';
        echo '<td>'.$row['contact'].'</td>';
        echo '<td>'.$row['contact_type'].'</td>';
        echo '</tr>';
    }
    echo '</table>';
    ?>
</div>
</body>
</html>
