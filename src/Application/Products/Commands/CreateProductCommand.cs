using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Products.Commands;

public sealed record CreateProductCommand(ProductDto Product) : IRequest<int>;
internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
	private readonly IUnitOfWork<Product> _unitOfWork;
	public CreateProductCommandHandler(IUnitOfWork<Product> unitOfWork) => 
		_unitOfWork = unitOfWork;

	public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		var product = request.Product.Adapt<Product>();
		await _unitOfWork.AddAsync(product, cancellationToken);

		return await _unitOfWork.SaveChangesAsync(cancellationToken);
	}
}