using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder
	.		HasKey(p => p.Id);

		builder
			.Property(p => p.Id)
			.IsRequired()
			.ValueGeneratedOnAdd();

		builder
			.Property(p => p.Id)
			.HasConversion(
				id => id.Value,
				value => new CustomerId(value));
	}
}