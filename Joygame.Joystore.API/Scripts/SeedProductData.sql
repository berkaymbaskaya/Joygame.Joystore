
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
