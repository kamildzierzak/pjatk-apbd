-- Clients
INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel)
VALUES 
('John', 'Doe', 'john.doe@example.com', '123-456-789', '12345678901'),
('Jane', 'Smith', 'jane.smith@example.com', '987-654-321', '98765432109'),
('Michael', 'Johnson', 'michael.johnson@example.com', '555-123-456', '11223344556'),
('Emily', 'Brown', 'emily.brown@example.com', '444-321-789', '12312312312'),
('Chris', 'Evans', 'chris.evans@example.com', '333-222-111', '98798798798'),
('Jessica', 'Williams', 'jessica.williams@example.com', '777-888-999', '45645645645'),
('Robert', 'Jones', 'robert.jones@example.com', '666-555-444', '65465465465'),
('Laura', 'Garcia', 'laura.garcia@example.com', '111-222-333', '32132132132'),
('Daniel', 'Martinez', 'daniel.martinez@example.com', '888-999-000', '99999999999'),
('Sophia', 'Lopez', 'sophia.lopez@example.com', '222-333-444', '11122233344'),
('Thomas', 'Harris', 'thomas.harris@example.com', '555-666-777', '22233344455'),
('Sarah', 'Clark', 'sarah.clark@example.com', '333-444-555', '33344455566'),
('James', 'Lewis', 'james.lewis@example.com', '444-555-666', '44455566677'),
('Olivia', 'Young', 'olivia.young@example.com', '222-444-666', '55566677788'),
('Matthew', 'Walker', 'matthew.walker@example.com', '111-555-999', '66677788899'),
('Anna', 'Kowalska', 'anna.kowalska@example.com', '555-444-333', '12309876543'),
('Piotr', 'Nowak', 'piotr.nowak@example.com', '222-555-888', '09876543212'),
('Marek', 'Wiśniewski', 'marek.wisniewski@example.com', '111-999-888', '56789012345'),
('Katarzyna', 'Jankowska', 'katarzyna.jankowska@example.com', '333-666-444', '34567890123'),
('Monika', 'Zielińska', 'monika.zielinska@example.com', '444-888-555', '23456789012'),
('Tomasz', 'Kaczmarek', 'tomasz.kaczmarek@example.com', '666-333-777', '87654321098'),
('Aleksandra', 'Dąbrowska', 'aleksandra.dabrowska@example.com', '888-222-111', '67890123456'),
('Mateusz', 'Szymański', 'mateusz.szymanski@example.com', '111-333-555', '89012345678'),
('Adam', 'Kowalski', 'adam.kowalski@example.com', '555-111-222', '10000000000'),
('Ewa', 'Nowak', 'ewa.nowak@example.com', '555-222-333', '20000000000'),
('Paweł', 'Wójcik', 'pawel.wojcik@example.com', '555-333-444', '30000000000'),
('Agnieszka', 'Zawisza', 'agnieszka.zawisza@example.com', '555-444-555', '40000000000'),
('Kamil', 'Kozłowski', 'kamil.kozlowski@example.com', '555-555-666', '50000000000'),
('Monika', 'Duda', 'monika.duda@example.com', '555-666-777', '60000000000'),
('Michał', 'Sienkiewicz', 'michal.sienkiewicz@example.com', '555-777-888', '70000000000'),
('Beata', 'Pawlak', 'beata.pawlak@example.com', '555-888-999', '80000000000'),
('Łukasz', 'Piotrowski', 'lukasz.piotrowski@example.com', '555-999-000', '90000000000'),
('Julia', 'Majewska', 'julia.majewska@example.com', '555-000-111', '10101010101');


-- Countries
INSERT INTO Country (Name)
VALUES 
('USA'),
('Poland'),
('France'),
('Germany'),
('Italy'),
('Spain'),
('Japan'),
('Australia'),
('Brazil'),
('Canada'),
('Mexico'),
('China'),
('India'),
('South Africa'),
('Russia'),
('Norway'),
('Sweden'),
('Denmark'),
('Finland'),
('Iceland'),
('Portugal'),
('Greece'),
('Turkey');

