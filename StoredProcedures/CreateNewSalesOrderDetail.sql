CREATE PROCEDURE CreateNewSalesOrderDetail
    @SalesOrderID INT,
    @OrderQty INT,
    @ProductID INT,
    @UnitPrice MONEY,
    @UnitPriceDiscount MONEY = NULL,      -- Optional parameter
    @ErrorMessage NVARCHAR(4000) OUTPUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM SalesLT.SalesOrderHeader WHERE SalesOrderID = @SalesOrderID)
        SET @ErrorMessage = COALESCE(@ErrorMessage + ' ', '') + 'Sales order does not exist.';

    IF NOT EXISTS (SELECT 1 FROM SalesLT.Product WHERE ProductID = @ProductID)
        SET @ErrorMessage = COALESCE(@ErrorMessage + ' ', '') + 'Product does not exist.';

    IF @ErrorMessage IS NOT NULL
        RETURN;

    DECLARE @SalesOrderDetailID INT;

    INSERT INTO SalesLT.SalesOrderDetail (SalesOrderID, OrderQty, ProductID, UnitPrice, UnitPriceDiscount)
    VALUES (@SalesOrderID, @OrderQty, @ProductID, @UnitPrice, @UnitPriceDiscount);

    SET @SalesOrderDetailID = SCOPE_IDENTITY();

    IF @SalesOrderDetailID IS NULL
        SET @ErrorMessage = 'Failed to create new sales order detail.';
END