using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
	public void Configure(EntityTypeBuilder<Gender> builder)
	{
		builder
			.ToTable("Genders");
	}
}