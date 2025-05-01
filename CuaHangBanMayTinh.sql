CREATE TABLE Employee (
    Employee_Id   INT IDENTITY(1,1) PRIMARY KEY,
    employeeName  NVARCHAR(100)    NOT NULL,
    age           INT              NULL,
    phoneNumber   NVARCHAR(20)     NULL
);
GO

CREATE TABLE Customer (
    Customer_Id   INT IDENTITY(1,1) PRIMARY KEY,
    customerName  NVARCHAR(100)    NOT NULL,
    phoneNumber   NVARCHAR(20)     NULL,
    email         NVARCHAR(100)    NULL,
    address       NVARCHAR(200)    NULL
);
GO

CREATE TABLE Supplier (
    Supplier_Id   INT IDENTITY(1,1) PRIMARY KEY,
    supplierName  NVARCHAR(100)    NOT NULL,
    phoneNumber   NVARCHAR(20)     NULL,
    email         NVARCHAR(100)    NULL,
    address       NVARCHAR(200)    NULL
);
GO

CREATE TABLE Product (
    Product_Id     INT IDENTITY(1,1) PRIMARY KEY,
    Supplier_Id    INT              NOT NULL
                         REFERENCES Supplier(Supplier_Id),
    productName    NVARCHAR(100)    NOT NULL,
    price          DECIMAL(10,2)    NULL,
    stockQuantity  INT              NULL
);
GO

CREATE TABLE Laptop (
    Product_Id    INT PRIMARY KEY
                    REFERENCES Product(Product_Id)
                    ON DELETE CASCADE,
    laptopName    NVARCHAR(100) NULL,
    weight        DECIMAL(5,2)   NULL,
    screenSize    DECIMAL(4,1)   NULL,
    specification NVARCHAR(MAX)  NULL,
    colour        NVARCHAR(50)   NULL
);
GO

CREATE TABLE Accessories (
    Product_Id       INT PRIMARY KEY
                             REFERENCES Product(Product_Id)
                             ON DELETE CASCADE,
    accessoriesName  NVARCHAR(100) NULL,
    overview         NVARCHAR(MAX) NULL
);
GO

CREATE TABLE PC (
    Product_Id    INT PRIMARY KEY
                   REFERENCES Product(Product_Id)
                   ON DELETE CASCADE,
    pcName        NVARCHAR(100) NULL,
    specification NVARCHAR(MAX) NULL
);
GO


CREATE TABLE Receipt (
    Receipt_Id   INT IDENTITY(1,1) PRIMARY KEY,
    receiptDate  DATE             NOT NULL,
    Customer_Id  INT              NOT NULL
                   REFERENCES Customer(Customer_Id),
    Employee_Id  INT              NOT NULL
                   REFERENCES Employee(Employee_Id)
);
GO


CREATE TABLE Goods_Receipt (
    GoodsReceipt_Id  INT IDENTITY(1,1) PRIMARY KEY,
    goodsReceiptDate DATE             NOT NULL,
    Employee_Id      INT              NOT NULL
                     REFERENCES Employee(Employee_Id)
);
GO

CREATE TABLE Details (
    Details_Id       INT IDENTITY(1,1) PRIMARY KEY,
    Product_Id       INT              NOT NULL
                         REFERENCES Product(Product_Id),
    quantity         INT              NOT NULL CHECK(quantity > 0),
    productPrice     DECIMAL(10,2)    NOT NULL CHECK(productPrice >= 0),
    Receipt_Id       INT              NULL
                         REFERENCES Receipt(Receipt_Id)
                         ON DELETE CASCADE,
    GoodsReceipt_Id  INT              NULL
                         REFERENCES Goods_Receipt(GoodsReceipt_Id)
                         ON DELETE CASCADE
);
GO


CREATE TABLE Account (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    username      NVARCHAR(50)       NOT NULL UNIQUE,
    password      NVARCHAR(100)      NOT NULL,
    Employee_Id   INT                NOT NULL
                     REFERENCES Employee(Employee_Id)
                     ON DELETE CASCADE,
    role          NVARCHAR(50)       NULL
);
GO
