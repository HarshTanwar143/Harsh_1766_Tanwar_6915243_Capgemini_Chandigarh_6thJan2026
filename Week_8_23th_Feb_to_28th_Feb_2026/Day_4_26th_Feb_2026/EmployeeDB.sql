create database EmployeeDB;
use [EmployeeDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create table [dbo].[Address]
(
[AddressID] [int] IDENTITY(1,1) NOT NULL,
[Street] [varchar](255) NULL,
[City] [varchar](100) NULL,
[State] [varchar](100) NULL,
[PostalCode] [varchar](20) NULL
);


INSERT INTO [dbo].[Address] (Street, City, State, PostalCode)
VALUES
('1234 Elm Street', 'Springfield', 'Bros', '62704'),
('5678 Oak Street', 'Decatur', 'Alabama', '35601'),
('123 Patia', 'BBSR', 'India', '755019'),
('123 Patia', 'BBSR', 'India', '755019');
GO
CREATE TABLE [dbo].[Employee]
(
    [EmployeeID] INT IDENTITY(1,1) NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(150) NOT NULL,
    [AddressID] INT NOT NULL,
);




SET IDENTITY_INSERT dbo.Employee ON;

INSERT INTO [dbo].[Employee]
(EmployeeID, FirstName, LastName, Email, AddressID)
VALUES
(1, 'John', 'Doe', 'johndoe@example.com', 1),
(2, 'Jane', 'Doe', 'janedoe@example.com', 2),
(4, 'Ramesh', 'Sharma', 'Ramesh@example.com', 4),
(5, 'Ramesh', 'Sharma', 'Ramesh@example.com', 5);

SET IDENTITY_INSERT dbo.Employee OFF;

Select TOP (1000) [EmployeeID]
,[FirstName]
,[LastName]
,[Email]
,[AddressID]
From [EmployeeDB].[dbo].[Employee]


-- procedure
GO
CREATE PROCEDURE [dbo].[CreateEmployeeWithAddress]
@FirstName VARCHAR(100),
@LastName VARCHAR(100),
@Email VARCHAR(100),
@Street VARCHAR(255),
@City VARCHAR(100),
@State VARCHAR(100),
@PostalCode VARCHAR(20)
AS
BEGIN
DECLARE @AddressID INT;

INSERT INTO [dbo].[Address] (Street, City, State, PostalCode)
VALUES (@Street, @City, @State, @PostalCode);

SET @AddressID = SCOPE_IDENTITY();

INSERT INTO [dbo].[Employee] (FirstName, LastName, Email, AddressID)
VALUES (@FirstName, @LastName, @Email, @AddressID);

END;

-- delete
GO
CREATE PROCEDURE [dbo].[DeleteEmployee]
    @EmployeeID INT
AS
BEGIN
    DECLARE @AddressID INT;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Get AddressID
        SELECT @AddressID = AddressID
        FROM Employee
        WHERE EmployeeID = @EmployeeID;

        -- Delete Employee
        DELETE FROM Employee
        WHERE EmployeeID = @EmployeeID;

        -- Delete Address
        DELETE FROM Address
        WHERE AddressID = @AddressID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH
END;
GO

-- getall
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllEmployees]
AS
BEGIN
    SELECT 
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        e.Email,
        a.Street,
        a.City,
        a.State,
        a.PostalCode
    FROM Employee e
    INNER JOIN Address a 
        ON e.AddressID = a.AddressID;
END
GO


-- getempid
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEmployeeByID]
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        e.Email,
        a.Street,
        a.City,
        a.State,
        a.PostalCode
    FROM Employee e
    INNER JOIN Address a 
        ON e.AddressID = a.AddressID
    WHERE e.EmployeeID = @EmployeeID;
END
GO



-- update
GO
CREATE PROCEDURE [dbo].[UpdateEmployeeWithAddress]
    @EmployeeID INT,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(100),
    @Street VARCHAR(255),
    @City VARCHAR(100),
    @State VARCHAR(100),
    @PostalCode VARCHAR(20),
    @AddressID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Update Address
        UPDATE Address
        SET Street = @Street,
            City = @City,
            State = @State,
            PostalCode = @PostalCode
        WHERE AddressID = @AddressID;

        -- Update Employee
        UPDATE Employee
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            AddressID = @AddressID
        WHERE EmployeeID = @EmployeeID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH
END;
GO

EXEC dbo.CreateEmployeeWithAddress
    @FirstName = 'Aman',
    @LastName = 'Verma',
    @Email = 'aman@example.com',
    @Street = '45 Sector 17',
    @City = 'Chandigarh',
    @State = 'Punjab',
    @PostalCode = '160017';
GO

-- Get All Employees
EXEC dbo.GetAllEmployees;
GO

-- Get Employee By ID
EXEC dbo.GetEmployeeByID 
    @EmployeeID = 1;
GO

EXEC UpdateEmployeeWithAddress
    @EmployeeID = 1,
    @FirstName = 'Sejal',
    @LastName = 'Sharma',
    @Email = 'sejal123@email.com',
    @Street = 'Old Street',
    @City = 'Delhi',
    @State = 'Delhi',
    @PostalCode = '110011',
    @AddressID = 1;
GO

-- Delete Employee
EXEC dbo.DeleteEmployee 
    @EmployeeID = 1;
GO

select * from Address;
select * from Employee;

USE EmployeeDB;
GO

IF OBJECT_ID('dbo.UpdateEmployeeWithAddress', 'P') IS NOT NULL
    DROP PROCEDURE dbo.UpdateEmployeeWithAddress;
GO

CREATE PROCEDURE dbo.UpdateEmployeeWithAddress
    @EmployeeID INT,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(100),
    @Street VARCHAR(255),
    @City VARCHAR(100),
    @State VARCHAR(100),
    @PostalCode VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AddressID INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @AddressID = AddressID
        FROM Employee
        WHERE EmployeeID = @EmployeeID;

        UPDATE Address
        SET Street = @Street,
            City = @City,
            State = @State,
            PostalCode = @PostalCode
        WHERE AddressID = @AddressID;

        UPDATE Employee
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email
        WHERE EmployeeID = @EmployeeID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO