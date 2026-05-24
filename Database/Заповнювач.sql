INSERT INTO Positions (Name) VALUES
('Адміністратор'),
('Касир');

INSERT INTO Employees (FullName, Login, Password, PositionId) VALUES
('Іваненко Іван Іванович', 'admin', 'admin123', 1),
('Петренко Петро Петрович', 'cashier', 'cashier123', 2);

INSERT INTO Fuel (Name, FuelType, PricePerLiter, AmountLiters) VALUES
('А-95', 'Бензин', 58.50, 5000),
('ДП', 'Дизель', 55.20, 4000),
('Газ', 'LPG', 32.00, 3000),
('А-95 Turbo', 'Бензин', 62.00, 3500),
('А-92', 'Бензин', 54.00, 4500),
('Дизель', 'Дизель', 55.20, 4000);

INSERT INTO FuelColumns (ColumnNumber, FuelId, Status) VALUES
(1, 1, 'Вільна'),
(2, 2, 'Вільна'),
(3, 3, 'Вільна');

INSERT INTO ProductCategories (Name) VALUES
('Напої'),
('Їжа'),
('Автотовари');

INSERT INTO Products (Name, CategoryId, Price, Quantity) VALUES
('Вода 0.5 л', 1, 25.00, 50),
('Кава', 1, 35.00, 40),
('Хот-дог', 2, 65.00, 25),
('Моторна олива', 3, 250.00, 10);