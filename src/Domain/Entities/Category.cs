﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("Categories")]
public class Category : AuditableEntity
{
	[Key]
	[Required]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public List<Product>? Products { get; set; }
}
