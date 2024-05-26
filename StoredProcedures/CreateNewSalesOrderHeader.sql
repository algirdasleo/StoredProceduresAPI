CREATE PROCEDURE CreateNewSalesOrderHeader
    @DueDate DATETIME,
    @CustomerID INT,
    @ShipMethod NVARCHAR(50)
AS
BEGIN
    INSERT INTO SalesLT.SalesOrderHeader (DueDate, CustomerID, ShipMethod)
    VALUES (@DueDate, @CustomerID, @ShipMethod)
END