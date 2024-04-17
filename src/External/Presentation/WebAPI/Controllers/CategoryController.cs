using Application.Categories.Queries;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
	private readonly IMediator _mediator;

	public CategoryController(IMediator mediator) =>
		_mediator = mediator;


	[HttpGet]
	public async Task<ActionResult<List<CategoryDto>>> Get() =>
		await _mediator.Send(new GetCategoryQuery());
}
