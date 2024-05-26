CREATE PROCEDURE UpdateSalesOrderStatus
    @SalesOrderID int,
    @Status tinyint
AS
BEGIN
    UPDATE SalesLT.SalesOrderHeader
    SET Status = @Status
    WHERE SalesOrderID = @SalesOrderID;
END