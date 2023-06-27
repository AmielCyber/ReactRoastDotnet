using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

[PrimaryKey(nameof(CartId), nameof(ProductItemId))]
public class CartItem
{
    // Belongs to one Cart.
    [Required] public int CartId { get; set; }

    // Has one product item.
    [Required] public int ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; } = null!;

    [Required] public int Quantity { get; set; }
}