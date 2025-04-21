CREATE OR ALTER PROCEDURE [dbo].[sp_GetRecursiveCategories]
AS
BEGIN
    SET NOCOUNT ON;

    WITH RecursiveCategory AS
    (
        SELECT 
            Id,
            Name,
            ParentId,
			CAST(NULL AS NVARCHAR(100)) AS ParentName

        FROM Categories
        WHERE ParentId IS NULL AND IsDeleted = 0

        UNION ALL

        SELECT 
            c.Id,
            c.Name,
            c.ParentId,
            rc.Name AS ParentName
        FROM Categories c
        INNER JOIN RecursiveCategory rc ON c.ParentId = rc.Id
        WHERE  c.IsDeleted = 0
    )
    SELECT 
        Id,
        Name,
        ParentId,
		ParentName
    FROM RecursiveCategory
END
