public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Stock { get; set; }

    // Nye felt for filtrering og søk
    public int Popularity { get; set; } // Antall salg eller visninger
    public double Rating { get; set; } // Gjennomsnittlig rangering
    public int RatingCount { get; set; } // Antall anmeldelser som gjennomsnittet er basert på

    // Felt for sted
    public string City { get; set; } // By
    public int PostalCode { get; set; } // Postnummer
}