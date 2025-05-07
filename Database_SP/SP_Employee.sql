CREATE PROCEDURE sp_Employee_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Employee;
END
GO

-- Lấy nhân viên theo ID
CREATE PROCEDURE sp_Employee_GetById
    @Employee_Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * 
    FROM Employee 
    WHERE Employee_Id = @Employee_Id;
END
GO

