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

-- PRODUCTS
IF OBJECT_ID('Products', 'U') IS NULL
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        CatId INT NOT NULL,
        ImageUrl NVARCHAR(200),
        Price DECIMAL(18, 2),
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

CREATE TABLE PasswordResetTokens (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Token NVARCHAR(100) NOT NULL,
    ExpiresAt DATETIME NOT NULL,
    IsUsed BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    DeletedAt DATETIME NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME NULL,
    CreatedUser INT NULL
);


USE JoystoreDb;
GO

-- ROLLER
INSERT INTO Roles (Name, CreatedUser)
VALUES 
('admin', NULL),
('product_view', NULL),
('product_creator', NULL),
('product_update', NULL),
('product_delete', NULL),
('category_view', NULL),
('category_creator', NULL),
('category_update', NULL);
GO

-- USERS
INSERT INTO Users (Username, PasswordHash, Email, CreatedUser)
VALUES 
('berkay', '$2a$11$BIo4d1KwZb1A.QMAGtsDn.zefd2r/89PuAAfudcFk.rwJg/zfIM2G', 'berkay@joygame.com', NULL);
GO

-- ROLE ASSIGNMENTS
INSERT INTO UserRoles (UserId, RoleId, CreatedUser)
SELECT 1, Id, NULL FROM Roles;
GO

-- Main Categories
INSERT INTO Categories (Name, ParentId, IsActive, IsDeleted, CreatedAt, CreatedUser)
VALUES 
('PC Oyunları', NULL, 1, 0, GETUTCDATE(), NULL),
('Mobil Oyunlar', NULL, 1, 0, GETUTCDATE(), NULL),
('Konsol Oyunları', NULL, 1, 0, GETUTCDATE(), NULL);

-- Sub Categories - PC Games
INSERT INTO Categories (Name, ParentId, IsActive, IsDeleted, CreatedAt, CreatedUser)
VALUES 
('FPS', (SELECT Id FROM Categories WHERE Name = 'PC Oyunları'), 1, 0, GETUTCDATE(), NULL),
('Strateji', (SELECT Id FROM Categories WHERE Name = 'PC Oyunları'), 1, 0, GETUTCDATE(), NULL),
('MMORPG', (SELECT Id FROM Categories WHERE Name = 'PC Oyunları'), 1, 0, GETUTCDATE(), NULL);

-- Sub Categories - Mobile Games
INSERT INTO Categories (Name, ParentId, IsActive, IsDeleted, CreatedAt, CreatedUser)
VALUES 
('Puzzle', (SELECT Id FROM Categories WHERE Name = 'Mobil Oyunlar'), 1, 0, GETUTCDATE(), NULL),
('Casual', (SELECT Id FROM Categories WHERE Name = 'Mobil Oyunlar'), 1, 0, GETUTCDATE(), NULL);

-- Sub Categories - Console Games
INSERT INTO Categories (Name, ParentId, IsActive, IsDeleted, CreatedAt, CreatedUser)
VALUES 
('Aksiyon', (SELECT Id FROM Categories WHERE Name = 'Konsol Oyunları'), 1, 0, GETUTCDATE(), NULL),
('Spor', (SELECT Id FROM Categories WHERE Name = 'Konsol Oyunları'), 1, 0, GETUTCDATE(), NULL);




USE JoystoreDb;
GO
-- FPS
INSERT INTO Products (Name, CatId, ImageUrl, Price, IsActive, Description, CreatedAt, CreatedUser)
VALUES
('Call of Duty: Modern Warfare', (SELECT Id FROM Categories WHERE Name = 'FPS'), NULL, 59.99, 1, 'First-person shooter', GETUTCDATE(), NULL),
('Battlefield 2042', (SELECT Id FROM Categories WHERE Name = 'FPS'), NULL, 49.99, 1, 'Multiplayer FPS war game', GETUTCDATE(), NULL),
('Valorant', (SELECT Id FROM Categories WHERE Name = 'FPS'), NULL, 0.00, 1, 'Tactical 5v5 shooter', GETUTCDATE(), NULL),
('Counter-Strike 2', (SELECT Id FROM Categories WHERE Name = 'FPS'), NULL, 0.00, 1, 'Competitive FPS classic', GETUTCDATE(), NULL),
('Rainbow Six Siege', (SELECT Id FROM Categories WHERE Name = 'FPS'), NULL, 19.99, 1, 'Strategic FPS', GETUTCDATE(), NULL);

