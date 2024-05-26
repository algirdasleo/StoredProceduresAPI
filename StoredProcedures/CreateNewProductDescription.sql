CREATE PROCEDURE CreateNewProductDescription
    @ProductDescription NVARCHAR(400)
AS
BEGIN   
    INSERT INTO SalesLT.ProductDescription(Description)
    VALUES(@ProductDescription);
END
