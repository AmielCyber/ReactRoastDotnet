using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<List<ProductItem>>> GetProducts(string? orderBy)
    {
        var query = _context.ProductItems.AsQueryable();
        query = orderBy switch
        {
            "price" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderBy(p => p.Id)
        };
        return await query.ToListAsync();
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