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
('berkay', '$2a$11$Bf2oxcGfdiCaacBbB0EfVercdsz6CCk4klo0R4V0OrHR3Q4.fg3wS', 'berkay@joygame.com', NULL);
GO

-- ROLE ASSIGNMENTS
INSERT INTO UserRoles (UserId, RoleId, CreatedUser)
SELECT 1, Id, NULL FROM Roles;
GO

-- CATEGORIES
INSERT INTO Categories (Name, ParentId, CreatedUser)
VALUES 
('MMO', NULL, NULL),               
('FPS', 1, NULL),                 
('RPG', 1, NULL),                  
('Casual', 1, NULL);              
GO

-- PRODUCTS 
INSERT INTO Products (Name, CatId, ImageUrl, Price, Description, CreatedUser)
VALUES 
('Wolfteam', 2, 'https://placehold.co/600x400', 50.00, 'Popüler Joygame MMOFPS oyunu.', 1),
('Goley', 4, 'https://placehold.co/600x400', 45.00, 'Futbol temalı casual oyun.', 1),
('Rise of Mythos', 3, 'https://placehold.co/600x400', 40.00, 'Kart ve RPG tabanlı MMO oyunu.', 1),
('Need for Speed Online', 4, 'https://placehold.co/600x400', 25.00, 'Online araba yarışı.', 1);
GO
