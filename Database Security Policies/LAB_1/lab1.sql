-- Задание 1
SELECT DISTINCT ad.model->>'en' AS model FROM flights f
JOIN airports_data ap ON ap.airport_code = f.arrival_airport
JOIN aircrafts_data ad ON f.aircraft_code = ad.aircraft_code
WHERE ap.airport_code = 'UFA';

-- Задание 2
WITH flights AS (
    SELECT COUNT(bp) AS passengers FROM flights f
    JOIN boarding_passes bp ON f.flight_id = bp.flight_id
    WHERE f.departure_airport IN (SELECT airport_code FROM airports_data WHERE city->>'ru' = 'Санкт-Петербург') AND
          f.arrival_airport IN (SELECT airport_code FROM airports_data WHERE city->>'ru' = 'Москва')
    GROUP BY f.flight_no, f.flight_id)
SELECT ROUND(AVG(passengers)) FROM flights;

-- Задание 3
SELECT ad.model->>'en' AS aircraft FROM seats s
JOIN aircrafts_data ad ON s.aircraft_code = ad.aircraft_code
GROUP BY aircraft
ORDER BY COUNT(s.seat_no) DESC LIMIT 1;

-- Задание 4
SELECT f.* FROM flights f
JOIN (SELECT aircraft_code, COUNT(seat_no) AS total FROM seats GROUP BY aircraft_code) AS a_seats ON a_seats.aircraft_code = f.aircraft_code
JOIN (SELECT flight_id, COUNT(ticket_no) AS sold FROM ticket_flights GROUP BY flight_id) AS s_seats ON s_seats.flight_id = f.flight_id
WHERE a_seats.total > s_seats.sold;

-- Задание 5
SELECT t.passenger_name, SUM(b.total_amount) FROM tickets t
JOIN bookings b ON t.book_ref = b.book_ref
GROUP BY t.passenger_name;

-- Задание 6
SELECT t.passenger_name, bp.seat_no FROM boarding_passes bp
JOIN tickets t ON bp.ticket_no = t.ticket_no
WHERE t.passenger_name = (SELECT passenger_name FROM tickets GROUP BY passenger_name ORDER BY COUNT(ticket_no) DESC LIMIT 1);

-- Задание 7
SELECT ad.model->>'en' AS aircarft, a_seats.total AS seats, coalesce(a_flights.total, 0) AS flights FROM aircrafts_data ad
LEFT JOIN (SELECT aircraft_code, COUNT(seat_no) AS total FROM seats GROUP BY aircraft_code) AS a_seats ON a_seats.aircraft_code = ad.aircraft_code
LEFT JOIN (SELECT aircraft_code, COUNT(flight_id) AS total FROM flights GROUP BY aircraft_code) AS a_flights ON a_flights.aircraft_code = ad.aircraft_code
ORDER BY a_seats.total DESC;
