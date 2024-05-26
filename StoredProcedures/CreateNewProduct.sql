CREATE PROCEDURE CreateNewProduct
    @Name DBO.NAME,
    @ProductNumber NVARCHAR(25),
    @StandardCost MONEY,
    @ListPrice MONEY,
    @SellStartDate DATETIME
AS
BEGIN
    INSERT INTO SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate)
    VALUES (@Name, @ProductNumber, @StandardCost, @ListPrice, @SellStartDate)
END
