﻿using Application.Common.Interfaces.Data;
using Application.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Queries
{
	public sealed record GetCategoryQuery : IRequest<List<CategoryDto>>;

	internal sealed class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<CategoryDto>>
	{
		private readonly IApplicationDbContext _context;

		public GetCategoryQueryHandler(IApplicationDbContext context) =>
			_context = context;

		public async Task<List<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
		{
			var categories = await _context.Category.ToListAsync(cancellationToken);

			if (categories is null)
				return await Task.FromResult(new List<CategoryDto>());

			return categories.Adapt<List<CategoryDto>>();
		}
	}
}
