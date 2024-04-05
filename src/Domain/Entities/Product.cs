using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("Products")]
public partial class Product : AuditableEntity
{
	[Key]
	[Required]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal? Price { get; set; }
	public int? StockQuantity { get; set; }
	public int CategoryId { get; set; }
}

public partial class Product
{
	[ForeignKey("CategoryId")]
	public Category? Category { get; set; }

}
