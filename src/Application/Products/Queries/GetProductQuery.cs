using Application.Common.Interfaces.Data;
using Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			var productResponse = await _context.Product.ToListAsync(cancellationToken);

			if (productResponse is null)
				return await Task.FromResult(new List<ProductDto>());

			var products = productResponse.Select(product => new ProductDto()
			{
				Id = product.Id.Value,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				StockQuantity = product.StockQuantity,
				CategoryId = product.CategoryId.Value
			}).ToList();

			return products;
		}
	}
}
