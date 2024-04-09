using Sales.Domain.Models;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.ProductAggregate.Entities;

public sealed class ProductInventory : Entity<ProductInventoryId>
{
    public decimal? UnitsInStock{ get; private set; }
    public decimal? UnitsOnOrder{ get; private set; }
    public decimal? ReorderLevel{ get; private set; }
    public string Location { get; private set; }

    private ProductInventory(ProductInventoryId inventoryId, 
                            decimal? unitsInStock, 
                            decimal? unitsOnOrder, 
                            decimal? reorderLevel,
                            string location) :
        base(inventoryId)
    {
        if (unitsInStock < 0)
        {
            throw new ArgumentException("UnitsInStock cannot be less than 0", nameof(unitsInStock));
        }

        if (reorderLevel < 0)
        {
            throw new ArgumentException("Reorder threshold cannot be less than 0", nameof(reorderLevel));
        }

        UnitsInStock = unitsInStock;
        UnitsOnOrder = unitsOnOrder;
        ReorderLevel = reorderLevel;
        Location = location;
    }

    public static ProductInventory Create(decimal unitsInStock, 
                                          decimal unitsOnOrder, 
                                          decimal reorderLevel,
                                          string location)
    {
        return new ProductInventory(
            ProductInventoryId.CreateUnique(),
            unitsInStock,
            unitsOnOrder,
            reorderLevel,
            location);
    }

    public static ProductInventory Create(
        ProductInventoryId inventoryId, 
        decimal unitsInStock, 
        decimal unitsOnOrder, 
        decimal reorderLevel,
        string location)
    {
        return new ProductInventory(
            inventoryId,
            unitsInStock,
            unitsOnOrder,
            reorderLevel,
            location);
    }

    // Update UnitsInStock
    public void UpdateUnitsInStock(decimal unitsInStock, decimal unitsOnOrder)
    {
        if (unitsInStock < 0)
        {
            throw new ArgumentException("UnitsInStock cannot be less than 0", nameof(unitsInStock));
        }

        UnitsInStock = unitsInStock;
        UnitsOnOrder = unitsOnOrder;
    }
}
