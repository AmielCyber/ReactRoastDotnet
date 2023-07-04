using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.Order;

namespace ReactRoastDotnet.Data.Extensions;

// TODO: Add filter for product types when we add other types rather than "Drinks"
public static class ProductItemsExtension
{
    public static IQueryable<ProductItem> Sort(this IQueryable<ProductItem> query, string? sortBy)
    {
        // SQLite does not support sorting by decimal type.
        query = sortBy switch
        {
            "name" => query.OrderBy(p => p.Name),
            "popular" => query.OrderBy(p => p.Id),
            "price" => query.OrderBy(p => p.Price),
            // Required to avoid sql warnings.
            _ => query.OrderBy(p => p.Id)
        };

        return query;
    }

    public static IQueryable<ProductItem> SearchDrink(this IQueryable<ProductItem> query, string? drinkName)
    {
        if (string.IsNullOrEmpty(drinkName))
        {
            // Short circuit. User did requested to search drinks.
            return query;
        }

        var lowerCaseDrinkName = drinkName.Trim().ToLower();

        return query.Where(i => i.Name.ToLower().Contains(lowerCaseDrinkName));
    }

    public static void UpdateProductItem(this ProductItem productItem, EditProductDto editProductDto)
    {
        productItem.Type = editProductDto.Type;
        productItem.Name = editProductDto.Name;
        productItem.Ounces = editProductDto.Ounces;
        productItem.Description = editProductDto.Description;
        productItem.Price = editProductDto.Price;
        productItem.Image = editProductDto.Image;
        productItem.ImageCreator = editProductDto.ImageCreator;
    }
}