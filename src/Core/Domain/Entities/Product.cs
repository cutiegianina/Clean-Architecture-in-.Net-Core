using Domain.Entities.ValueObjects;

namespace Domain.Entities;

public partial class Product : AuditableEntity
{
	public ProductId Id { get; private set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal? Price { get; set; }
	public int? StockQuantity { get; set; }
	public CategoryId CategoryId { get; init; }
}

public partial class Product
{
	public Category? Category { get; set; }
}