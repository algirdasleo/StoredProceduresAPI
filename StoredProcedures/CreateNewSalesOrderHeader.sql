CREATE PROCEDURE CreateNewSalesOrderHeader
    @DueDate DATETIME,
    @CustomerID INT,
    @ShipToAddressID INT = NULL,   -- Optional parameter
    @BillToAddressID INT = NULL,   -- Optional parameter
    @ShipMethod NVARCHAR(50),
    @ErrorMessage NVARCHAR(4000) OUTPUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM SalesLT.Customer WHERE CustomerID = @CustomerID)
        SET @ErrorMessage = COALESCE(@ErrorMessage + ' ', '') + 'Customer does not exist.';

    IF @ShipToAddressID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM SalesLT.Address WHERE AddressID = @ShipToAddressID)
        SET @ErrorMessage = COALESCE(@ErrorMessage + ' ', '') + 'Shipping address does not exist.';

    IF @BillToAddressID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM SalesLT.Address WHERE AddressID = @BillToAddressID)
        SET @ErrorMessage = COALESCE(@ErrorMessage + ' ', '') + 'Billing address does not exist.';

    IF @ErrorMessage IS NOT NULL
        RETURN;

    INSERT INTO SalesLT.SalesOrderHeader (DueDate, CustomerID, ShipToAddressID, BillToAddressID, ShipMethod)
    VALUES (@DueDate, @CustomerID, @ShipToAddressID, @BillToAddressID, @ShipMethod);
END