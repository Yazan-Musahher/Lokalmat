using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using server.Models.AuthModule;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetProducts(
        string search = null, 
        string city = null, 
        decimal? maxPrice = null, 
        int? minPopularity = null, 
        double? minRating = null)
    {
        var query = _context.Products
            .Select(p => new {
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.Category,
                p.ImageUrl,
                p.Stock,
                p.Popularity,
                p.Rating,
                p.RatingCount,
                p.City,
                p.PostalCode,
                ManufacturerName = _context.Users.Where(u => u.Id == p.ManufacturerId).Select(u => u.Name).FirstOrDefault()
            });

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
        }

        if (!string.IsNullOrEmpty(city))
       {
        var cities = city.Split(',').Select(c => c.Trim()).ToList();
        query = query.Where(p => cities.Contains(p.City));
       }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        if (minPopularity.HasValue)
        {
            query = query.Where(p => p.Popularity >= minPopularity.Value);
        }

        if (minRating.HasValue)
        {
            query = query.Where(p => p.Rating >= minRating.Value);
        }

        query = query.OrderByDescending(p => p.Popularity).ThenByDescending(p => p.Rating);

        var products = await query.ToListAsync();
        return Ok(products);
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<dynamic>> GetProduct(Guid id)
    {
        var product = await _context.Products
            .Where(p => p.Id == id)
            .Select(p => new {
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.Category,
                p.ImageUrl,
                p.Stock,
                p.Popularity,
                p.Rating,
                p.RatingCount,
                p.City,
                p.PostalCode,
                ManufacturerName = _context.Users.Where(u => u.Id == p.ManufacturerId).Select(u => u.Name).FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct([FromBody] Product product, [FromQuery] string manufacturerEmail)
    {
        var manufacturer = await _context.Users
            .Where(u => u.Email == manufacturerEmail && u.UserType == "Manufacturer")
            .FirstOrDefaultAsync();

        if (manufacturer == null)
        {
            return BadRequest("Invalid manufacturer email or the specified user is not a manufacturer.");
        }

        product.ManufacturerId = manufacturer.Id;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/Product/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(Guid id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Product/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/Product/cities
    [HttpGet("cities")]
    public async Task<ActionResult<IEnumerable<string>>> GetCities()
    {
        var cities = await _context.Products
            .Where(p => p.City != null)
            .Select(p => p.City)
            .Distinct()
            .ToListAsync();

        return cities;
    }

    // GET: api/Product/price?minPrice=100&maxPrice=200
    [HttpGet("price")]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetProductsByPriceRange([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
    {
        var query = _context.Products
            .Select(p => new {
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.Category,
                p.ImageUrl,
                p.Stock,
                p.Popularity,
                p.Rating,
                p.RatingCount,
                p.City,
                p.PostalCode,
                ManufacturerName = _context.Users.Where(u => u.Id == p.ManufacturerId).Select(u => u.Name).FirstOrDefault()
            })
            .AsQueryable();

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        var products = await query.ToListAsync();
        if (!products.Any())
        {
            return NotFound("No products found within the specified price range.");
        }

        return Ok(products);
    }

    private bool ProductExists(Guid id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
