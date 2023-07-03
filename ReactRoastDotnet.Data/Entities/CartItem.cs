using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReactRoastDotnet.Data.Entities;

[Table("CartItems")]
[PrimaryKey(nameof(CartId), nameof(ProductItemId))]
public class CartItem
{
    // Belongs to one Cart.
    public int CartId { get; set; }

    // Has one product item.
    public int ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; } = null!;

    [Required] public int Quantity { get; set; }
}