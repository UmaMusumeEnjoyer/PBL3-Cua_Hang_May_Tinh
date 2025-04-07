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

INSERT INTO Employee (employeeName, age, phoneNumber)
VALUES 
(N'Nguyễn Văn A', 30, '0901234567'),
(N'Trần Thị B', 28, '0912345678'),
(N'Lê Văn C', 35, '0923456789');

INSERT INTO Customer (customerName, phoneNumber, email, address)
VALUES 
(N'Phạm Thị D', '0934567890', 'pham.d@example.com', N'Hà Nội'),
(N'Hoàng Văn E', '0945678901', 'hoang.e@example.com', N'Hồ Chí Minh');

INSERT INTO Supplier (supplierName, phoneNumber, email, address)
VALUES 
(N'Công ty ABC', '0956789012', 'abc@supplier.com', N'Đà Nẵng'),
(N'Công ty XYZ', '0967890123', 'xyz@supplier.com', N'Cần Thơ');

INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
VALUES 
(1, N'Dell Inspiron', 15000000, 10),
(1, N'Chuột không dây Logitech', 500000, 50),
(2, N'PC Gaming MSI', 25000000, 5);

INSERT INTO Laptop (Product_Id, laptopName, weight, screenSize, specification, colour)
VALUES 
(2, N'Dell Inspiron 15', 1.80, 15.6, N'Intel i5, 8GB RAM, 256GB SSD', N'Bạc');

INSERT INTO Accessories (Product_Id, accessoriesName, overview)
VALUES 
(2, N'Chuột Logitech M185', N'Chuột không dây, kết nối USB, pin AA');

INSERT INTO PC (Product_Id, pcName, specification)
VALUES 
(3, N'MSI Gaming PC', N'Ryzen 7, 16GB RAM, RTX 3060, 1TB SSD');

INSERT INTO Details (Product_Id, quantity, productPrice)
VALUES 
(1, 2, 15000000),
(2, 5, 500000),
(3, 1, 25000000);

INSERT INTO Receipt (Details_Id, totalAmount, receiptDate, Customer_Id, Employee_Id)
VALUES 
(1, 30000000, '2025-04-01', 1, 1),
(2, 2500000, '2025-04-02', 2, 2);

INSERT INTO Goods_Receipt (Employee_Id, Details_Id, totalAmount, goodsReceiptDate)
VALUES 
(1, 3, 25000000, '2025-04-03');

INSERT INTO Account (username, password, Employee_Id, role)
VALUES 
('adminA', 'admin123', 1, 'Admin'),
('nvB', 'passwordB', 2, 'Nhân viên'),
('nvC', 'passwordC', 3, 'Quản lý kho');
