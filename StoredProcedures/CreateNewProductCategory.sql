CREATE PROCEDURE CreateNewProductCategory
    @Name NVARCHAR(50),
    @ErrorMessage NVARCHAR(4000) OUTPUT
AS
BEGIN
    DECLARE @ProductCategoryID INT;
    INSERT SalesLT.ProductCategory (Name)
    Values (@Name)
    SET @ProductCategoryID = SCOPE_IDENTITY();
    
    IF @ProductCategoryID IS NULL
        SET @ErrorMessage = 'Failed to create new product category.';
END