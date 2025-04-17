USE JoystoreDb;
GO

-- ROLLER
INSERT INTO Roles (Name, CreatedUser)
VALUES 
('admin', NULL),
('product_view', NULL),
('product_creator', NULL),
('product_update', NULL),
('category_view', NULL),
('category_creator', NULL),
('category_update', NULL);
GO

-- USERS
INSERT INTO Users (Username, PasswordHash, Email, CreatedUser)
VALUES 
('berkay', 'it-need-change-reset-password', 'berkay@joygame.com', NULL);
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

