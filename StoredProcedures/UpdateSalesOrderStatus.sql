CREATE PROCEDURE UpdateSalesOrderStatus
    @SalesOrderID int,
    @Status tinyint,
    @ErrorMessage NVARCHAR(4000) OUTPUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM SalesLT.SalesOrderHeader WHERE SalesOrderID = @SalesOrderID)
    BEGIN
        SET @ErrorMessage = 'Sales order does not exist.';
        RETURN;
    END
    UPDATE SalesLT.SalesOrderHeader
    SET Status = @Status
    WHERE SalesOrderID = @SalesOrderID;
END