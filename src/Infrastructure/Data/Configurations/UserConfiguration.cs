using Domain.Entities;
using Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder
			.ToTable("Users");

		builder
			.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.IsRequired()
			.ValueGeneratedOnAdd();

		builder
			.Property(p => p.Id)
			.HasConversion(
				p => p.Value,
				value => new UserId(value));
	}
}