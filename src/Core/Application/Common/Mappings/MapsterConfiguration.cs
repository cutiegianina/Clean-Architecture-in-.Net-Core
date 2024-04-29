using Application.Dtos;
using Domain.Entities;
using Domain.Entities.ValueObjects;
using Mapster;

namespace Application.Common.Mappings;

public static class MapsterConfiguration
{
	public static void ConfigureMappings()
	{
		TypeAdapterConfig<Product, ProductDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id.Value)
			.Map(des => des.UserId, src => src.UserId.Value)
			.Map(dest => dest.CategoryId, src => src.CategoryId.Value);

		TypeAdapterConfig<ProductDto, Product>.NewConfig()
			.Map(dest => dest.Id, src => src.Id)
			.Map(des => des.UserId, src => src.UserId)
			.Map(dest => dest.CategoryId, src => new CategoryId(src.CategoryId));

		TypeAdapterConfig<Category, CategoryDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id.Value);

		TypeAdapterConfig<User, UserDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id.Value)
			.Ignore(dest => dest.Password);

		TypeAdapterConfig<UserDto, User>.NewConfig()
			.Ignore(dest => dest.Id)
			.Ignore(dest => dest.Salt)
			.Ignore(dest => dest.Iterations);
	}
}