CREATE DATABASE IF NOT EXISTS GoDriveDB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

USE GoDriveDB;

CREATE TABLE Positions (
    PositionId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Employees (
    EmployeeId INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Login VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    PositionId INT NOT NULL,
    FOREIGN KEY (PositionId) REFERENCES Positions(PositionId)
);

CREATE TABLE Fuel (
    FuelId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    FuelType VARCHAR(50) NOT NULL,
    PricePerLiter DECIMAL(10,2) NOT NULL,
    AmountLiters DECIMAL(10,2) NOT NULL
);

CREATE TABLE FuelColumns (
    ColumnId INT AUTO_INCREMENT PRIMARY KEY,
    ColumnNumber INT NOT NULL UNIQUE,
    FuelId INT NOT NULL,
    Status VARCHAR(30) NOT NULL DEFAULT 'Вільна',
    FOREIGN KEY (FuelId) REFERENCES Fuel(FuelId)
);

CREATE TABLE ProductCategories (
    CategoryId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Products (
    ProductId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    CategoryId INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES ProductCategories(CategoryId)
);

CREATE TABLE Shifts (
    ShiftId INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NULL,
    TotalMoney DECIMAL(10,2) DEFAULT 0,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

CREATE TABLE Sales (
    SaleId INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeId INT NOT NULL,
    ShiftId INT NOT NULL,
    SaleDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TotalPrice DECIMAL(10,2) NOT NULL,
    PaymentType VARCHAR(30) NOT NULL,
    Status VARCHAR(30) NOT NULL DEFAULT 'Завершено',
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    FOREIGN KEY (ShiftId) REFERENCES Shifts(ShiftId)
);

CREATE TABLE SaleItems (
    SaleItemId INT AUTO_INCREMENT PRIMARY KEY,
    SaleId INT NOT NULL,
    ProductId INT NULL,
    FuelId INT NULL,
    Quantity DECIMAL(10,2) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (SaleId) REFERENCES Sales(SaleId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    FOREIGN KEY (FuelId) REFERENCES Fuel(FuelId)
);



CREATE TABLE Promotions (
    PromotionId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,

    TargetType VARCHAR(30) NOT NULL,

    ProductId INT NULL,
    CategoryId INT NULL,
    FuelId INT NULL,

    MinQuantity DECIMAL(10,2) NOT NULL DEFAULT 1,

    ConditionType VARCHAR(30) NOT NULL DEFAULT 'None',

    ConditionFuelId INT NULL,
    ConditionProductId INT NULL,

    ConditionMinLiters DECIMAL(10,2) NULL,
    ConditionMinProductQuantity DECIMAL(10,2) NULL,

    DiscountType VARCHAR(30) NOT NULL,
    DiscountValue DECIMAL(10,2) NOT NULL,

    MaxDiscountQuantity DECIMAL(10,2) NULL,

    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsActive TINYINT(1) NOT NULL DEFAULT 1,

    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    FOREIGN KEY (CategoryId) REFERENCES ProductCategories(CategoryId),
    FOREIGN KEY (FuelId) REFERENCES Fuel(FuelId),
    FOREIGN KEY (ConditionFuelId) REFERENCES Fuel(FuelId),
    FOREIGN KEY (ConditionProductId) REFERENCES Products(ProductId)
);
