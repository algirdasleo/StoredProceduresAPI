CREATE PROCEDURE GetProductById
    @ProductID INT,
    @ErrorMessage NVARCHAR(4000) OUTPUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM SalesLT.Product WHERE ProductID = @ProductID)
    BEGIN
        SET @ErrorMessage = 'Product not found.';
        RETURN;
    END

    SELECT
        ProductID,
        Name,
        ProductNumber,
        ProductModelID,
        ProductCategoryID,
        StandardCost,
        ListPrice,
        SellStartDate
    FROM SalesLT.Product
    WHERE ProductID = @ProductID;
END