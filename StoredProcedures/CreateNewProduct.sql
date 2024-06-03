CREATE PROCEDURE CreateNewProduct
    @Name DBO.NAME,
    @ProductNumber NVARCHAR(25),
    @ProductModelID INT, -- optional parameter
    @ProductCategoryID INT, -- optional parameter
    @StandardCost MONEY,
    @ListPrice MONEY,
    @SellStartDate DATETIME
AS
BEGIN
    IF @ProductModelID IS NOT NULL
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM SalesLT.ProductModel WHERE ProductModelID = @ProductModelID)
        BEGIN
            RETURN -2;
        END
    END

    IF @ProductCategoryID IS NOT NULL
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM SalesLT.ProductCategory WHERE ProductCategoryID = @ProductCategoryID)
        BEGIN
            RETURN -3;
        END
    END

    INSERT INTO SalesLT.Product (Name, ProductNumber, ProductModelID, ProductCategoryID, StandardCost, ListPrice, SellStartDate)
    VALUES (@Name, @ProductNumber, @ProductModelID, @ProductCategoryID, @StandardCost, @ListPrice, @SellStartDate);
END
