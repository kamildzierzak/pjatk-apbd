INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel)
VALUES 
('John', 'Doe', 'john.doe@example.com', '123-456-789', '12345678901'),
('Jane', 'Smith', 'jane.smith@example.com', '987-654-321', '98765432109'),
('Michael', 'Johnson', 'michael.johnson@example.com', '555-123-456', '11223344556');
('Delete Me Please', 'Johnson', 'michael.johnson@example.com', '555-123-456', '11223344556');

INSERT INTO Country (Name)
VALUES 
('USA'),
('Poland'),
('France');

INSERT INTO Trip (Name, Description, DateFrom, DateTo, MaxPeople)
VALUES 
('Summer Vacation', 'A relaxing summer trip to the coast', '2024-06-01', '2024-06-14', 50),
('Winter Adventure', 'A thrilling winter holiday in the mountains', '2024-12-10', '2024-12-20', 30),
('Cultural Exploration', 'A cultural tour through historical landmarks', '2025-03-01', '2025-03-07', 20);

INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate)
VALUES 
(1, 1, '2024-04-01 10:00:00', '2024-04-05 14:00:00'),
(2, 2, '2024-04-15 09:30:00', '2024-04-20 16:00:00'),
(3, 3, '2024-05-01 12:00:00', NULL);

INSERT INTO Country_Trip (IdCountry, IdTrip)
VALUES 
(1, 1),  -- USA for Summer Vacation
(2, 2),  -- Poland for Winter Adventure
(3, 3);  -- France for Cultural Exploration
