import pymongo

client = pymongo.MongoClient("mongodb://localhost:27017/")
db = client["mirea"]


def test_games_collection():
    games = db["games"]

    assert games is not None
    assert "name" in games.find_one()
    assert "price" in games.find_one()
    assert "publisher" in games.find_one()
    assert "categories" in games.find_one()


def test_categories_collection():
    categories = db["categories"]

    assert categories is not None
    assert "name" in categories.find_one()


def test_clients_collection():
    clients = db["clients"]

    assert clients is not None
    assert "nickname" in clients.find_one()
    assert "email" in clients.find_one()


def test_workers_collection():
    workers = db["workers"]

    assert workers is not None
    assert "name" in workers.find_one()
    assert "email" in workers.find_one()
    assert "phone" in workers.find_one()
    assert "position" in workers.find_one()


def test_orders_collection():
    orders = db["orders"]

    assert orders is not None
    assert "client" in orders.find_one()
    assert "games" in orders.find_one()
    assert "date" in orders.find_one()
    assert "status" in orders.find_one()
