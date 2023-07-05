using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

[Table("OrderItems")]
[PrimaryKey(nameof(OrderId), nameof(ProductItemId))]
public class OrderItem
{
    // Belongs to one order.
    public int OrderId { get; set; }

    // Has one product item.
    public int ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; } = null!;

    [Required] public int Quantity { get; set; }

    [Precision(18, 2)]
    [Required] public decimal Price { get; set; }
}