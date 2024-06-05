CREATE PROCEDURE CreateNewProductDescription
    @ProductDescription NVARCHAR(400),
    @ErrorMessage NVARCHAR(400) OUTPUT
AS
BEGIN   
    DECLARE @ProductDescriptionId INT;
    INSERT INTO SalesLT.ProductDescription(Description)
    VALUES(@ProductDescription);
    SET @ProductDescriptionId = SCOPE_IDENTITY();

    IF @ProductDescriptionId IS NULL
        SET @ErrorMessage = 'Failed to create new product description';
END
