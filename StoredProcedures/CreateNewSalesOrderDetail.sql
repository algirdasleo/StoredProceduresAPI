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
    BEGIN
        SET @ErrorMessage = 'Sales order does not exist.';
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM SalesLT.Product WHERE ProductID = @ProductID)
    BEGIN
        SET @ErrorMessage = 'Product does not exist.';
        RETURN;
    END

    DECLARE @SalesOrderDetailID INT;
    IF @UnitPriceDiscount IS NULL
    BEGIN
        INSERT INTO SalesLT.SalesOrderDetail(SalesOrderID, OrderQty, ProductID, UnitPrice)
        VALUES(@SalesOrderID, @OrderQty, @ProductID, @UnitPrice);
    END
    ELSE
    BEGIN
        INSERT INTO SalesLT.SalesOrderDetail(SalesOrderID, OrderQty, ProductID, UnitPrice, UnitPriceDiscount)
        VALUES(@SalesOrderID, @OrderQty, @ProductID, @UnitPrice, @UnitPriceDiscount);
    END

    SET @SalesOrderDetailID = SCOPE_IDENTITY();

    IF @SalesOrderDetailID IS NULL
    BEGIN
        SET @ErrorMessage = 'Failed to create new sales order detail.';
        RETURN;
    END
END