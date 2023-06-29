using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.API.Extensions;
using ReactRoastDotnet.API.RequestParams;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Models.ResponseDto;

namespace ReactRoastDotnet.API.Controllers;

// TODO: Implement a ProductService
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ProductItemListDto>> GetProducts([FromQuery]ProductParams productParams)
    {
        var totalItems = await _context.ProductItems.CountAsync();
        
        var query = _context.ProductItems
            .AsNoTracking()
            .Skip((productParams.PageNumber-1)*productParams.PageSize)
            .Take(productParams.PageSize)
            .Sort(productParams.Sort)
            .SearchDrink(productParams.DrinkName)
            .AsQueryable();

        List<ProductItem> productItems = await query.ToListAsync();
        Pagination pagination = new Pagination
        {
            CurrentPage = productParams.PageNumber,
            PageSize = productParams.PageSize,
            TotalCount = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)productParams.PageSize)
        };

        return new ProductItemListDto(productItems, pagination);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductItem>> GetProduct(int id)
    {
        var productItem = await _context.ProductItems.FindAsync(id);
        if (productItem is null)
        {
            return NotFound();
        }

        return Ok(productItem);
    }
}