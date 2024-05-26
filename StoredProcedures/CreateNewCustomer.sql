CREATE PROCEDURE CreateNewCustomer
    @FirstName dbo.Name,
    @LastName dbo.Name,
    @PasswordHash VARCHAR(128),
    @PasswordSalt VARCHAR(10)
AS
BEGIN
    INSERT INTO SalesLT.Customer (FirstName, LastName, PasswordHash, PasswordSalt)
    VALUES (@FirstName, @LastName, @PasswordHash, @PasswordSalt);
END