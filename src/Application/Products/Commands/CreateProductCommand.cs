using Application.Common.Interfaces.Data;
using Application.Dtos;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Products.Commands;

public sealed record CreateProductCommand(ProductDto Product) : IRequest<int>;
internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
	private readonly IApplicationDbContext _context;

	public CreateProductCommandHandler(IApplicationDbContext context) => 
		_context = context;

	public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		var product = request.Product.Adapt<Product>();
		await _context.Product.AddAsync(product, cancellationToken);

		return await _context.SaveChangesAsync(cancellationToken);
	}
}