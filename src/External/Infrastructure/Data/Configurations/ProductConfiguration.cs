using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder
			.ToTable("Products");

		builder
			.HasKey(p => p.Id);

		builder
			.Property(p => p.Id)
			.IsRequired()
			.ValueGeneratedOnAdd();

		builder
			.Property(p => p.Id)
			.HasConversion(
				id => id.Value,
				value => new ProductId(value));

		builder
			.Property(p => p.Price)
			.HasColumnType("decimal(18, 2)");

		builder
			.HasOne(p => p.Category)
			.WithMany()
			.HasForeignKey(p => p.CategoryId);

		builder
			.Property(p => p.UserId)
			.HasConversion(
				id => id.Value,
				value => new UserId(value));
	}
}
