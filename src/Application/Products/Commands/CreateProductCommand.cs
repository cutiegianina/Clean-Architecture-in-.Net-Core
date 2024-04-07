using Application.Common.Interfaces.Data;
using Application.Dtos;
using Domain.Entities;
using Domain.Entities.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands;

public sealed record CreateProductCommand(ProductDto Product) : IRequest<int>;
internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
	private readonly IApplicationDbContext _context;

	public CreateProductCommandHandler(IApplicationDbContext context) => 
		_context = context;

	public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		var productRequest = request.Product;
		var product = new Product()
		{
			Name = productRequest.Name,
			Description = productRequest.Description,
			Price = productRequest.Price,
			StockQuantity = productRequest.StockQuantity,
			CategoryId = new CategoryId(productRequest.CategoryId)
		};

		await _context.Product.AddAsync(product);
		return await _context.SaveChangesAsync(cancellationToken);
	}
}
