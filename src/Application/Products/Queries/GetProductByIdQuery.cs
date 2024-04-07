using Application.Common.Interfaces.Data;
using Application.Dtos;
using Domain.Entities.ValueObjects;
using MediatR;
using System;

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

		return new ProductDto()
		{
			Id = product.Id.Value,
			Description = product.Description,
			Price = product.Price,
			StockQuantity = product.StockQuantity,
			CategoryId = product.CategoryId.Value
		};
	}
}
