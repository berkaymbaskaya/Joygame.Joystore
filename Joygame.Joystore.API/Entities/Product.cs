using System;
using System.Collections.Generic;

namespace Joygame.Joystore.API.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CatId { get; set; }

    public string? ImageUrl { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public string? Description { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedUser { get; set; }

    public virtual Category Cat { get; set; } = null!;
}
