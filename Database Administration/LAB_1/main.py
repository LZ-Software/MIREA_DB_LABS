from pymongo import MongoClient
from datetime import datetime


class Client:
    def __init__(self):
        self.client = MongoClient('localhost', 27017)
        self.db = self.client['mirea']

    # Все коллекции
    def show_collections(self):
        return self.db.list_collection_names()

    # Все категории
    def show_categories(self):
        categories_collection = self.db['categories']
        return [category['name'] for category in categories_collection.find()]

    # Поиск игры по названию
    def find_games_by_name(self, name):
        games_collection = self.db['games']
        cursor = games_collection.find({'name': {'$regex': name, '$options': 'i'}})
        games = [game['name'] for game in cursor]
        return games

    # Поиск игр по категории
    def find_games_by_category(self, category):
        category_collection = self.db['categories']
        game_collection = self.db['games']
        category_games = game_collection.find({'categories.category': category_collection.find_one({'name': category})["_id"]})
        games = [game['name'] for game in category_games]
        return games

    # Поиск игр по издателю
    def find_games_by_publisher(self, publisher):
        games_collection = self.db['games']
        cursor = games_collection.find({'publisher': {'$regex': publisher, '$options': 'i'}})
        games = [game['name'] for game in cursor]
        return games

    # Поиск игр в заданном ценовом диапазоне
    def find_games_by_price(self, min_price, max_price):
        games_collection = self.db['games']
        cursor = games_collection.find({'price': {'$gte': min_price, '$lte': max_price}})
        games = [game['name'] for game in cursor]
        return games

    # Добавить игры в корзину
    def add_games_to_order(self, client_email, *args):
        _id = self.db.orders.insert_one(
            {
                "client": self.db.clients.find_one({"email": client_email})["_id"],
                "games": [
                    {
                        "game": self.db.games.find_one({"name": game})["_id"]
                    }
                    for game in args
                ],
                "date": datetime.now(),
                "status": False,
            })
        return _id.inserted_id

    # Общая стоимость корзины
    def calculate_cart_total(self, *args):
        total = 0
        games_collection = self.db['games']
        for name in args:
            game = games_collection.find_one({'name': name})
            total += game['price']
        return total


client = Client()
# print(client.show_collections())
# print(client.show_categories())
# print(client.find_games_by_name('Call Of Duty'))
# print(client.find_games_by_category('Гонки'))
# print(client.find_games_by_publisher('Capcom'))
# print(client.find_games_by_price(1999, 4999))
# print(client.add_games_to_order('pivolublu@mail.ru', 'Rocket League', 'Call Of Duty Black Ops Cold War'))
print(client.calculate_cart_total('Resident Evil Village', 'Battlefield 5'))
