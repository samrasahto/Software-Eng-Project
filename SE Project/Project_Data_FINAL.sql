-- Inserting some initial garbage data
INSERT INTO City values ('Karachi'), ('Islamabad'), ('Lahore'), ('Multan'), ('Faisalabad'), ('Murree'), ('Quetta'), ('Bahawalpur'), ('Hyderabad'), ('Hunza'), ('Shikarpur'), ('Rawalpindi');

INSERT INTO Departments values ('Production'),('Packaging'), ('Research and Development'),
('Human Resources'), ('Accounting and Finance'), ('Maintenance'), ('Marketing'), ('Sales'), ('General Managment'), ('Finance');

INSERT INTO Food_Categories values ('Vegetables', 'healthy veggies'), ('Mutton', 'goat meat items'), ('Desi', 'spicy pakistani frozen food'), 
('Baked', 'bakery items'), ('Ready to Eat','products that just need to be warmed'),('Ready to Cook', 'products that have to be cooked');

INSERT INTO Products values (1,'Peas', 500, 0, 15),(1,'Mixed Vegetable', 700, 0, 25), (6,'Chicken Samosa', 750, 0, 10), (3,'Pakoras', 500, 0, 15),
(2,'Goat Meat patties', 200, 0, 200), (5,'Camel on skewer', 100, 1, 500), (6,'Aloo Samosa', 550, 0, 10), (6,'Vegetable Patties', 750, 0, 10), 
(4,'Mini Pizza', 100, 0, 55), (5,'Malai Boti', 200, 0, 75), (5,'Gola Kabab', 100, 0, 55), (4,'Quiche', 100, 0, 55), (3,'Paratha', 245, 0, 55),
(3,'Naan', 210, 0, 35);

INSERT INTO [Login] values ('shayan_1', 'password_1'), ('addi', 'password_2'), ('zaki_zaki', 'password_3'), ('khizer99', 'password_4'), 
('hammad101', 'password_5'), ('aleen20', 'password_6'), ('pinchofsass', 'password_7'), ('yomama', 'password_8');

INSERT INTO Customer values (1, 1, 'Shayan', 'Aamir', 03162508074, 'DHA', 'shayan@hotmail.com', 'Male'),
(2, 2, 'Shafaq', 'Mughal', 03162544074, 'Defence', 'shafaq@gmail.com', 'Feale'),
(3, 3, 'Zaki', 'Zaki', 03162508074, 'DHA', 'zaki_zaki@hotmail.com', 'Male'),
(4, 4, 'Khizer', 'Shehzad', 03166608079, 'North Nazimabad', 'khizzz@hotmail.com', 'Male'),
(5, 5, 'Hammad', 'Makhdoom', 03162555075, 'Gulshan', 'hmm88@gmail.com', 'Male'),
(6, 6, 'Aleen', 'Hussain', 03122508000, 'DHA Phase 2', 'aleen4ever@hotmail.com', 'Female'),
(7, 8, 'Samarah', 'Asghar', 03332555078, 'DHA Phase 7', 'ss05572@gmail.com', 'Female'),
(8, 7, 'Omama', 'Barak', 03322508333, 'Clifton', 'yomama@gmail.com', 'Female');

INSERT INTO Job values ('General Manager', 50), ('Sales Expert', 25), ('Customer Service', 20), ('Accountant', 50), ('HR Person', 25), 
('Packman', 20), ('Maintainance Man', 50), ('Sales Expert', 25);

INSERT INTO Employee values (2, 1, NULL, 1, 'Samra', 'Sahto', 03125478412, 'Female', 'Middle of nowhere', 'seenzone@gmail.com'),
(1, 2, 1, 1, 'Umema', 'Zehra', 05216547899, 'Female', 'Middle of somewhere', 'workinghard@gmail.com'),
(1, 1, 1, 2, 'Zoha', 'Karim', 05487965584, 'Female', 'Gulshan', 'zoha@gmail.com');

INSERT INTO Orders values (1, '2020-11-26', NULL, '2020-12-06', 'Not Complete', 100),
(2, '2020-11-16', '2020-11-21', '2020-11-28', 'Completed', 200),
(1, '2020-11-26', NULL, '2020-12-06', 'Not Complete', 300),
(4, '2020-11-16', '2020-11-20', '2020-11-29', 'Completed', 500),
(3, '2020-10-26', NULL, '2020-12-06', 'Not Complete', 250),
(6, '2020-11-16', '2020-10-2', '2020-11-30', 'Completed', 400),
(7, '2020-11-26', NULL, '2020-12-06', 'Not Complete', 200),
(5, '2020-10-6', '2020-10-2', '2020-10-20', 'Completed', 1000);

INSERT INTO Order_Details Values (1, 2, 100), (2, 1, 100), (3, 3, 50), (4, 6, 75), (5, 4, 50), (6, 5, 100), (7, 7, 50), (8, 8, 75);

-- Queries to just check current data in the table which ensure if the new data has been added or not
SELECT * FROM City
SELECT * FROM Customer
SELECT * FROM Departments
SELECT * FROM Job
SELECT * FROM Orders
SELECT * FROM Products
SELECT * FROM [Login]
SELECT * FROM Food_Categories
SELECT * FROM Employee

