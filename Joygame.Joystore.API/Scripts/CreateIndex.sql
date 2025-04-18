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
