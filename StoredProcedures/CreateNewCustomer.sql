CREATE PROCEDURE CreateNewCustomer
    @FirstName dbo.Name,
    @LastName dbo.Name,
    @PasswordHash VARCHAR(128),
    @PasswordSalt VARCHAR(10),
    @AddressLine1 NVARCHAR(60), 
    @AddressLine2 NVARCHAR(60) = NULL,  -- Optional parameter
    @City NVARCHAR(30),
    @StateProvince dbo.Name,
    @CountryRegion dbo.Name,
    @PostalCode NVARCHAR(15),
    @ErrorMessage NVARCHAR(4000) OUTPUT -- Output parameter, to return an error message, if any
AS
BEGIN
    DECLARE @CustomerID INT;
    INSERT INTO SalesLT.Customer (FirstName, LastName, PasswordHash, PasswordSalt)
    VALUES (@FirstName, @LastName, @PasswordHash, @PasswordSalt);
    SET @CustomerID = SCOPE_IDENTITY();

    IF @CustomerID IS NULL
    BEGIN
        SET @ErrorMessage = 'Failed to create new customer.';
        RETURN;
    END

    DECLARE @AddressID INT;
    INSERT INTO SalesLT.Address (AddressLine1, AddressLine2, City, StateProvince, CountryRegion, PostalCode)
    VALUES (@AddressLine1, @AddressLine2, @City, @StateProvince, @CountryRegion, @PostalCode);
    SET @AddressID = SCOPE_IDENTITY();

    IF @AddressID IS NULL
    BEGIN
        SET @ErrorMessage = 'Failed to create new customer address.';
        RETURN;
    END
    
    INSERT INTO SalesLT.CustomerAddress (CustomerID, AddressID)
    VALUES (@CustomerID, @AddressID);
END