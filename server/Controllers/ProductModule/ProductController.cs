using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
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
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
        string search = null, 
        string city = null, 
        decimal? maxPrice = null, 
        int? minPopularity = null, 
        double? minRating = null)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            // Søk etter produktnavn eller beskrivelse
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
        }

        if (!string.IsNullOrEmpty(city))
        {
            // Filtrer etter by
            query = query.Where(p => p.City == city);
        }

        if (maxPrice.HasValue)
        {
            // Filtrer etter makspris
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        if (minPopularity.HasValue)
        {
            // Filtrer etter minimum popularitet
            query = query.Where(p => p.Popularity >= minPopularity.Value);
        }

        if (minRating.HasValue)
        {
            // Filtrer etter minimum rangering
            query = query.Where(p => p.Rating >= minRating.Value);
        }

        // Sorter etter popularitet og rangering som eksempel
        query = query.OrderByDescending(p => p.Popularity).ThenByDescending(p => p.Rating);

        return await query.ToListAsync();
    }


    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
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

    private bool ProductExists(Guid id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
