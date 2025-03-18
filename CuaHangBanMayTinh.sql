CREATE DATABASE PBL3;
USE PBL3;

CREATE TABLE Employee (
    Employee_Id INT PRIMARY KEY IDENTITY(1,1),
    employeeName NVARCHAR(100) NOT NULL,
    age INT,
    phoneNumber NVARCHAR(20)
);

CREATE TABLE Customer (
    Customer_Id INT PRIMARY KEY IDENTITY(1,1),
    customerName NVARCHAR(100) NOT NULL,
    phoneNumber NVARCHAR(20),
    email NVARCHAR(100),
    address NVARCHAR(200)
);

CREATE TABLE Supplier (
    Supplier_Id INT PRIMARY KEY IDENTITY(1,1),
    supplierName NVARCHAR(100) NOT NULL,
    phoneNumber NVARCHAR(20),
    email NVARCHAR(100),
    address NVARCHAR(200)
);

CREATE TABLE Product (
    Product_Id INT PRIMARY KEY IDENTITY(1,1),
    Supplier_Id INT,
    productName NVARCHAR(100) NOT NULL,
    price DECIMAL(10,2),
    stockQuantity INT,
    FOREIGN KEY (Supplier_Id) REFERENCES Supplier(Supplier_Id)
);

CREATE TABLE Laptop (
    Product_Id INT PRIMARY KEY,
    laptopName NVARCHAR(100),
    weight DECIMAL(5,2),
    screenSize DECIMAL(4,1),
    specification NVARCHAR(MAX),
    colour NVARCHAR(50),
    FOREIGN KEY (Product_Id) REFERENCES Product(Product_Id)
);

CREATE TABLE Accessories (
    Product_Id INT PRIMARY KEY,
    accessoriesName NVARCHAR(100),
    overview NVARCHAR(MAX),
    FOREIGN KEY (Product_Id) REFERENCES Product(Product_Id)
);

CREATE TABLE PC (
    Product_Id INT PRIMARY KEY,
    pcName NVARCHAR(100),
    specification NVARCHAR(MAX),
    FOREIGN KEY (Product_Id) REFERENCES Product(Product_Id)
);

CREATE TABLE Details (
    Details_Id INT PRIMARY KEY IDENTITY(1,1),
    Product_Id INT,
    quantity INT,
    productPrice DECIMAL(10,2),
    FOREIGN KEY (Product_Id) REFERENCES Product(Product_Id)
);

CREATE TABLE Receipt (
    Receipt_Id INT PRIMARY KEY IDENTITY(1,1),
    Details_Id INT,
    totalAmount DECIMAL(10,2),
    receiptDate DATE,
    Customer_Id INT,
    Employee_Id INT,
    FOREIGN KEY (Details_Id) REFERENCES Details(Details_Id),
    FOREIGN KEY (Customer_Id) REFERENCES Customer(Customer_Id),
    FOREIGN KEY (Employee_Id) REFERENCES Employee(Employee_Id)
);

CREATE TABLE Goods_Receipt (
    goodsReceipt_Id INT PRIMARY KEY IDENTITY(1,1),
    Employee_Id INT,
    Details_Id INT,
    totalAmount DECIMAL(10,2),
    goodsReceiptDate DATE,
    FOREIGN KEY (Employee_Id) REFERENCES Employee(Employee_Id),
    FOREIGN KEY (Details_Id) REFERENCES Details(Details_Id)
);

CREATE TABLE Account (
    Id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(100) NOT NULL,
    Employee_Id INT,
    role NVARCHAR(50),
    FOREIGN KEY (Employee_Id) REFERENCES Employee(Employee_Id)
);
