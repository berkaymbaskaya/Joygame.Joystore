﻿CREATE OR ALTER PROCEDURE [dbo].[sp_GetProductsWithCategory]
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
		p.ImageUrl,
        p.Description,
		p.IsActive
    FROM Products p
    INNER JOIN Categories c ON p.CatId = c.Id
    LEFT JOIN Categories pc ON c.ParentId = pc.Id
    WHERE 
        p.IsDeleted = 0  AND
        c.IsDeleted = 0  
	ORDER BY p.Id  DESC
	OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;		
END
