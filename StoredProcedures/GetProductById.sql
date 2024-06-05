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
        p.Name,
        p.ProductNumber,
        p.ProductModelID,
        p.ProductCategoryID,
        pd.Description,
        p.StandardCost,
        p.ListPrice,
        p.SellStartDate
    FROM SalesLT.Product p
    JOIN SalesLT.ProductModelProductDescription pmpd ON p.ProductModelID = pmpd.ProductModelID
    JOIN SalesLT.ProductDescription pd ON pmpd.ProductDescriptionID = pd.ProductDescriptionID
    WHERE ProductID = @ProductID;
END