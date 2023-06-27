using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactRoastDotnet.Data.Entities;

public class Cart
{
    // Has one user.
    [Key] public int UserId { get; set; }
    public User? User { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public DateTime DateCreated { get; set; }

    // Relationships.

    // Has many cart items.
    public List<CartItem> Items { get; set; } = new();
    
    public void AddItem(ProductItem productItem, int quantity)
    {
        CartItem? existingCartItem = Items.FirstOrDefault(cartItem => cartItem.ProductItemId == productItem.Id);
        
        if (existingCartItem is null)
        {
            Items.Add(new CartItem
            {
                ProductItem = productItem,
                Quantity = quantity
            });
        }
        else
        {
            existingCartItem.Quantity += quantity;
        }
    }

    public void RemoveItem(int productItemId, int quantity)
    {
        CartItem? existingCartItem = Items.FirstOrDefault(cartItem => cartItem.ProductItemId == productItemId);

        if (existingCartItem is null) return;
        
        existingCartItem.Quantity -= quantity;
        if (existingCartItem.Quantity <= 0)
        {
            Items.Remove(existingCartItem);
        }

    }
}