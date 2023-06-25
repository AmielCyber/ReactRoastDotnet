using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

[PrimaryKey(nameof(OrderId), nameof(ProductItemId))]
public class OrderItem
{
    // Belongs to one order.
    [Required] public int OrderId { get; set; }

    // Has one product item.
    [Required] public int ProductItemId { get; set; }
    public virtual ProductItem? ProductItem { get; set; }

    [Required] public int Quantity { get; set; }

    [Required] public decimal Price { get; set; }
}