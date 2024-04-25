using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder
			.ToTable("Categories");

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
				value => new CategoryId(value));
	}
}
