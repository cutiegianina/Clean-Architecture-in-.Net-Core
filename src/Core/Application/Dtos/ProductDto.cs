﻿namespace Application.Dtos;

public class ProductDto
{
	public Guid? Id { get; set; }
	public Guid? UserId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal? Price { get; set; }
	public int? Quantity { get; set; }
	public Guid CategoryId { get; set; }
	public CategoryDto? Category { get; set; }
}