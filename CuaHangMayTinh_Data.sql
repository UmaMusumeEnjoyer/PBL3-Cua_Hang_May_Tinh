-- 1. Nhân viên
INSERT INTO Employee (employeeName, age, phoneNumber)
VALUES
  (N'Nguyễn Văn A', 30, N'0901234567'),
  (N'Trần Thị B',   28, N'0912345678'),
  (N'Lê Văn C',     35, N'0923456789');

-- 2. Khách hàng
INSERT INTO Customer (customerName, phoneNumber, email, address)
VALUES
  (N'Phạm Thị D', N'0934567890', N'pham.d@example.com',     N'Hà Nội, Việt Nam'),
  (N'Hoàng Văn E',N'0945678901', N'hoang.e@example.com',   N'Hồ Chí Minh, Việt Nam'),
  (N'Trần Thị F', N'0950001112', N'tran.f@example.com',     N'Đà Nẵng, Việt Nam');

-- 3. Nhà cung cấp
INSERT INTO Supplier (supplierName, phoneNumber, email, address)
VALUES
  (N'Công ty ABC', N'0961112223', N'contact@abc-supplier.com', N'Đà Nẵng, Việt Nam'),
  (N'Công ty XYZ', N'0972223334', N'info@xyz-supplier.com',    N'Cần Thơ, Việt Nam');

-- 4. Sản phẩm (bao gồm soft-delete)
INSERT INTO Product (Supplier_Id, productName, price, stockQuantity, IsDeleted)
VALUES
  (1, N'Dell Inspiron 15', 15000000.00, 10, 0),
  (1, N'Logitech M185 Mouse',      500000.00, 50, 0),
  (2, N'MSI Gaming PC',    25000000.00,  5, 0);

-- 5. Chi tiết mỗi loại sản phẩm
INSERT INTO Laptop (Product_Id, laptopName, weight, screenSize, specification, colour)
VALUES
  (1, N'Dell Inspiron 15', 1.80, 15.6, N'Intel Core i5, 8GB RAM, 256GB SSD', N'Bạc');

INSERT INTO Accessories (Product_Id, accessoriesName, overview)
VALUES
  (2, N'Logitech M185 Wireless Mouse', N'Chuột không dây, kết nối USB, sử dụng pin AA');

INSERT INTO PC (Product_Id, pcName, specification)
VALUES
  (3, N'MSI Gaming PC', N'AMD Ryzen 7, 16GB RAM, NVIDIA RTX3060, 1TB SSD');

-- 6. Tạo hoá đơn bán (Receipt) và phiếu nhập kho (Goods_Receipt)
INSERT INTO Receipt (receiptDate, Customer_Id, Employee_Id, IsCanceled)
VALUES
  ('2025-04-01', 1, 1, 0),
  ('2025-04-02', 2, 2, 0);

INSERT INTO Goods_Receipt (goodsReceiptDate, Employee_Id, IsCanceled)
VALUES
  ('2025-04-03', 1, 0);

-- 7. Thêm dòng chi tiết (Details)
INSERT INTO Details (Product_Id, quantity, productPrice, Receipt_Id, GoodsReceipt_Id, AdjustmentType, OriginalDetailId)
VALUES
  -- Hóa đơn bán #1: 2 chiếc Dell Inspiron 15
  (1, 2, 15000000.00, 1, NULL, N'ORIGINAL', NULL),
  -- Hóa đơn bán #2: 5 chuột Logitech
  (2, 5,   500000.00, 2, NULL, N'ORIGINAL', NULL),
  -- Phiếu nhập kho #1: nhập 1 MSI PC
  (3, 1, 25000000.00, NULL, 1, N'ORIGINAL', NULL);

INSERT INTO Account (username, password, Employee_Id, role)
VALUES
  (N'adminA', N'admin123', 1, N'Admin'),
  (N'nvB',    N'passwordB',2, N'Staff'),
  (N'nvC',    N'passwordC',3, N'Warehouse');
