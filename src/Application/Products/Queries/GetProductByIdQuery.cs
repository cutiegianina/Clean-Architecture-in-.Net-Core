using Application.Common.Interfaces.Data;
using Application.Dtos;
using Domain.Entities.ValueObjects;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;

internal sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
	private readonly IApplicationDbContext _context;

	public GetProductByIdQueryHandler(IApplicationDbContext context) =>
		_context = context;

	public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
	{
		var product = await _context.Product.FindAsync(new ProductId(request.Id), cancellationToken);

		if (product is null)
			return await Task.FromResult(new ProductDto());

		return product.Adapt<ProductDto>();
	}
}