-- Strateji
INSERT INTO Products (Name, CatId, ImageUrl, Price, IsActive, Description, CreatedAt, CreatedUser)
VALUES
('Age of Empires IV', (SELECT Id FROM Categories WHERE Name = 'Strateji'), NULL, 39.99, 1, 'Real-time historical strategy', GETUTCDATE(), NULL),
('Civilization VI', (SELECT Id FROM Categories WHERE Name = 'Strateji'), NULL, 29.99, 1, 'Turn-based strategy', GETUTCDATE(), NULL),
('StarCraft II', (SELECT Id FROM Categories WHERE Name = 'Strateji'), NULL, 0.00, 1, 'Sci-fi RTS', GETUTCDATE(), NULL),
('Company of Heroes 3', (SELECT Id FROM Categories WHERE Name = 'Strateji'), NULL, 49.99, 1, 'WWII RTS', GETUTCDATE(), NULL),
('Total War: Warhammer III', (SELECT Id FROM Categories WHERE Name = 'Strateji'), NULL, 59.99, 1, 'Grand strategy fantasy', GETUTCDATE(), NULL);

-- MMORPG
INSERT INTO Products (Name, CatId, ImageUrl, Price, IsActive, Description, CreatedAt, CreatedUser)
VALUES
('World of Warcraft', (SELECT Id FROM Categories WHERE Name = 'MMORPG'), NULL, 14.99, 1, 'Classic MMO experience', GETUTCDATE(), NULL),
('Final Fantasy XIV', (SELECT Id FROM Categories WHERE Name = 'MMORPG'), NULL, 29.99, 1, 'Story-rich MMORPG', GETUTCDATE(), NULL),
('Guild Wars 2', (SELECT Id FROM Categories WHERE Name = 'MMORPG'), NULL, 0.00, 1, 'Dynamic world MMO', GETUTCDATE(), NULL),
('The Elder Scrolls Online', (SELECT Id FROM Categories WHERE Name = 'MMORPG'), NULL, 19.99, 1, 'TES universe MMORPG', GETUTCDATE(), NULL),
('Black Desert Online', (SELECT Id FROM Categories WHERE Name = 'MMORPG'), NULL, 9.99, 1, 'Fast-action fantasy MMO', GETUTCDATE(), NULL);

-- Puzzle
INSERT INTO Products (Name, CatId, ImageUrl, Price, IsActive, Description, CreatedAt, CreatedUser)
VALUES
('Candy Crush Saga', (SELECT Id FROM Categories WHERE Name = 'Puzzle'), NULL, 0.00, 1, 'Match-3 puzzle fun', GETUTCDATE(), NULL),
('Monument Valley', (SELECT Id FROM Categories WHERE Name = 'Puzzle'), NULL, 3.99, 1, 'Surreal puzzle journey', GETUTCDATE(), NULL),
('Cut the Rope', (SELECT Id FROM Categories WHERE Name = 'Puzzle'), NULL, 0.99, 1, 'Feed the candy', GETUTCDATE(), NULL),
('The Room', (SELECT Id FROM Categories WHERE Name = 'Puzzle'), NULL, 4.99, 1, '3D puzzle mystery', GETUTCDATE(), NULL),
('Brain Test', (SELECT Id FROM Categories WHERE Name = 'Puzzle'), NULL, 0.00, 1, 'Tricky puzzles', GETUTCDATE(), NULL);

-- Casual
INSERT INTO Products (Name, CatId, ImageUrl, Price, IsActive, Description, CreatedAt, CreatedUser)
VALUES
('Subway Surfers', (SELECT Id FROM Categories WHERE Name = 'Casual'), NULL, 0.00, 1, 'Endless runner', GETUTCDATE(), NULL),
('Temple Run 2', (SELECT Id FROM Categories WHERE Name = 'Casual'), NULL, 0.00, 1, 'Fast-paced runner', GETUTCDATE(), NULL),
('My Talking Tom', (SELECT Id FROM Categories WHERE Name = 'Casual'), NULL, 0.00, 1, 'Virtual pet', GETUTCDATE(), NULL),
('8 Ball Pool', (SELECT Id FROM Categories WHERE Name = 'Casual'), NULL, 0.00, 1, 'Online billiards', GETUTCDATE(), NULL),
('Paper.io 2', (SELECT Id FROM Categories WHERE Name = 'Casual'), NULL, 0.00, 1, 'Territory battle', GETUTCDATE(), NULL);

