
-- 1. Nhân viên
INSERT INTO Employee (employeeName, age, phoneNumber)
VALUES
  (N'Nguyễn Văn A', 30, '0901234567'),
  (N'Trần Thị B',   28, '0912345678'),
  (N'Lê Văn C',     35, '0923456789');

-- 2. Khách hàng
INSERT INTO Customer (customerName, phoneNumber, email, address)
VALUES
  (N'Phạm Thị D', '0934567890', 'pham.d@example.com', N'Hà Nội'),
  (N'Hoàng Văn E','0945678901','hoang.e@example.com',   N'Hồ Chí Minh');

-- 3. Nhà cung cấp
INSERT INTO Supplier (supplierName, phoneNumber, email, address)
VALUES
  (N'Công ty ABC', '0956789012','abc@supplier.com',  N'Đà Nẵng'),
  (N'Công ty XYZ', '0967890123','xyz@supplier.com',  N'Cần Thơ');

-- 4. Sản phẩm
INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
VALUES
  (1, N'Dell Inspiron 15', 15000000, 10),
  (1, N'Logitech M185',      500000, 50),
  (2, N'MSI Gaming PC',    25000000,  5);

-- 5. Chi tiết mỗi loại sản phẩm
INSERT INTO Laptop (Product_Id, laptopName, weight, screenSize, specification, colour)
VALUES
  (1, N'Dell Inspiron 15', 1.8, 15.6, N'Intel i5,8GB RAM,256GB SSD', N'Bạc');

INSERT INTO Accessories (Product_Id, accessoriesName, overview)
VALUES
  (2, N'Chuột Logitech M185', N'Chuột không dây, USB, pin AA');

INSERT INTO PC (Product_Id, pcName, specification)
VALUES
  (3, N'MSI Gaming PC', N'Ryzen 7,16GB RAM,RTX3060,1TB SSD');

-- 6. Tạo 2 hoá đơn bán và 1 phiếu nhập kho
INSERT INTO Receipt (receiptDate, Customer_Id, Employee_Id)
VALUES
  ('2025-04-01', 1, 1),
  ('2025-04-02', 2, 2);

INSERT INTO Goods_Receipt (goodsReceiptDate, Employee_Id)
VALUES
  ('2025-04-03', 1);

-- 7. Thêm dòng chi tiết (Details)
-- Ví dụ: Hóa đơn 1 mua 2 chiếc Dell, hoá đơn 2 mua 5 con chuột; phiếu nhập kho nhập 1 PC
INSERT INTO Details (Product_Id, quantity, productPrice, Receipt_Id, GoodsReceipt_Id)
VALUES
  (1, 2, 15000000, 1, NULL),
  (2, 5,   500000, 2, NULL),
  (3, 1, 25000000, NULL, 1);

-- 8. Tài khoản người dùng
INSERT INTO Account (username, password, Employee_Id, role)
VALUES
  ('adminA', 'admin123', 1, 'Admin'),
  ('nvB',    'passwordB',2, 'Staff'),
  ('nvC',    'passwordC',3, 'Warehouse');
GO

