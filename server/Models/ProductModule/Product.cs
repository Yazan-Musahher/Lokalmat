using server.Models.AuthModule;
using System.Text.Json.Serialization;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Stock { get; set; }

    // New fields for filtering and search
    public int Popularity { get; set; } // Number of sales or views
    public double Rating { get; set; } // Average rating
    public int RatingCount { get; set; } // Number of reviews that the average is based on

    // Field for location
    public string City { get; set; } // City
    public int PostalCode { get; set; } // Postal code

    // Reference to the manufacturer who creates the product
    public string ManufacturerId { get; set; } // Foreign key to ApplicationUser who is the manufacturer
    
}
