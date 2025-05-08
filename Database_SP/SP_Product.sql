-- 1. Lấy tất cả sản phẩm chưa bị xóa
CREATE PROCEDURE sp_Product_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.*, s.supplierName
    FROM Product p
    INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
    WHERE p.IsDeleted = 0;
END
GO

-- 2. Lấy tất cả sản phẩm đã xóa (thùng rác)
CREATE PROCEDURE sp_Product_GetDeleted
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.*, s.supplierName
    FROM Product p
    INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
    WHERE p.IsDeleted = 1;
END
GO

-- 3. Lấy sản phẩm theo ID (chưa xóa)
CREATE PROCEDURE sp_Product_GetById
    @Product_Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.*, s.supplierName
    FROM Product p
    INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
    WHERE p.Product_Id = @Product_Id
      AND p.IsDeleted = 0;
END
GO

-- 4. Tìm kiếm sản phẩm theo keyword
CREATE PROCEDURE sp_Product_Search
    @Keyword NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.*, s.supplierName
    FROM Product p
    INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
    WHERE p.productName LIKE @Keyword
      AND p.IsDeleted = 0;
END
GO

-- 5. Khôi phục (Restore) sản phẩm
CREATE PROCEDURE sp_Product_Restore
    @Product_Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Product
    SET IsDeleted = 0
    WHERE Product_Id = @Product_Id;
END
GO

--------------------------------------------------------------------------------
-- 6. Insert Product + Laptop
CREATE PROCEDURE sp_Product_InsertWithLaptop
    @Supplier_Id      INT,
    @ProductName      NVARCHAR(100),
    @Price            DECIMAL(10,2),
    @StockQuantity    INT,
    @LaptopName       NVARCHAR(100),
    @Weight           DECIMAL(5,2),
    @ScreenSize       DECIMAL(4,1),
    @Specification    NVARCHAR(MAX),
    @Colour           NVARCHAR(50),
    @NewProductId     INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
        VALUES (@Supplier_Id, @ProductName, @Price, @StockQuantity);

        SET @NewProductId = SCOPE_IDENTITY();

        INSERT INTO Laptop (Product_Id, laptopName, weight, screenSize, specification, colour)
        VALUES (@NewProductId, @LaptopName, @Weight, @ScreenSize, @Specification, @Colour);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 7. Insert Product + PC
CREATE PROCEDURE sp_Product_InsertWithPC
    @Supplier_Id      INT,
    @ProductName      NVARCHAR(100),
    @Price            DECIMAL(10,2),
    @StockQuantity    INT,
    @PCName           NVARCHAR(100),
    @Specification    NVARCHAR(MAX),
    @NewProductId     INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
        VALUES (@Supplier_Id, @ProductName, @Price, @StockQuantity);

        SET @NewProductId = SCOPE_IDENTITY();

        INSERT INTO PC (Product_Id, pcName, specification)
        VALUES (@NewProductId, @PCName, @Specification);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 8. Insert Product + Accessories
CREATE PROCEDURE sp_Product_InsertWithAccessory
    @Supplier_Id      INT,
    @ProductName      NVARCHAR(100),
    @Price            DECIMAL(10,2),
    @StockQuantity    INT,
    @AccessoriesName  NVARCHAR(100),
    @Overview         NVARCHAR(MAX),
    @NewProductId     INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
        VALUES (@Supplier_Id, @ProductName, @Price, @StockQuantity);

        SET @NewProductId = SCOPE_IDENTITY();

        INSERT INTO Accessories (Product_Id, accessoriesName, overview)
        VALUES (@NewProductId, @AccessoriesName, @Overview);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

--------------------------------------------------------------------------------
-- 9. Update Product + Laptop
CREATE PROCEDURE sp_Product_UpdateWithLaptop
    @Product_Id       INT,
    @Supplier_Id      INT,
    @ProductName      NVARCHAR(100),
    @Price            DECIMAL(10,2),
    @StockQuantity    INT,
    @LaptopName       NVARCHAR(100),
    @Weight           DECIMAL(5,2),
    @ScreenSize       DECIMAL(4,1),
    @Specification    NVARCHAR(MAX),
    @Colour           NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Product
        SET Supplier_Id   = @Supplier_Id,
            productName   = @ProductName,
            price         = @Price,
            stockQuantity = @StockQuantity
        WHERE Product_Id = @Product_Id;

        UPDATE Laptop
        SET laptopName    = @LaptopName,
            weight        = @Weight,
            screenSize    = @ScreenSize,
            specification = @Specification,
            colour        = @Colour
        WHERE Product_Id = @Product_Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 10. Update Product + PC
CREATE PROCEDURE sp_Product_UpdateWithPC
    @Product_Id       INT,
    @Supplier_Id      INT,
    @ProductName      NVARCHAR(100),
    @Price            DECIMAL(10,2),
    @StockQuantity    INT,
    @PCName           NVARCHAR(100),
    @Specification    NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Product
        SET Supplier_Id   = @Supplier_Id,
            productName   = @ProductName,
            price         = @Price,
            stockQuantity = @StockQuantity
        WHERE Product_Id = @Product_Id;

        UPDATE PC
        SET pcName       = @PCName,
            specification= @Specification
        WHERE Product_Id = @Product_Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 11. Update Product + Accessories
CREATE PROCEDURE sp_Product_UpdateWithAccessory
    @Product_Id       INT,
    @Supplier_Id      INT,
    @ProductName      NVARCHAR(100),
    @Price            DECIMAL(10,2),
    @StockQuantity    INT,
    @AccessoriesName  NVARCHAR(100),
    @Overview         NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Product
        SET Supplier_Id   = @Supplier_Id,
            productName   = @ProductName,
            price         = @Price,
            stockQuantity = @StockQuantity
        WHERE Product_Id = @Product_Id;

        UPDATE Accessories
        SET accessoriesName = @AccessoriesName,
            overview        = @Overview
        WHERE Product_Id = @Product_Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

--------------------------------------------------------------------------------
-- 12. Delete (soft-delete) 
CREATE PROCEDURE sp_Product_Delete
    @Product_Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Xóa bản ghi con (nếu có)
        DELETE FROM Laptop      WHERE Product_Id = @Product_Id;
        DELETE FROM PC          WHERE Product_Id = @Product_Id;
        DELETE FROM Accessories WHERE Product_Id = @Product_Id;
        UPDATE Product
        SET IsDeleted = 1
        WHERE Product_Id = @Product_Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE sp_Product_GetAllDetails
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        p.Product_Id,
        p.productName,
        CASE 
          WHEN l.Product_Id IS NOT NULL THEN 'Laptop'
          WHEN pc.Product_Id IS NOT NULL THEN 'PC'
          ELSE 'Accessories'
        END AS Category,
        COALESCE(l.specification, pc.specification, a.overview) AS Specification,
        COALESCE(l.colour, '') AS Colour,
        p.price,
        p.stockQuantity
    FROM Product p
    LEFT JOIN Laptop l ON p.Product_Id = l.Product_Id
    LEFT JOIN PC pc ON p.Product_Id = pc.Product_Id
    LEFT JOIN Accessories a ON p.Product_Id = a.Product_Id
    WHERE p.IsDeleted = 0;
END
GO

