using Application.Common.Interfaces.Data;
using Application.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries
{
	public sealed record GetProductQuery : IRequest<List<ProductDto>>;

	internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductDto>>
	{
		private readonly IApplicationDbContext _context;

		public GetProductQueryHandler(IApplicationDbContext context) =>
			_context = context;

		public async Task<List<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			var products = await _context.Product
				.Include(p => p.Category)
				.AsNoTracking().ToListAsync(cancellationToken);

			if (products is null)
				return await Task.FromResult(new List<ProductDto>());

			return products.Adapt<List<ProductDto>>();
		}
	}
}