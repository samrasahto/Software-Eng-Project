CREATE TABLE Departments (
  Department_ID INTEGER IDENTITY(1,1) NOT NULL,
  Department_Name varchar(50) NULL,
  PRIMARY KEY(Department_ID)
);

CREATE TABLE Food_Categories (
  Category_ID INTEGER IDENTITY(1,1) NOT NULL,
  Category_Name varchar(50) NULL,
  Description TEXT NULL,
  PRIMARY KEY(Category_ID)
);

CREATE TABLE City (
  idCity INTEGER IDENTITY(1,1) NOT NULL,
  City_Name varchar(50) NULL,
  PRIMARY KEY(idCity)
);

CREATE TABLE Login (
  idLogin INTEGER IDENTITY(1,1) NOT NULL,
  User_Name varchar(50) NULL,
  Password_2 varchar(50) NULL,
  PRIMARY KEY(idLogin)
);

CREATE TABLE Products (
  Product_ID INTEGER IDENTITY(1,1) NOT NULL,
  Food_Categories_Category_ID INTEGER NOT NULL,
  Product_Name varchar(50) NULL,
  Units_In_Stock INTEGER NULL,
  Discontinued BIT,
  Unit_Price FLOAT NULL,
  PRIMARY KEY(Product_ID),
  INDEX Products_FKIndex1(Food_Categories_Category_ID),
  FOREIGN KEY(Food_Categories_Category_ID)
    REFERENCES Food_Categories(Category_ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
);

CREATE TABLE Job (
  Job_Id INTEGER IDENTITY(1,1) NOT NULL ,
  Job_Name varchar(50) NULL,
  Salary_Per_Hour INTEGER NULL,
  PRIMARY KEY(Job_Id)
);

CREATE TABLE Employee (
  Employee_ID INTEGER IDENTITY(1,1) NOT NULL,
  Job_Id INTEGER NOT NULL,
  City_idCity INTEGER  NOT NULL,
  Manager_ID INTEGER  NULL,
  Departments_Department_ID INTEGER  NOT NULL,
  First_Name varchar(50) NULL,
  Last_Name varchar(50) NULL,
  Contact varchar(20) NULL,
  Gender varchar(50) NULL,
  Address varchar(50) NULL,
  Email varchar(50) NULL,
  PRIMARY KEY(Employee_ID),
  INDEX Employee_FKIndex1(Manager_ID),
  INDEX Employee_FKIndex2(Departments_Department_ID),
  INDEX Employee_FKIndex3(City_idCity),
  INDEX Employee_FKIndex4(Job_Id),
  FOREIGN KEY(Manager_ID)
    REFERENCES Employee(Employee_ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION,
  FOREIGN KEY(Departments_Department_ID)
    REFERENCES Departments(Department_ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION,
  FOREIGN KEY(City_idCity)
    REFERENCES City(idCity)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION,
  FOREIGN KEY(Job_Id)
    REFERENCES Job(Job_Id)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
);

CREATE TABLE Customer (
  Customer_ID INTEGER IDENTITY(1,1) NOT NULL,
  Login_idLogin INTEGER  NOT NULL,
  City_idCity INTEGER  NOT NULL,
  First_Name varchar(50) NULL,
  Last_Name varchar(50) NULL,
  Contact varchar(20) NULL,
  Address varchar(50) NULL,
  Email varchar(50) NULL,
  Gender varchar(50) NULL,
  PRIMARY KEY(Customer_ID),
  INDEX Customer_FKIndex1(City_idCity),
  INDEX Customer_FKIndex2(Login_idLogin),
  FOREIGN KEY(City_idCity)
    REFERENCES City(idCity)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION,
  FOREIGN KEY(Login_idLogin)
    REFERENCES Login(idLogin)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
);

CREATE TABLE Orders (
  Order_ID INTEGER IDENTITY(1,1) NOT NULL,
  Customer_ID INTEGER  NOT NULL,
  Order_Date DATE NULL,
  Shipping_Date DATE NULL,
  Expected_Date DATE NULL,
  Order_Status varchar(50) NULL,
  Total INTEGER NULL,
  PRIMARY KEY(Order_ID),
  INDEX Orders_FKIndex1(Customer_ID),
  FOREIGN KEY(Customer_ID)
    REFERENCES Customer(Customer_ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
);

CREATE TABLE Order_Details (
  Orders_Order_ID INTEGER  NOT NULL,
  Products_Product_ID INTEGER  NOT NULL,
  Quantity INTEGER NOT NULL,
  PRIMARY KEY(Orders_Order_ID, Products_Product_ID),
  INDEX Order_Details_FKIndex1(Orders_Order_ID),
  INDEX Order_Details_FKIndex2(Products_Product_ID),
  FOREIGN KEY(Orders_Order_ID)
    REFERENCES Orders(Order_ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION,
  FOREIGN KEY(Products_Product_ID)
    REFERENCES Products(Product_ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
);
