using Application.Dtos;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings;

public static class MapsterConfiguration
{
	public static void ConfigureMappings()
	{
		TypeAdapterConfig<Product, ProductDto>.NewConfig()
			.Map(dest => dest.Id, src => src.Id.Value)
			.Map(dest => dest.CategoryId, src => src.CategoryId.Value);
	}
}
