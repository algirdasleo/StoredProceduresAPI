CREATE PROCEDURE CreateNewSalesOrderDetail
    @SalesOrderID INT,
    @OrderQty INT,
    @ProductID INT,
    @UnitPrice DECIMAL(8,2)
AS
BEGIN
    INSERT INTO SalesLT.SalesOrderDetail(SalesOrderID, OrderQty, ProductID, UnitPrice)
    VALUES(@SalesOrderID, @OrderQty, @ProductID, @UnitPrice);
END