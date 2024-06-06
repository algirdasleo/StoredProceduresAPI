CREATE PROCEDURE CreateNewProduct
    @Name DBO.NAME,
    @ProductNumber NVARCHAR(25),
    @ProductModelID INT = NULL,                            -- optional parameter, leave null to create new product model
    @ProductModelName NVARCHAR(50) = NULL,                 -- optional parameter
    @ProductCategoryID INT = NULL,                         -- optional parameter, leave null to create new product category
    @ProductCategoryName NVARCHAR(50) = NULL,              -- optional parameter
    @ProductDescription NVARCHAR(4000) = NULL,             -- optional parameter
    @Culture NVARCHAR(6) = NULL,                           -- optional parameter
    @StandardCost MONEY,
    @ListPrice MONEY,
    @SellStartDate DATETIME,
    @ErrorMessage NVARCHAR(4000) OUTPUT 
AS
BEGIN
    BEGIN TRY

        BEGIN TRANSACTION;

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

        DECLARE @ProductDescriptionID INT;
        IF @ProductDescription IS NOT NULL
        BEGIN
            IF NOT EXISTS (SELECT ProductDescriptionID FROM SalesLT.ProductModelProductDescription WHERE ProductModelID = @ProductModelID)
            BEGIN
                INSERT INTO SalesLT.ProductDescription (Description)
                VALUES (@ProductDescription);
                SET @ProductDescriptionID = SCOPE_IDENTITY();
            END
        END
        ELSE IF @ProductDescription IS NOT NULL
        BEGIN
            INSERT INTO SalesLT.ProductDescription (Description)
            VALUES (@ProductDescription);
            SET @ProductDescriptionID = SCOPE_IDENTITY();
        END

        IF @ProductModelID IS NULL AND @ProductModelName IS NOT NULL
        BEGIN
            INSERT INTO SalesLT.ProductModel (Name)
            VALUES (@ProductModelName);
            SET @ProductModelID = SCOPE_IDENTITY();
        END

        IF @ProductModelID IS NOT NULL AND @ProductDescriptionID IS NOT NULL AND @Culture IS NOT NULL
        BEGIN
            INSERT INTO SalesLT.ProductModelProductDescription (ProductModelID, ProductDescriptionID, Culture)
            VALUES (@ProductModelID, @ProductDescriptionID, @Culture);
        END

        IF @ProductCategoryName IS NOT NULL
        BEGIN
            INSERT INTO SalesLT.ProductCategory(Name)
            VALUES (@ProductCategoryName);
            SET @ProductCategoryID = SCOPE_IDENTITY();
        END

        INSERT INTO SalesLT.Product (Name, ProductNumber, ProductModelID, ProductCategoryID, StandardCost, ListPrice, SellStartDate)
        VALUES (@Name, @ProductNumber, @ProductModelID, @ProductCategoryID, @StandardCost, @ListPrice, @SellStartDate);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @ErrorMessage = ERROR_MESSAGE();
    END CATCH
END