-- Aksiyon
INSERT INTO Products (Name, CatId, ImageUrl, Price, IsActive, Description, CreatedAt, CreatedUser)
VALUES
('God of War', (SELECT Id FROM Categories WHERE Name = 'Aksiyon'), NULL, 59.99, 1, 'Mythological action', GETUTCDATE(), NULL),
('Spider-Man: Miles Morales', (SELECT Id FROM Categories WHERE Name = 'Aksiyon'), NULL, 49.99, 1, 'Marvel action game', GETUTCDATE(), NULL),
('Devil May Cry 5', (SELECT Id FROM Categories WHERE Name = 'Aksiyon'), NULL, 39.99, 1, 'Stylish hack and slash', GETUTCDATE(), NULL),
('Bayonetta', (SELECT Id FROM Categories WHERE Name = 'Aksiyon'), NULL, 19.99, 1, 'Witch-powered combat', GETUTCDATE(), NULL),
('Sekiro: Shadows Die Twice', (SELECT Id FROM Categories WHERE Name = 'Aksiyon'), NULL, 59.99, 1, 'Souls-like samurai action', GETUTCDATE(), NULL);

-- Spor
INSERT INTO Products (Name, CatId, ImageUrl, Price, IsActive, Description, CreatedAt, CreatedUser)
VALUES
('FIFA 24', (SELECT Id FROM Categories WHERE Name = 'Spor'), NULL, 69.99, 1, 'Football simulation', GETUTCDATE(), NULL),
('NBA 2K24', (SELECT Id FROM Categories WHERE Name = 'Spor'), NULL, 59.99, 1, 'Basketball simulation', GETUTCDATE(), NULL),
('Rocket League', (SELECT Id FROM Categories WHERE Name = 'Spor'), NULL, 0.00, 1, 'Car football', GETUTCDATE(), NULL),
('eFootball 2024', (SELECT Id FROM Categories WHERE Name = 'Spor'), NULL, 0.00, 1, 'Free football sim', GETUTCDATE(), NULL),
('Tony Hawk''s Pro Skater 1+2', (SELECT Id FROM Categories WHERE Name = 'Spor'), NULL, 29.99, 1, 'Skateboarding game', GETUTCDATE(), NULL);

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_GetProductsWithCategory]
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    -- Data
	SELECT 
        p.Id,
        p.Name,
		c.Id AS CategoryId,
		pc.Id AS ParentCategoryId,
		c.Name AS CategoryName,
        pc.Name AS ParentCategoryName,
        p.Price,
		p.ImageUrl
    FROM Products p
    INNER JOIN Categories c ON p.CatId = c.Id
    LEFT JOIN Categories pc ON c.ParentId = pc.Id
    WHERE 
        p.IsDeleted = 0 AND p.IsActive = 1 AND
        c.IsDeleted = 0 AND c.IsActive = 1    
	ORDER BY pc.Name, c.Name, p.Name
	OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;		
    -- Total Count
    SELECT COUNT(*) AS TotalCount
    FROM Products p
    INNER JOIN Categories c ON p.CatId = c.Id
    WHERE 
        p.IsDeleted = 0 AND p.IsActive = 1 AND
        c.IsDeleted = 0 AND c.IsActive = 1;
END

GO
CREATE OR ALTER PROCEDURE [dbo].[sp_GetRecursiveCategories]
AS
BEGIN
    SET NOCOUNT ON;

    WITH RecursiveCategory AS
    (
        SELECT 
            Id,
            Name,
            ParentId
        FROM Categories
        WHERE ParentId IS NULL AND IsActive = 1 AND IsDeleted = 0

        UNION ALL

        SELECT 
            c.Id,
            c.Name,
            c.ParentId
        FROM Categories c
        INNER JOIN RecursiveCategory rc ON c.ParentId = rc.Id
        WHERE c.IsActive = 1 AND c.IsDeleted = 0
    )
    SELECT 
        Id,
        Name,
        ParentId
    FROM RecursiveCategory
END
GO

USE JoystoreDb;
GO

CREATE NONCLUSTERED INDEX IX_Products_CatId
ON Products (CatId);
GO

CREATE NONCLUSTERED INDEX IX_Products_IsDeleted_IsActive
ON Products (IsDeleted, IsActive);
GO

CREATE NONCLUSTERED INDEX IX_Categories_ParentId
ON Categories (ParentId);
GO

CREATE NONCLUSTERED INDEX IX_Categories_IsDeleted_IsActive
ON Categories (IsDeleted, IsActive);
GO
CREATE NONCLUSTERED INDEX IX_Users_IsDeleted_IsActive
ON Users (IsDeleted, IsActive);
GO
