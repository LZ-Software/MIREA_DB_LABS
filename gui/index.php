<!DOCTYPE html>
<html lang="ru">
<head>
    <title>Авторизация</title>
    <meta charset="utf-8"/>
    <link rel="stylesheet" type="text/css" href="style.css" media="screen"/>
</head>
<body>
    <div class="form">
        <form action="index.php" method="post">
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
                echo "<p class='element' id='success'>Подключен к БД</p>";
            }
            catch (PDOException $e)
            {
                echo "<p class='element' id='error'>Не подключен к БД</p>";
            }
            ?>
            <?php
            if (isset($_POST["name"]) and isset($_POST["password"]))
            {
                $login = $_POST['name'];
                $pass = hash('sha256', $_POST['password']);
                $sql = 'SELECT username, password FROM user_login';
                $ps = $pdo->prepare($sql, [PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY]);
                $ps->execute();
                $result = $ps->fetchAll();
                foreach ($result as $row)
                {
                    $u = $row['username'];
                    $p = str_replace('\x', '', $row['password']);
                    if ($login == $u and $pass == $p)
                    {
                        echo "<p class='element' id='success'>Авторизован</p>";
                        header("Location: menu.php");
                        break;
                    }
                    else
                    {
                        echo "<p class='element' id='error'>Ошибка</p>";
                    }
                }
            }
            ?>
            <input class="element" id="data" placeholder="Логин" type="text" name="name">
            <input class="element" id="data" placeholder="Пароль" type="password" name="password">
            <input class="element" type="submit" value="Войти">
        </form>
    </div>
</body>
</html>
