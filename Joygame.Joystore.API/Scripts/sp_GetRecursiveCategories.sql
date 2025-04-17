CREATE OR ALTER PROCEDURE sp_GetRecursiveCategories
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
