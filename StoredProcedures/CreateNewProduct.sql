CREATE PROCEDURE CreateNewProduct
    @Name DBO.NAME,
    @ProductNumber NVARCHAR(25),
    @ProductModelID INT,                -- optional parameter
    @ProductCategoryID INT,             -- optional parameter
    @ProductDescription NVARCHAR(4000), -- optional parameter
    @StandardCost MONEY,
    @ListPrice MONEY,
    @SellStartDate DATETIME,
    @ErrorMessage NVARCHAR(4000) OUTPUT 
AS
BEGIN
    IF @ProductModelID IS NOT NULL
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM SalesLT.ProductModel WHERE ProductModelID = @ProductModelID)
        BEGIN
            IF @ErrorMessage IS NULL 
                SET @ErrorMessage = 'Invalid Product Model ID.';
            ELSE
                SET @ErrorMessage = @ErrorMessage + ' Invalid Product Model ID.';
        END
    END

    IF @ProductCategoryID IS NOT NULL
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM SalesLT.ProductCategory WHERE ProductCategoryID = @ProductCategoryID)
        BEGIN
            IF @ErrorMessage IS NULL 
                SET @ErrorMessage = 'Invalid Product Category ID.';
            ELSE
                SET @ErrorMessage = @ErrorMessage + ' Invalid Product Category ID.';
        END
    END

    IF @ProductDescription IS NOT NULL
    BEGIN
        IF EXISTS (SELECT 1 FROM ProductModelProductDescription WHERE ProductModelID = @ProductModelID)
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
            INSERT INTO SalesLT.ProductModelProductDescription (ProductModelID, ProductDescriptionID)
            VALUES (@ProductModelID, @ProductDescriptionID);
        END
    END
END