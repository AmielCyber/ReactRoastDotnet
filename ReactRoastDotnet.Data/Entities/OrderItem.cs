using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

[PrimaryKey(nameof(OrderId), nameof(ProductItemId))]
public class OrderItem
{
    // Belongs to one order.
    public int OrderId { get; set; }

    // Has one product item.
    [Required] public int ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; } = null!;

    [Required] public int Quantity { get; set; }

    [Required] public decimal Price { get; set; }
}