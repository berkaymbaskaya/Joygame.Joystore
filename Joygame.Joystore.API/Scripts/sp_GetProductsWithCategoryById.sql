CREATE OR ALTER PROCEDURE [dbo].[sp_GetProductsWithCategoryById]
    @ProductId INT 
AS
BEGIN
    SET NOCOUNT ON;

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
        p.IsActive,
        p.CreatedAt,
        p.UpdatedAt
    FROM Products p
    INNER JOIN Categories c ON p.CatId = c.Id
    LEFT JOIN Categories pc ON c.ParentId = pc.Id
    WHERE 
        p.Id = @ProductId AND
        p.IsDeleted = 0  AND
        c.IsDeleted = 0     
END
