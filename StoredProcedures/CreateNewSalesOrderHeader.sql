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
    BEGIN
        IF @ErrorMessage IS NULL
        BEGIN
            SET @ErrorMessage = 'Customer does not exist.';
        END
        ELSE 
        BEGIN
            SET @ErrorMessage = @ErrorMessage + ' Customer does not exist.';
        END
    END

    IF @ShipToAddressID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM SalesLT.Address WHERE AddressID = @ShipToAddressID)
    BEGIN
        IF @ErrorMessage IS NULL
        BEGIN
            SET @ErrorMessage = 'Shipping address does not exist.';
        END
        ELSE
        BEGIN
            SET @ErrorMessage = @ErrorMessage + ' Shipping address does not exist.';
        END
    END

    IF @BillToAddressID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM SalesLT.Address WHERE AddressID = @BillToAddressID)
    BEGIN
        IF @ErrorMessage IS NULL
        BEGIN
            SET @ErrorMessage = 'Billing address does not exist.';
        END
        ELSE
        BEGIN
            SET @ErrorMessage = @ErrorMessage + ' Billing address does not exist.';
        END
    END

    IF @ErrorMessage IS NOT NULL
    BEGIN
        RETURN;
    END

    INSERT INTO SalesLT.SalesOrderHeader (DueDate, CustomerID, ShipToAddressID, BillToAddressID, ShipMethod)
    VALUES (@DueDate, @CustomerID, @ShipToAddressID, @BillToAddressID, @ShipMethod);
END