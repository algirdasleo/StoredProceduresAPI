CREATE PROCEDURE CreateNewProduct
    @Name DBO.NAME,
    @ProductNumber NVARCHAR(25),
    @ProductModelID INT = NULL,                -- optional parameter
    @ProductCategoryID INT = NULL,             -- optional parameter
    @ProductDescription NVARCHAR(4000) = NULL, -- optional parameter
    @Culture NVARCHAR(6) = 'en',               -- optional parameter, with default value
    @StandardCost MONEY,
    @ListPrice MONEY,
    @SellStartDate DATETIME,
    @ErrorMessage NVARCHAR(4000) OUTPUT 
AS
BEGIN
    IF @ProductModelID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM SalesLT.ProductModel WHERE ProductModelID = @ProductModelID)
        SET @ErrorMessage = COALESCE(@ErrorMessage + ' ', '') + 'Invalid Product Model ID.';


    IF @ProductCategoryID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM SalesLT.ProductCategory WHERE ProductCategoryID = @ProductCategoryID)
        SET @ErrorMessage = COALESCE(@ErrorMessage + ' ', '') + 'Invalid Product Category ID.';

    IF @ProductDescription IS NOT NULL
    BEGIN
        IF EXISTS (SELECT 1 FROM SalesLT.ProductModelProductDescription WHERE ProductModelID = @ProductModelID)
        BEGIN
            IF @ErrorMessage IS NULL 
                SET @ErrorMessage = 'Product Model already has a description.';
            ELSE
                SET @ErrorMessage = @ErrorMessage + ' Product Model already has a description.';
        END
    END

    INSERT INTO SalesLT.Product (Name, ProductNumber, ProductModelID, ProductCategoryID, StandardCost, ListPrice, SellStartDate)
    VALUES (@Name, @ProductNumber, @ProductModelID, @ProductCategoryID, @StandardCost, @ListPrice, @SellStartDate);

    IF @ProductDescription IS NOT NULL
    BEGIN
        IF NOT EXISTS (SELECT ProductDescriptionID FROM SalesLT.ProductModelProductDescription WHERE ProductModelID = @ProductModelID)
        BEGIN
            DECLARE @ProductDescriptionID INT;
            INSERT INTO SalesLT.ProductDescription (Description)
            VALUES (@ProductDescription);
            SET @ProductDescriptionID = SCOPE_IDENTITY();
            INSERT INTO SalesLT.ProductModelProductDescription (ProductModelID, ProductDescriptionID, Culture)
            VALUES (@ProductModelID, @ProductDescriptionID, @Culture);
        END
    END
END