-- Trips
INSERT INTO Trip (Name, Description, DateFrom, DateTo, MaxPeople)
VALUES 
('Summer Vacation', 'A relaxing summer trip to the coast', '2024-06-01', '2024-06-14', 50),
('Winter Adventure', 'A thrilling winter holiday in the mountains', '2024-12-10', '2024-12-20', 30),
('Cultural Exploration', 'A cultural tour through historical landmarks', '2025-03-01', '2025-03-07', 20),
('Tropical Getaway', 'A warm escape to tropical beaches', '2025-01-15', '2025-01-30', 25),
('Safari Experience', 'An exciting safari through African wildlife', '2025-06-10', '2025-06-20', 15),
('Historical Europe', 'A journey through Europe’s historic cities', '2024-09-01', '2024-09-15', 40),
('Asian Cuisine Tour', 'Explore the flavors of Asia on this culinary tour', '2024-11-05', '2024-11-15', 35),
('Island Adventure', 'Discover hidden islands and their beauty', '2024-08-01', '2024-08-12', 20),
('Canadian Rockies', 'Experience the breathtaking beauty of the Rockies', '2025-02-10', '2025-02-20', 30),
('Latin American Dance Trip', 'A dance-themed adventure through Latin America', '2025-05-01', '2025-05-15', 50),
('Nordic Adventure', 'Explore the beauty of the Nordic countries', '2024-07-15', '2024-07-25', 25),
('Greek Islands', 'Relax on the beautiful Greek islands', '2024-05-10', '2024-05-20', 30),
('Icelandic Wonders', 'Discover geysers, waterfalls, and glaciers', '2025-02-01', '2025-02-10', 20),
('Scandinavian Capitals', 'Visit the capitals of Scandinavia', '2024-09-01', '2024-09-12', 40),
('Portuguese Retreat', 'Enjoy the sun and culture of Portugal', '2024-10-05', '2024-10-15', 15),
('Turkish Delight', 'Discover the rich culture of Turkey', '2025-03-15', '2025-03-25', 35);


-- Client_Trip
INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate)
VALUES 
(1, 1, '2024-04-01 10:00:00', '2024-04-05 14:00:00'),
(2, 2, '2024-04-15 09:30:00', '2024-04-20 16:00:00'),
(3, 3, '2024-05-01 12:00:00', NULL),
(4, 4, '2024-06-01 14:00:00', '2024-06-05 15:00:00'),
(5, 5, '2024-07-01 10:00:00', '2024-07-03 12:00:00'),
(6, 6, '2024-08-01 09:00:00', '2024-08-04 11:00:00'),
(7, 7, '2024-09-01 08:00:00', '2024-09-03 10:00:00'),
(8, 8, '2024-10-01 15:00:00', '2024-10-05 18:00:00'),
(9, 9, '2024-11-01 12:00:00', '2024-11-06 14:00:00'),
(10, 10, '2024-12-01 11:00:00', '2024-12-07 13:00:00'),
(11, 11, '2024-06-01 14:00:00', '2024-06-05 12:00:00'),
(12, 12, '2024-05-01 11:00:00', '2024-05-03 15:00:00'),
(13, 13, '2025-01-01 09:00:00', '2025-01-02 16:00:00'),
(14, 14, '2024-08-01 10:30:00', '2024-08-04 14:00:00'),
(15, 15, '2024-10-01 13:00:00', '2024-10-05 18:00:00'),
(16, 16, '2025-03-01 08:00:00', '2025-03-10 10:00:00');

-- Country_Trip
INSERT INTO Country_Trip (IdCountry, IdTrip)
VALUES 
(1, 1),  -- USA for Summer Vacation
(2, 2),  -- Poland for Winter Adventure
(3, 3),  -- France for Cultural Exploration
(4, 4),  -- Germany for Tropical Getaway
(5, 5),  -- Italy for Safari Experience
(6, 6),  -- Spain for Historical Europe
(7, 7),  -- Japan for Asian Cuisine Tour
(8, 8),  -- Australia for Island Adventure
(9, 9),  -- Brazil for Canadian Rockies
(10, 10), -- Canada for Latin American Dance Trip
(11, 11),  -- Norway for Nordic Adventure
(12, 12),  -- Greece for Greek Islands
(13, 13),  -- Iceland for Icelandic Wonders
(14, 14),  -- Sweden for Scandinavian Capitals
(15, 15),  -- Portugal for Portuguese Retreat
(16, 16);  -- Turkey for Turkish Delight
