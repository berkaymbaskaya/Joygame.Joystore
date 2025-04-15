IF DB_ID('JoystoreDb') IS NULL
BEGIN
    CREATE DATABASE JoystoreDb;
END
GO

USE JoystoreDb;
GO

-- ROLES 
IF OBJECT_ID('Roles', 'U') IS NULL
BEGIN
    CREATE TABLE Roles (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(50) NOT NULL UNIQUE,
        IsActive BIT DEFAULT 1,
        IsDeleted BIT DEFAULT 0,
        DeletedAt DATETIME NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME NULL,
        CreatedUser INT NULL
    );
END
GO

-- USERS 
IF OBJECT_ID('Users', 'U') IS NULL
BEGIN
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) NOT NULL,
        PasswordHash NVARCHAR(200) NOT NULL,
        Email NVARCHAR(100) UNIQUE,
        IsActive BIT DEFAULT 1,
        IsDeleted BIT DEFAULT 0,
        DeletedAt DATETIME NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME NULL,
        CreatedUser INT NULL
    );
END
GO

-- USERROLES 
IF OBJECT_ID('UserRoles', 'U') IS NULL
BEGIN
    CREATE TABLE UserRoles (
        UserId INT NOT NULL,
        RoleId INT NOT NULL,
        PRIMARY KEY (UserId, RoleId),
        IsActive BIT DEFAULT 1,
        IsDeleted BIT DEFAULT 0,
        DeletedAt DATETIME NULL,
        DeletedUser INT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME NULL,
        CreatedUser INT NULL,
        CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
        CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id)
    );
END
GO


-- CATEGORIES
IF OBJECT_ID('Categories', 'U') IS NULL
BEGIN
    CREATE TABLE Categories (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        ParentId INT NULL,
        IsActive BIT DEFAULT 1,
        IsDeleted BIT DEFAULT 0,
        DeletedAt DATETIME NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME NULL,
        CreatedUser INT NULL
    );
END
GO

-- CATEGORIES FOREIGN KEY
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys WHERE name = 'FK_Categories_Parent'
)
BEGIN
    ALTER TABLE Categories
    ADD CONSTRAINT FK_Categories_Parent FOREIGN KEY (ParentId) REFERENCES Categories(Id);
END
GO

-- PRODUCTS
IF OBJECT_ID('Products', 'U') IS NULL
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        CatId INT NOT NULL,
        ImageUrl NVARCHAR(200),
        Price DECIMAL(18, 2),
        IsActive BIT,
        Description NVARCHAR(MAX),
        IsActive BIT DEFAULT 1,
        IsDeleted BIT DEFAULT 0,
        DeletedAt DATETIME NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME NULL,
        CreatedUser INT NULL
    );
END
GO

-- PRODUCTS FOREIGN KEY
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys WHERE name = 'FK_Products_Categories'
)
BEGIN
    ALTER TABLE Products
    ADD CONSTRAINT FK_Products_Categories FOREIGN KEY (CatId) REFERENCES Categories(Id);
END
GO
