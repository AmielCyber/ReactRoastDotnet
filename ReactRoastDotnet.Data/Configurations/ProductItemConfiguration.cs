using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReactRoastDotnet.Data.Entities;

namespace ReactRoastDotnet.Data.Configurations;

public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        const string drinkType = "Drink";

        builder.HasData(
            new ProductItem
            {
                Id = 1,
                Type = drinkType,
                Name = "Latte",
                Ounces = 10.0,
                Description =
                    "The most popular espresso drink. For those that love milk in their drinks. Made of 2 ounces of espresso and 8 ounces of steamed milk.",
                Price = 5.0m,
                Image = "/drinkImages/latte.jpg",
                ImageCreator = "@taisiia_shestopal"
            },
            new ProductItem
            {
                Id = 2,
                Type = drinkType,
                Name = "Iced Latte",
                Ounces = 10.0,
                Description =
                    "Hot espresso drinks may not be for everyone. For everyone else there is always an Iced Latte!",
                Price = 5.0m,
                Image = "/drinkImages/icedLatte.jpg",
                ImageCreator = "@pariwatt"
            },
            new ProductItem
            {
                Id = 3,
                Type = drinkType,
                Name = "Nitro Cold Brew",
                Ounces = 8.0,
                Description = "Cold brew infused with nitro giving it a creamy texture.",
                Price = 4.5m,
                Image = "/drinkImages/nitroBrew.jpg",
                ImageCreator = "@schimiggy"
            },
            new ProductItem
            {
                Id = 4,
                Type = drinkType,
                Name = "Cold Brew",
                Ounces = 8.0,
                Description = "Coffee steeped in cold water giving it a less acidic signature and more caffeine.",
                Price = 4.0m,
                Image = "/drinkImages/coldBrew.jpg",
                ImageCreator = "@pariwatt"
            },
            new ProductItem
            {
                Id = 5,
                Type = drinkType,
                Name = "Drip Coffee",
                Ounces = 8.0,
                Description = "For those that just want a cup of hot coffee!",
                Price = 3.5m,
                Image = "/drinkImages/dripCoffee.jpg",
                ImageCreator = "@andrewtneel"
            },
            new ProductItem
            {
                Id = 6,
                Type = drinkType,
                Name = "Americano",
                Ounces = 6.0,
                Description =
                    "For those who want a plain coffee drink but with the taste of espresso. Made of 2 ounces of espresso and 4 ounces of hot water.",
                Price = 4.0m,
                Image = "/drinkImages/americano.jpg",
                ImageCreator = "@cdib925"
            },
            new ProductItem
            {
                Id = 7,
                Type = drinkType,
                Name = "Cappuccino",
                Ounces = 6.0,
                Description =
                    "For those that prefer more of the milk foam texture. Made of 2 ounces of espresso, 2 ounces of milk foam, and 2 ounces of steamed milk.",
                Price = 4.5m,
                Image = "/drinkImages/cappuccino.jpg",
                ImageCreator = "@nadyeldems"
            },
            new ProductItem
            {
                Id = 8,
                Type = drinkType,
                Name = "Espresso Shot",
                Ounces = 1.0,
                Description = "For the coffee connoisseur.",
                Price = 3.5m,
                Image = "/drinkImages/espresso.jpg",
                ImageCreator = "@nate_dumlao"
            },
            new ProductItem
            {
                Id = 9,
                Type = drinkType,
                Name = "Cortado",
                Ounces = 3.0,
                Description =
                    "For those who want a plain coffee drink but with the taste of espresso. Made of 2 ounces of espresso and 4 ounces of hot water.",
                Price = 3.75m,
                Image = "/drinkImages/cortado.jpg",
                ImageCreator = "@relentlessjpg"
            },
            new ProductItem
            {
                Id = 10,
                Type = drinkType,
                Name = "Macchiato",
                Ounces = 3.0,
                Description =
                    "For those that want to taste our espresso with a whip of cream. Made of 2 ounces of espresso and 1 ounce of milk foam.",
                Price = 3.75m,
                Image = "/drinkImages/macchiato.jpg",
                ImageCreator = "@13on"
            },
            new ProductItem
            {
                Id = 11,
                Type = drinkType,
                Name = "Flat White",
                Ounces = 6.0,
                Description =
                    "For those who wished the latte had more of an espresso taste. Made of 2 ounces of espresso and 4 ounces of steam milk.",
                Price = 4.5m,
                Image = "/drinkImages/flatWhite.jpg",
                ImageCreator = "@hoanvokim"
            }
        );
    }
}