CREATE PROCEDURE CreateNewProductCategory
    @Name NVARCHAR(50)
AS
BEGIN
    INSERT SalesLT.ProductCategory (Name)
    Values (@Name)
END