using Domain.Entities.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Products")]
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
